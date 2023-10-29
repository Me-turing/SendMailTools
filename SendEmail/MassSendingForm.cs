using System;
using System.Collections.Generic;
using System.Drawing;
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
        private List<UserInfo> userInfoList = new List<UserInfo>(); 
        
        
        public MassSendingForm(SmtpClient smtpClient,String loginUserName)
        {
            this.smtpClient = smtpClient;
            this.loginUserName = loginUserName;
            InitializeComponent();
        }

        private void MassSendingForm_Load(object sender, EventArgs e)
        {
            // throw new System.NotImplementedException();
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

        //选中附件目录
        private void selectAttachmentBtn_Click(object sender, EventArgs e)
        {
            // 打开选中窗口,获得选中路径
            
            // 获取当前路径下面的所有次级文件夹列表信息
            
            // 根据次级目录 判断是否与邮件列表中的数据映射成功
            
            // 映射成功的更新List列表
            
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
                // 在此处执行您的代码，使用获取到的数据
                Console.WriteLine($" userAddress ：{userAddress} ");
            }
        }
    }
}