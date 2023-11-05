using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
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
        private String selectToUserListPath = null; //收件人列表路径
        private String pathStr = null; //附件地址


        
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
                                UtilTools.SetAllControlsEnabled(this,true);// 禁用控件
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
                    if (taskDetails.MessageInfo != null) 
                    {
                        var mailMessage = taskDetails.MessageInfo.getMailMessage(emailIndex);
                        string messageStr = await Task.Run(() => new MailUtils().sendEmail(this.smtpClient,mailMessage));
                        if (messageStr=="Success")
                        {
                            taskDetails.TaskSchedule = "100.00%";
                            taskDetails.AttachmentList.Add(new FileDetails("Single email without attachments",0,"N/A","N/A","OK"));
                        }   
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
        
        private void batchInputTask_Click(object sender, EventArgs e)
        {
            //选择指定的文件目录
            selectToUserListPath = UtilTools.getSelectTxtFile();
            this.selectToUserListTextBox.Text = selectToUserListPath;
            //遍历文件目录中的文件,获取UserInfoList -> 抽取方法
            var processEmailFile = UtilTools.ProcessEmailFile(selectToUserListPath);
            Dictionary<string, HashSet<string>> invalidEmails = new Dictionary<string, HashSet<string>>();
            // 遍历外部字典的每个键（例如，行号）
            foreach (var outerEntry in processEmailFile)
            {
                HashSet<string> recipientsSet = null;
                HashSet<string> ccsSet = null;
                HashSet<string> invalidEmailsSet = null;
                string messageInfo = "";
                
                string outerKey = outerEntry.Key; // 外部的键（行号）
                var taskDetails = TaskDetails.TaskFactory.Instance.GetTaskDetails(outerKey);
                var innerDict = outerEntry.Value; // 内部字典
                foreach (var innerEntry in innerDict)
                {
                    string setName = innerEntry.Key; // 内部字典的键（集合名称）
                    HashSet<string> setEmails = innerEntry.Value; // 对应的HashSet
                    switch (setName)
                    {
                        case "toAddress":
                            recipientsSet = setEmails; // 或者使用适当的方法来设置这个属性
                            break;
                        case "ccAddress":
                            ccsSet = setEmails; // 或者使用适当的方法来设置这个属性
                            break;
                        case "invalidAddress":
                            invalidEmailsSet = setEmails; // 或者使用适当的方法来设置这个属性
                            break;
                        case "titleStr":
                            taskDetails.TaskTitle = setEmails.First(); // 邮件标题
                            break;
                        case "messageInfo":
                            messageInfo = setEmails.First(); // 邮件内容
                            break;
                    }
                }

                if (!UtilTools.checkListOrSetIsNull(invalidEmailsSet))
                {
                    invalidEmails.Add(outerKey,invalidEmailsSet);
                }
                taskDetails.MessageInfo =  new MessageInfo(loginUserName, recipientsSet, ccsSet, taskDetails.TaskTitle, messageInfo);
            }
            //展示失败的邮件
            // 现在打印出所有的无效电子邮件地址，按任务编号分类
            var stringBuilder = new StringBuilder();
            foreach (var entry in invalidEmails)
            {
                string taskNumber = entry.Key;
                HashSet<string> emailSet = entry.Value;
                string emailList = String.Join(";", emailSet);
                stringBuilder.Append($" task number: {taskNumber} 中 {emailList} 导入失败 \n");
            }
            MessageBox.Show(stringBuilder.ToString());
            this.selectAttachmentBtn.Enabled = true;
            this.updateTaskDetailsToView();
        }

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
            updateTaskDetailsToView();
            //默认check第一条
            if (TaskListView != null && TaskListView.Items.Count > 0)
            {
                var firstItem = this.TaskListView.Items[0];
                if (firstItem != null)
                {
                    firstItem.Selected = true;
                    this.TaskListView.Focus(); // 给ListView控件焦点
                }
            }
        }
        
        /// <summary>
        /// 将附件映射到用户对象中
        /// </summary>
        /// <param name="fileDetailsList"></param>
        private void mappingAttachmentToUserInfo(List<FileDetails> fileDetailsList)
        {
            //所有任务第一个收件人-> 我们认为他是主要的收件人
            var taskNumberAndMainAddress = TaskDetails.TaskFactory.Instance.GetAllTaskDetails()
                .Where(td => td.MessageInfo != null && td.MessageInfo.ToEmailAddressList.Any())
                .ToDictionary(td => td.TaskNumber, td => td.MessageInfo.ToEmailAddressList.First());

            foreach (var mainAddress in taskNumberAndMainAddress)
            {
                //构建目录
                var subfoldersPath =UtilTools.CreateDirectoriesForSubfolders(pathStr, mainAddress.Key);
                //如果
                for (int i = fileDetailsList.Count - 1; i >= 0; i--)
                {
                    if (fileDetailsList[i].FileName.Contains(UtilTools.SanitizeEmailToLocalPart(mainAddress.Value)))
                    {
                        //将文件列表中的附件移动到对应目录
                        var flag = UtilTools.MoveFile(pathStr+"\\"+fileDetailsList[i].FileName,subfoldersPath);
                        //将文件对象添加到用户对象中
                        if (flag)
                        {
                            fileDetailsList[i].FileDirectory = subfoldersPath;
                            var attachmentList = TaskDetails.TaskFactory.Instance.GetTaskDetails(mainAddress.Key).AttachmentList;
                            bool directoryExists = attachmentList.Any(fd => fd.FileDirectory == subfoldersPath && fd.FileName == fileDetailsList[i].FileName);
                            if (!directoryExists)
                            {
                                FileDetails newObj = new FileDetails(fileDetailsList[i].FileName,
                                    fileDetailsList[i].FileSize,
                                    fileDetailsList[i].FileDirectory,
                                    fileDetailsList[i].FilePath,
                                    fileDetailsList[i].FileStatus);
                                attachmentList.Add(newObj);
                            }
                        }
                    }
                }
            }
        }
    }
}