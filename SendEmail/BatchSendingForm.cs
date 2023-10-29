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
        
        private List<FileDetails> fileList = null; //附件列表
        private String pathStr = null; //附件地址
        
        public BatchSendingForm(SmtpClient smtpClient,String loginUserName)
        {
            this.smtpClient = smtpClient;
            this.loginUserName = loginUserName;
            InitializeComponent();
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
            if (string.IsNullOrEmpty(titleText))
            {
                MessageBox.Show("邮件标题不能为空");
                UtilTools.SetAllControlsEnabled(this,true);// 启用控件
                return;
            }
            
            //内容不能为空
            var mailInfoText = this.MailInfoText.Text;
            if (string.IsNullOrEmpty(mailInfoText))
            {
                MessageBox.Show("邮件内容不能为空");
                UtilTools.SetAllControlsEnabled(this,true);// 启用控件
                return;
            }
            
            //发送人不能为空
            var toUserAddress = this.toUserAddressList.Text;
            if (string.IsNullOrEmpty(toUserAddress)  || addToUserSet.Count==0)
            {
                MessageBox.Show("收件人不能为空");
                UtilTools.SetAllControlsEnabled(this,true);// 启用控件
                return;
            }
            
            //发送邮件,此时无附件
            List<String> addToUserList = addToUserSet.ToList();//获取发送列表
            List<String> addCCUserList = addCCUserSet.ToList();//获取抄送列表
            
            var messageStr = "";
            //判断是否有附件
            if (fileList!=null&&fileList.Count>0)
            {
                //循环发送邮件
                foreach (var fileDetails in fileList)
                {
                    //如果是Ready尝试发送,否则跳过
                    if (fileDetails.FileStatus=="Ready")
                    {
                        //构建Meaasge
                        MailMessage message = new MailMessage
                        {
                            From = new MailAddress(this.loginUserName),
                            Subject = this.titleTextBox.Text,
                            Body = this.MailInfoText.Text
                        };
                        
                        var attachmentPathList = new List<String>();
                        var attachmentPath = this.pathStr +"\\"+ fileDetails.FileName;
                        attachmentPathList.Add(attachmentPath);
                        messageStr = await Task.Run(() => new MailUtils().sendEmail(this.smtpClient, message, addToUserList, addCCUserList, attachmentPathList));
                        if (messageStr!="Success")
                        {
                            MessageBox.Show(messageStr);
                            MessageBox.Show("点击[发送按钮],可以继续发送邮件");
                            return;
                        }
                        else
                        {
                            fileDetails.FileStatus = "OK!";
                            this.Invoke((MethodInvoker)delegate { this.updateListView(fileList); });    
                        }
                        Thread.Sleep(1000); 
                    }
                }
            }
            else
            {
                //构建Meaasge
                MailMessage message = new MailMessage
                {
                    From = new MailAddress(this.loginUserName),
                    Subject = this.titleTextBox.Text,
                    Body = this.MailInfoText.Text
                };
                messageStr = await Task.Run(() => new MailUtils().sendEmail(this.smtpClient, message, addToUserList, addCCUserList, null));
            }
            
            if (messageStr!="")
            {
                MessageBox.Show(messageStr);
            }
            UtilTools.SetAllControlsEnabled(this,true);// 启用控件
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

        /// <summary>
        /// 程序退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MailInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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