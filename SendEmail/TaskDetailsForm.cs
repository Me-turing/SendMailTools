using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
            int okCount = attachmentList.Count(f => f.FileStatus.Equals("OK"));
            return attachmentList.Count > 0 ? (double)okCount / attachmentList.Count * 100 : 0;
        }

        public void updateTaskDetailsToView()
        {
            this.TaskListView.Items.Clear(); // 尝试情况现有内容
            List<TaskDetails> taskDetailsList = TaskDetails.TaskFactory.Instance.GetAllTaskDetails();
            if(UtilTools.checkListOrSetIsNull(taskDetailsList)) return;
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
    }
}