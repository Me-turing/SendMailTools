using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using SendEmail.model;
using SendEmail.Util;

namespace SendEmail
{
    public partial class BatchSendingForm : Form
    {
        private HashSet<String> addToUserSet = new HashSet<string>();//存放发送人地址
        private HashSet<String> addCCUserSet = new HashSet<String>();//存放抄送人地址
        private SmtpClient smtpClient = null; //获取邮件链接
        private String loginUserName = null; //当前用户登录的角色
        private TaskDetails taskDetails = null; //任务详情
        
        private List<FileDetails> fileList = null; //附件列表
        private String pathStr = null; //附件地址
        private TaskDetailsForm taskDetailsForm;
        
        public BatchSendingForm(SmtpClient smtpClient,String loginUserName,TaskDetails taskDetails,TaskDetailsForm form)
        {
            InitializeComponent();
            this.smtpClient = smtpClient;
            this.loginUserName = loginUserName;
            this.taskDetailsForm = form;
            initFromInfo(taskDetails);
        }

        /// <summary>
        /// 初始化页面信息
        /// </summary>
        /// <param name="taskDetails"></param>
        private void initFromInfo(TaskDetails taskDetails)
        {
            //执行初始化操作
            if (taskDetails != null)
            {
                fileList = taskDetails.AttachmentList;
                if (!UtilTools.checkListOrSetIsNull(fileList))
                {
                    this.FilePathText.Text = fileList[0].FileDirectory;
                }
                if (taskDetails.MessageInfo != null)
                {
                    addToUserSet.Clear();
                    addCCUserSet.Clear();
                    addToUserSet.UnionWith(taskDetails.MessageInfo.ToEmailAddressList);
                    this.toUserAddressList.Text = UtilTools.formatMailingList(addToUserSet);
                    addCCUserSet.UnionWith(taskDetails.MessageInfo.CcEmailAddressList);
                    this.ccUserAddressList.Text = UtilTools.formatMailingList(addCCUserSet);
                    this.MailInfoText.Text = taskDetails.MessageInfo.EmailMessage;
                    this.titleTextBox.Text = taskDetails.MessageInfo.EmailTitle;
                }
                this.taskDetails = taskDetails;
                this.updateListView(fileList);
            }
        }

        /// <summary>
        /// 单击添加发送列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addToUserList_Click(object sender, EventArgs e)
        {
            String inputMailAddress = this.getUserInputEmailAddress();
            if (!inputMailAddress.Equals(""))
            {
                addToUserSet.Add(inputMailAddress);
            }
            this.toUserAddressList.Text = UtilTools.formatMailingList(addToUserSet);
        }

        /// <summary>
        /// 单击新增抄送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addCCUserList_Click(object sender, EventArgs e)
        {
            String inputMailAddress = this.getUserInputEmailAddress();
            if (!inputMailAddress.Equals(""))
            {
                addCCUserSet.Add(inputMailAddress);
            }
            this.ccUserAddressList.Text = UtilTools.formatMailingList(addCCUserSet);
        }

        /// <summary>
        /// 获取用户数输入的邮件地址
        /// </summary>
        /// <returns></returns>
        private String getUserInputEmailAddress()
        {
            //获取用户当前输入的邮件地址
            String inputMailAddress = "";
            //校验当前输入的邮件是否是合规
            inputMailAddress = Interaction.InputBox("请输入邮箱地址", "输入邮箱地址", "", 100, 100);
            if (string.IsNullOrEmpty(inputMailAddress))
            {
                return "";
            }
            if (!UtilTools.IsValidEmail(inputMailAddress))
            {
                MessageBox.Show("邮件格式不合规,请检查");
                return "";
            }
            return inputMailAddress;
        }

        /// <summary>
        /// 单击发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void SendBtn_Click(object sender, EventArgs e)
        {
            UtilTools.SetAllControlsEnabled(this,false);// 禁用控件
            
            //检查主题不能为空
            var titleText = this.titleTextBox.Text;
            //内容不能为空
            var mailInfoText = this.MailInfoText.Text;
            //发送人不能为空
            var toUserAddress = this.toUserAddressList.Text;
            var userInputResult = checkUserInput(titleText, mailInfoText, toUserAddress);
            if (userInputResult.Length > 0)
            {
                MessageBox.Show(userInputResult);
                return;
            }

            //发送邮件,此时无附件
            HashSet<String> addToUserList = addToUserSet.ToHashSet();//获取发送列表
            HashSet<String> addCCUserList = addCCUserSet.ToHashSet();//获取抄送列表
            
            taskDetails.MessageInfo = new MessageInfo(loginUserName, addToUserList, addCCUserList, titleText, mailInfoText);
            taskDetails.TaskTitle = this.titleTextBox.Text;
            taskDetails.EmailCount = fileList.Count;
            taskDetails.TaskSchedule = CalculateOkPercentage(fileList);
            //判断是否有附件
            if (fileList!=null&&fileList.Count>0)
            {
                taskDetails.AttachmentList = fileList;
            }
            UtilTools.SetAllControlsEnabled(this,true);// 启用控件
            taskDetailsForm.Show();
            taskDetailsForm.updateTaskDetailsToView();
            this.Hide();
        }
        private string CalculateOkPercentage(List<FileDetails> attachmentList)
        {
            int okCount = attachmentList.Count(f => f.FileStatus.Equals("OK"));
            return attachmentList.Count > 0 ? ((double)okCount / attachmentList.Count * 100) + " %" : "0.00 %";
        }
        
        private string checkUserInput(String titleText,String mailInfoText,string toUserAddress)
        {
            string result = "";
            if (string.IsNullOrEmpty(titleText))
            {
                result += "邮件标题不能为空 \n";
          
            }
            //内容不能为空
            if (string.IsNullOrEmpty(mailInfoText))
            {
                result += "邮件内容不能为空 \n";
            }
            
            //发送人不能为空
            if (string.IsNullOrEmpty(toUserAddress)  || addToUserSet.Count==0)
            {
                result += "收件人不能为空 \n";
            }
            UtilTools.SetAllControlsEnabled(this,true);// 启用控件
            return result;
        }

        /// <summary>
        /// 选择附件目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            //选择指定的文件目录
            pathStr = UtilTools.getSelectPath();
            this.FilePathText.Text = pathStr;
            //遍历文件目录中的文件,获取FileInfoList -> 抽取方法
            fileList = UtilTools.getFileList(pathStr);
            //更新List视图 -> 抽取方法
            foreach (var fileDetails in fileList)
            {
                Console.WriteLine($"文件名：{fileDetails.FileName}, 文件大小：{fileDetails.FileSize} 字节, 文件状态：{fileDetails.FileStatus} 字节");
            }
            //更新ListView
            this.updateListView(fileList);
        }


        /// <summary>
        /// 更新ListView
        /// </summary>
        /// <param name="fileList"></param>
        private void updateListView(List<FileDetails> fileList)
        {
            if (this.FileListView != null && fileList != null)
            {
                this.FileListView.Items.Clear(); // 尝试情况现有内容
                for (var i = 0; i < fileList.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem((i + 1).ToString());
                    lvi.SubItems.Add(fileList[i].FileName);
                    double megabytes = (double)fileList[i].FileSize / (1024 * 1024);
                    string formattedMegabytes = megabytes.ToString("0.0000");
                    lvi.SubItems.Add(formattedMegabytes + " MB");
                    lvi.SubItems.Add(fileList[i].FileStatus);
                    this.FileListView.Items.Add(lvi);
                }
            }
        }

        /// <summary>
        /// 程序退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MailInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Application.Exit();
            //检查主题不能为空
            var titleText = this.titleTextBox.Text;
            //内容不能为空
            var mailInfoText = this.MailInfoText.Text;
            //发送人不能为空
            var toUserAddress = this.toUserAddressList.Text;
            var userInputResult = checkUserInput(titleText, mailInfoText, toUserAddress);
            if (userInputResult.Length > 0 || taskDetails.MessageInfo is null)
            {
                TaskDetails.TaskFactory.Instance.RemoveTaskDetail(taskDetails.TaskNumber);
            }
            this.Hide();
            taskDetailsForm.Show();
            taskDetailsForm.updateTaskDetailsToView();
        }

        /// <summary>
        /// 删除发送列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void delToUserListBtn_Click(object sender, EventArgs e)
        {
            String inputMailAddress = this.getUserInputEmailAddress();
            if (!inputMailAddress.Equals(""))
            {
                if (addToUserSet.Contains(inputMailAddress))
                {
                    addToUserSet.Remove(inputMailAddress);
                }
            }
            this.toUserAddressList.Text = UtilTools.formatMailingList(addToUserSet);
        }

        /// <summary>
        /// 删除抄送列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void delCCUserListBtn_Click(object sender, EventArgs e)
        {
            String inputMailAddress = this.getUserInputEmailAddress();
            if (!inputMailAddress.Equals(""))
            {
                if (addCCUserSet.Contains(inputMailAddress))
                {
                    addCCUserSet.Remove(inputMailAddress);
                }
            }
            this.ccUserAddressList.Text = UtilTools.formatMailingList(addCCUserSet);
        }

        /// <summary>
        /// 页面初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void BatchSendingForm_Load(object sender, EventArgs e)
        {
            // throw new System.NotImplementedException();
        }
    }
}