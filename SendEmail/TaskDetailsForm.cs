using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace SendEmail
{
    public partial class TaskDetailsForm : Form
    {
        private SmtpClient smtpClient = null; //获取邮件链接
        private String loginUserName = null; //当前用户登录的角色
        
        public TaskDetailsForm(SmtpClient smtpClient,String loginUserName )
        {
            this.smtpClient = smtpClient;
            this.loginUserName = loginUserName;
            InitializeComponent();
        }

        private void AddTaskBtn_Click(object sender, EventArgs e)
        {
            new BatchSendingForm(smtpClient,loginUserName).Show();
            this.Hide();
        }
    }
}