using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using SendEmail.model;
using SendEmail.Util;

namespace SendEmail
{
    public partial class MassSendingForm : Form
    {
        private SmtpClient smtpClient = null; //获取邮件链接
        private String loginUserName = null; //当前用户登录的角色
        private String selectToUserListPath = null; //收件人列表路径
        private UserInfo.UserFactory userFactory = new UserInfo.UserFactory();//收件人工厂类
        // private List<UserInfo> userInfoList = new List<UserInfo>(); //用户对象
        private String pathStr = null; //附件地址
        
        public MassSendingForm(SmtpClient smtpClient,String loginUserName)
        {
            this.smtpClient = smtpClient;
            this.loginUserName = loginUserName;
            InitializeComponent();
        }

        private void MassSendingForm_Load(object sender, EventArgs e)
        {
            // throw new System.NotImplementedException();
            this.sendBtn.Enabled = false;
            this.selectAttachmentBtn.Enabled = false;
        }

        /// <summary>
        /// 单击选择发送人列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void selectToUserListBtn_Click(object sender, EventArgs e)
        {
            //选择指定的文件目录
            selectToUserListPath = UtilTools.getSelectTxtFile();
            this.selectToUserListTextBox.Text = selectToUserListPath;
            //遍历文件目录中的文件,获取UserInfoList -> 抽取方法
            var (validEmails, invalidEmails) = UtilTools.readAndValidateTxtFile(selectToUserListPath);
            if (validEmails.Count>0)
            {
                //初始化发送队列
                foreach (var validEmail in validEmails)
                {
                   userFactory.GetUser(validEmail);
                }
                initTaskQueueListView(userFactory.GetAllUser());
                TaskQueueListView.Items[0].Selected = true; // 选中行
                TaskQueueListView.Focus(); // 给ListView控件焦点
            }
            
            if (invalidEmails.Count>0)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("以下地址导入失败: 不符合邮件格式....\n");
                foreach (var invalidEmail in invalidEmails)
                {
                    stringBuilder.Append(invalidEmail + "\n");
                }
                MessageBox.Show(stringBuilder.ToString());
            }

            this.sendBtn.Enabled = true;
            this.selectAttachmentBtn.Enabled = true;
        }
        
        /// <summary>
        /// 初始化TaskQueueListView
        /// </summary>
        /// <param name="userInfoList"></param>
        private void initTaskQueueListView(IReadOnlyList<IUser> userInfoList)
        {
            this.TaskQueueListView.Items.Clear(); // 尝试情况现有内容
            for (var i = 0; i < userInfoList.Count; i++)
            {
                ListViewItem lvi = new ListViewItem((i + 1).ToString());
                lvi.SubItems.Add(userInfoList[i].ToUserAddress);
                lvi.SubItems.Add("N/A");
                lvi.SubItems.Add("0");
                lvi.SubItems.Add("0%");
                this.TaskQueueListView.Items.Add(lvi);
            }
        }
        
        /// <summary>
        /// 更新TaskQueueListView
        /// </summary>
        /// <param name="userInfoList"></param>
        private void updateTaskQueueListView(List<UserInfo> userInfoList)
        {
            this.TaskQueueListView.Items.Clear(); // 尝试情况现有内容
            for (var i = 0; i < userInfoList.Count; i++)
            {
                ListViewItem lvi = new ListViewItem((i + 1).ToString());
                lvi.SubItems.Add(userInfoList[i].ToUserAddress);
                lvi.SubItems.Add(userInfoList[i].AttachmentPath);
                lvi.SubItems.Add(userInfoList[i].FileDetailsList.Count.ToString());
                lvi.SubItems.Add("0%");
                this.TaskQueueListView.Items.Add(lvi);
            }
        }

        //选中附件目录
        private void selectAttachmentBtn_Click(object sender, EventArgs e)
        {
            // 打开选中窗口,获得选中路径
            //选择指定的文件目录
            pathStr = UtilTools.getSelectPath();
            this.selectAttachmentTextBox.Text = pathStr;
            //遍历文件目录中的文件,获取FileInfoList -> 抽取方法
            var fileDetailsList = UtilTools.getFileList(pathStr);
            // 获取当前路径下面的所有次级文件夹列表信息,根据次级目录 判断是否与邮件列表中的数据映射成功
            mappingAttachmentToUserInfo(fileDetailsList);
            // 映射成功的更新List列表
            updateTaskQueueListView(userFactory.GetAllUser());
        }

        /// <summary>
        /// TaskQueueListView的选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void TaskQueueListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TaskQueueListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = TaskQueueListView.SelectedItems[0];  // 获取第一个选中的项
                string userAddress = selectedItem.SubItems[1].Text;  // 获取选中的邮件地址
                //获取用户选中的对象,更新附件详情的ListView
                updateTaskDetailsListView((UserInfo)userFactory.GetUser(userAddress));
            }
        }
        
        /// <summary>
        /// 更新TaskDetailsListView
        /// </summary>
        /// <param name="userInfoList"></param>
        private void updateTaskDetailsListView(UserInfo userInfo)
        {
            this.TaskDetailsListView.Items.Clear(); // 尝试情况现有内容
            
            //尝试从目录获取最新
            List<FileDetails> fileDetailsList = UtilTools.getFileList(userInfo.AttachmentPath);
            // 先获取FileDetailsList1中所有的FileDetails.name
            var namesInList = userInfo.FileDetailsList.Select(f => f.FileName).ToHashSet();
            // 找到FileDetailsList2中与FileDetailsList1不重复的FileDetails.name元素
            var itemsToMove = fileDetailsList.Where(f => !namesInList.Contains(f.FileName)).ToList();
            
            if (itemsToMove.Count>0)
            {
                // 将这些项添加到FileDetailsList1
                userInfo.FileDetailsList.AddRange(itemsToMove);
                updateTaskQueueListView(userFactory.GetAllUser());
            }
            
            for (var i = 0; i < userInfo.FileDetailsList.Count; i++)
            {
                ListViewItem lvi = new ListViewItem((i + 1).ToString());
                lvi.SubItems.Add(userInfo.FileDetailsList[i].FileName);
                double megabytes = (double)userInfo.FileDetailsList[i].FileSize / (1024 * 1024);
                string formattedMegabytes = megabytes.ToString("0.0000");
                lvi.SubItems.Add(formattedMegabytes + " MB");
                lvi.SubItems.Add(userInfo.FileDetailsList[i].FileStatus);
                this.TaskDetailsListView.Items.Add(lvi);
            }
        }

        /// <summary>
        /// 将附件映射到用户对象中
        /// </summary>
        /// <param name="fileDetailsList"></param>
        private void mappingAttachmentToUserInfo(List<FileDetails> fileDetailsList)
        {
            //所有用户的昵称列表
            string[] nickName =  userFactory.GetAllUser().Select(u => u.NickName).ToArray();
            //遍历用户对象
            foreach (var userInfo in userFactory.GetAllUser())
            {
                var sanitizeEmailToLocalPart = userInfo.NickName;
                //构建对应的目录
                var subfoldersPath = UtilTools.CreateDirectoriesForSubfolders(pathStr, sanitizeEmailToLocalPart);
                userInfo.AttachmentPath = subfoldersPath;
                //获取可以移动的文件
                for (int i = fileDetailsList.Count - 1; i >= 0; i--)
                {
                    //判断附件名称是否满足多个用户匹配,如果是则跳过
                    if (nickName.Count(s => fileDetailsList[i].FileName.Contains(s)) != 1)
                    {
                        continue;
                    }
                    
                    if (fileDetailsList[i].FileName.Contains(sanitizeEmailToLocalPart))
                    {
                        //将文件列表中的附件移动到对应目录
                        var flag = UtilTools.MoveFile(pathStr+"\\"+fileDetailsList[i].FileName,subfoldersPath);
                       //将文件对象添加到用户对象中
                        if (flag)
                        {
                            userInfo.FileDetailsList.Add(fileDetailsList[i]);
                        }
                        fileDetailsList.RemoveAt(i);
                    }
                }
            }

            //如果存在没有Mapping上的文件,则打开目录用户手动处理
            if (fileDetailsList.Count>0)
            {
                MessageBox.Show("您还有没有被映射上的文件,请手动移动到对应用户的文件夹后,再次点击该用户的刷新!或忽视该文件!");
                Process.Start("explorer.exe", pathStr);
            }
        }

        /// <summary>
        /// 双击用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskQueueListView_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("计划双击新增收件人~~~");
        }

        /// <summary>
        /// 单击 TaskQueueListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TaskQueueListView_Click(object sender, EventArgs e)
        {
            if (TaskQueueListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = TaskQueueListView.SelectedItems[0];  // 获取第一个选中的项
                string userAddress = selectedItem.SubItems[1].Text;  // 获取选中的邮件地址
                //获取用户选中的对象,更新附件详情的ListView
                updateTaskDetailsListView((UserInfo)userFactory.GetUser(userAddress));
            }
        }

        /// <summary>
        /// 发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void sendBtn_Click(object sender, EventArgs e)
        {
            //前沿检查
            
            //构建对象的Message内容
            
            //异步发送
            
            //更新ListView
        }
    }
}