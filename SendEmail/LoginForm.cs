using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SendEmail.Util;

namespace SendEmail
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

       /// <summary>
       /// 点击登录按钮
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void loginBtn_Click(object sender, EventArgs e)
        {
            //获取当前输入的用户名和密码
            var userName = this.userNameTextBox.Text;
            var userPwd = this.userPwdTextBox.Text;
            var smtpAddress = this.smtpAddressTextBox.Text;
            var prot = this.protTextBox.Text;
                
            //参数不能为空
            if (String.IsNullOrEmpty(userName)||String.IsNullOrEmpty(userPwd)||
                String.IsNullOrEmpty(smtpAddress)||String.IsNullOrEmpty(prot))
            {
                MessageBox.Show("参数不能为空~!");
                return;
            }
                
            //参数格式需要正确
            Boolean isValidPort = UtilTools.IsValidPort(prot);
            if (!isValidPort)
            {
                MessageBox.Show("请正确输入端口~!");
            }
            
            //与指定的邮件服务进行交互
            MailConfig mailConfig = new MailConfig(int.Parse(prot),smtpAddress,userName,userPwd,false);
            SmtpClient smtpClient = mailConfig.getClient();
            Boolean sendTestEmail = new MailUtils().sendTestEmail(smtpClient,userName);
            //判断是否登录成功
            if (sendTestEmail)
            {
                MessageBox.Show("登陆成功!");
                // 记住用户配置
                Properties.Settings.Default.userName = userName;
                Properties.Settings.Default.userPwd = userPwd;
                Properties.Settings.Default.prot = prot;
                Properties.Settings.Default.smtpAddress = smtpAddress;
                Properties.Settings.Default.MassSending = this.MassSendingBtn.Checked;
                Properties.Settings.Default.BatchSending = this.BatchSendingBtn.Checked;
                Properties.Settings.Default.Save();

                if (this.MassSendingBtn.Checked)
                {
                    MessageBox.Show("开发中....");
                }
                else
                {
                    new BatchSendingForm(smtpClient,userName).Show();
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("请检查输入!");
            }
        }

       private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
       {
           Application.Exit();
       }
       

       /// <summary>
       /// 群发邮件
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       /// <exception cref="NotImplementedException"></exception>
       private void MassSendingBtn_CheckedChanged(object sender, EventArgs e)
       {
           if (this.MassSendingBtn.Checked)
           {
               this.BatchSendingBtn.Checked = false;
           }
       }

       /// <summary>
       /// 批量发送附件
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       /// <exception cref="NotImplementedException"></exception>
       private void BatchSendingBtn_CheckedChanged(object sender, EventArgs e)
       {
           if (this.BatchSendingBtn.Checked )
           {
               this.MassSendingBtn.Checked = false;
           }
       }

       /// <summary>
       /// 窗口初始化加载
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       /// <exception cref="NotImplementedException"></exception>
       private void LoginForm_Load(object sender, EventArgs e)
       {
           //尝试从Setting中获取值
           this.userNameTextBox.Text = Properties.Settings.Default.userName;
           this.userPwdTextBox.Text = Properties.Settings.Default.userPwd;
           this.protTextBox.Text = Properties.Settings.Default.prot;
           this.smtpAddressTextBox.Text =  Properties.Settings.Default.smtpAddress;
       }
    }
}