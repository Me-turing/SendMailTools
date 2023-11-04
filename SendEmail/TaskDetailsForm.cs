using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Windows.Forms;
using SendEmail.model;

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
            updateTaskDetail(taskDetails);
        }

        public void updateTaskDetail(TaskDetails taskDetails)
        {
            if (taskDetails == null) return;

            ListViewItem existingItem = this.TaskListView.Items
                .Cast<ListViewItem>()
                .FirstOrDefault(item => item.Text.Equals(taskDetails.TaskNumber));

            // 如果找到了匹配的ListViewItem，则更新它
            if (existingItem != null)
            {
                UpdateListViewItem(existingItem, taskDetails);
            }
            else
            {
                // 如果没有找到，则添加新的ListViewItem
                // 首先创建一个新的ListViewItem
                ListViewItem item = CreateListViewItemFromTaskDetails(taskDetails);
                // 然后将它添加到ListView控件中
                this.TaskListView.Items.Add(item);
            }
        }
        private void UpdateListViewItem(ListViewItem item, TaskDetails taskDetails)
        {
            // 更新ListViewItem的标题
            item.SubItems[1].Text = taskDetails.TaskTitle ?? "N/A";

            // 更新附件数量和百分比
            if (taskDetails.AttachmentList != null)
            {
                item.SubItems[2].Text = taskDetails.AttachmentList.Count.ToString();
                double percentage = CalculateOkPercentage(taskDetails.AttachmentList);
                item.SubItems[3].Text = $"{percentage:F2}%";
            }
            else
            {
                item.SubItems[2].Text = "N/A";
                item.SubItems[3].Text = "N/A";
            }
        }
        
        private ListViewItem CreateListViewItemFromTaskDetails(TaskDetails taskDetails)
        {
            // 创建一个新的ListViewItem并根据taskDetails填充它
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
            return item;
        }

        // 这个方法用于计算OK状态的百分比
        private double CalculateOkPercentage(List<FileDetails> attachmentList)
        {
            int okCount = attachmentList.Count(f => f.FileStatus.Equals("OK"));
            return attachmentList.Count > 0 ? (double)okCount / attachmentList.Count * 100 : 0;
        }

        private void AddTaskBtn_Click(object sender, EventArgs e)
        {
            //尝试获取任务
            TaskDetails taskDetails = TaskDetails.TaskFactory.Instance.GetTaskDetails(DateTime.Now.ToString("yyMMddHHmmss"));
            this.Hide();
            new BatchSendingForm(smtpClient,loginUserName,taskDetails,this).Show();
        }

        private void editTaskBtn_Click(object sender, EventArgs e)
        {
            //获取当前选中行
            if (this.TaskListView.SelectedItems.Count > 0)
            {
                this.Hide();
                ListViewItem selectedItem = this.TaskListView.SelectedItems[0];  // 获取第一个选中的项
                TaskDetails taskDetails = TaskDetails.TaskFactory.Instance.GetTaskDetails(selectedItem.Text);
                new BatchSendingForm(smtpClient,loginUserName,taskDetails,this).Show();
            }
        }
    }
}