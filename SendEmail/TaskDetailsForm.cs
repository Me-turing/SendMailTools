using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using SendEmail.model;
using SendEmail.Util;

namespace SendEmail
{
    public partial class TaskDetailsForm : Form
    {
        private SmtpClient smtpClient = null; //获取邮件链接
        private String loginUserName = null; //当前用户登录的角色
        
        public TaskDetailsForm(SmtpClient smtpClient,String loginUserName,TaskDetails taskDetails)
        {
            this.smtpClient = smtpClient;
            this.loginUserName = loginUserName;
            InitializeComponent();
            updateTaskDetailsToView();
        }
        
        // 这个方法用于计算OK状态的百分比
        private double CalculateOkPercentage(List<FileDetails> attachmentList)
        {
            double culateOk = 0.00;
            if (attachmentList!=null && attachmentList.Count > 0)
            {
                int okCount = attachmentList.Count(f => f.FileStatus.Equals("OK"));
                culateOk =  attachmentList.Count > 0 ? (double)okCount / attachmentList.Count * 100 : 0;
            }
            return culateOk;
        }

        public void updateTaskDetailsToView()
        {
            this.TaskListView.Items.Clear(); // 尝试情况现有内容
            List<TaskDetails> taskDetailsList = TaskDetails.TaskFactory.Instance.GetAllTaskDetails();
            if (!UtilTools.checkListOrSetIsNull(taskDetailsList))
            {
                foreach (var taskDetails in taskDetailsList)
                {
                    ListViewItem item = new ListViewItem(taskDetails.TaskNumber);
                    item.SubItems.Add(taskDetails.TaskTitle ?? "N/A");
                    if (taskDetails.AttachmentList != null)
                    {
                        item.SubItems.Add(taskDetails.AttachmentList.Count.ToString());
                        double percentage = CalculateOkPercentage(taskDetails.AttachmentList);
                        item.SubItems.Add($"{percentage:F2}%");
                    }
                    else
                    {
                        item.SubItems.Add("N/A");
                        item.SubItems.Add("N/A");
                    }
                    this.TaskListView.Items.Add(item);
                }
            }
        }

        private void AddTaskBtn_Click(object sender, EventArgs e)
        {
            //尝试获取任务
            TaskDetails taskDetails = TaskDetails.TaskFactory.Instance.GetTaskDetails(DateTime.Now.ToString("yyMMddHHmmss"));
            this.Hide();
            new BatchSendingForm(smtpClient,loginUserName,taskDetails,this).Show();
        }

        private void editTskBtn_Click(object sender, EventArgs e)
        {
            //获取当前选中行
            if (this.TaskListView.SelectedItems.Count > 0)
            {
                this.Hide();
                ListViewItem selectedItem = this.TaskListView.SelectedItems[0];  // 获取第一个选中的项
                TaskDetails taskDetails = TaskDetails.TaskFactory.Instance.GetTaskDetails(selectedItem.Text);
                new BatchSendingForm(smtpClient,loginUserName,taskDetails,this).Show();
                updateTaskDetailsToView();
            }
            else
            {
                MessageBox.Show("请选择一条任务操作");
            }
        }

        private void TaskListView_DoubleClick(object sender, EventArgs e)
        {
            editTskBtn_Click(sender, e);
        }

        private void DelTaskBtn_Click(object sender, EventArgs e)
        {
            if (this.TaskListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = this.TaskListView.SelectedItems[0];  // 获取第一个选中的项
                if (!string.IsNullOrEmpty(selectedItem.Text))
                {
                    DialogResult result = MessageBox.Show("您确定要删除吗？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        // 用户点击了"是"，执行删除操作
                        TaskDetails.TaskFactory.Instance.RemoveTaskDetail(selectedItem.Text);
                    }
                }
                //刷新ListView
                updateTaskDetailsToView();
            }
            else
            {
                MessageBox.Show("请选择一条任务操作");
            }
        }

        private async void sendEmailBtn_Click(object sender, EventArgs e)
        {
            //禁用所有的控件
            UtilTools.SetAllControlsEnabled(this,false);// 禁用控件
            //发送邮件
            var taskDetailsList = TaskDetails.TaskFactory.Instance.GetAllTaskDetails();
            foreach (var taskDetails in taskDetailsList)
            {
                int emailIndex = 1;
                if (taskDetails.AttachmentList != null && taskDetails.AttachmentList.Count > 0)
                {
                    foreach (var fileDetails in taskDetails.AttachmentList)
                    {
                        string messageStr = "";
                        //构建邮件并发送
                        var mailMessage = taskDetails.MessageInfo.getMailMessage(fileDetails,emailIndex);
                        if (mailMessage == null)
                        {
                            this.Invoke((MethodInvoker)delegate { this.updateTaskDetailsToView(); });
                            continue;
                        }

                        //如果是Ready尝试发送,否则跳过
                        if (fileDetails.FileStatus=="Ready")
                        {
                            messageStr = await Task.Run(() => new MailUtils().sendEmail(this.smtpClient,mailMessage));
                            if (messageStr!="Success")
                            {
                                MessageBox.Show(messageStr);
                                MessageBox.Show("点击[发送按钮],可以继续发送邮件");
                                return;
                            }
                            else
                            {
                                ++emailIndex;
                                fileDetails.FileStatus = "OK";
                            }
                            this.Invoke((MethodInvoker)delegate { this.updateTaskDetailsToView(); });
                        }
                    }
                }
                else
                {
                    var mailMessage = taskDetails.MessageInfo.getMailMessage(emailIndex);
                    string messageStr = await Task.Run(() => new MailUtils().sendEmail(this.smtpClient,mailMessage));
                    if (messageStr=="Success")
                    {
                        taskDetails.TaskSchedule = "100.00%";
                        taskDetails.AttachmentList.Add(new FileDetails("Single email without attachments",0,"N/A","N/A","OK"));
                    }
                }
                this.Invoke((MethodInvoker)delegate { this.updateTaskDetailsToView(); });    
            }
            //启用所有的控件
            UtilTools.SetAllControlsEnabled(this,true);// 禁用控件
        }

        private void TaskDetailsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}