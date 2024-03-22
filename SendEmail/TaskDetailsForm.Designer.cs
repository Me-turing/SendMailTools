using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SendEmail
{
    partial class TaskDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TaskListView = new System.Windows.Forms.ListView();
            this.taskNumber = new System.Windows.Forms.ColumnHeader();
            this.taskTitle = new System.Windows.Forms.ColumnHeader();
            this.emailCount = new System.Windows.Forms.ColumnHeader();
            this.taskSchedule = new System.Windows.Forms.ColumnHeader();
            this.DelTaskBtn = new System.Windows.Forms.Button();
            this.AddTaskBtn = new System.Windows.Forms.Button();
            this.sendEmailBtn = new System.Windows.Forms.Button();
            this.editTaskBtn = new System.Windows.Forms.Button();
            this.selectToUserListBtn = new System.Windows.Forms.Button();
            this.selectAttachmentBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.isTimedSending = new System.Windows.Forms.CheckBox();
            this.timedSendingDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.selectAttachmentTextBox = new System.Windows.Forms.TextBox();
            this.selectToUserListTextBox = new System.Windows.Forms.TextBox();
            this.dataGridBoolColumn1 = new System.Windows.Forms.DataGridBoolColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TaskListView
            // 
            this.TaskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.taskNumber, this.taskTitle, this.emailCount, this.taskSchedule });
            this.TaskListView.FullRowSelect = true;
            this.TaskListView.HideSelection = false;
            this.TaskListView.Location = new System.Drawing.Point(11, 84);
            this.TaskListView.Margin = new System.Windows.Forms.Padding(2);
            this.TaskListView.Name = "TaskListView";
            this.TaskListView.Size = new System.Drawing.Size(658, 243);
            this.TaskListView.TabIndex = 0;
            this.TaskListView.UseCompatibleStateImageBehavior = false;
            this.TaskListView.View = System.Windows.Forms.View.Details;
            this.TaskListView.DoubleClick += new System.EventHandler(this.TaskListView_DoubleClick);
            // 
            // taskNumber
            // 
            this.taskNumber.Text = "任务编号";
            this.taskNumber.Width = 120;
            // 
            // taskTitle
            // 
            this.taskTitle.Text = "邮件标题";
            this.taskTitle.Width = 320;
            // 
            // emailCount
            // 
            this.emailCount.Text = "邮件数量";
            this.emailCount.Width = 120;
            // 
            // taskSchedule
            // 
            this.taskSchedule.Text = "任务进度";
            this.taskSchedule.Width = 120;
            // 
            // DelTaskBtn
            // 
            this.DelTaskBtn.BackColor = System.Drawing.Color.White;
            this.DelTaskBtn.Location = new System.Drawing.Point(214, 23);
            this.DelTaskBtn.Margin = new System.Windows.Forms.Padding(2);
            this.DelTaskBtn.Name = "DelTaskBtn";
            this.DelTaskBtn.Size = new System.Drawing.Size(84, 48);
            this.DelTaskBtn.TabIndex = 2;
            this.DelTaskBtn.Text = "删除任务";
            this.DelTaskBtn.UseVisualStyleBackColor = false;
            this.DelTaskBtn.Click += new System.EventHandler(this.DelTaskBtn_Click);
            // 
            // AddTaskBtn
            // 
            this.AddTaskBtn.Location = new System.Drawing.Point(11, 23);
            this.AddTaskBtn.Margin = new System.Windows.Forms.Padding(2);
            this.AddTaskBtn.Name = "AddTaskBtn";
            this.AddTaskBtn.Size = new System.Drawing.Size(90, 48);
            this.AddTaskBtn.TabIndex = 1;
            this.AddTaskBtn.Text = "新增任务";
            this.AddTaskBtn.UseVisualStyleBackColor = true;
            this.AddTaskBtn.Click += new System.EventHandler(this.AddTaskBtn_Click);
            // 
            // sendEmailBtn
            // 
            this.sendEmailBtn.BackColor = System.Drawing.Color.White;
            this.sendEmailBtn.Location = new System.Drawing.Point(579, 23);
            this.sendEmailBtn.Margin = new System.Windows.Forms.Padding(2);
            this.sendEmailBtn.Name = "sendEmailBtn";
            this.sendEmailBtn.Size = new System.Drawing.Size(90, 48);
            this.sendEmailBtn.TabIndex = 3;
            this.sendEmailBtn.Text = "发送";
            this.sendEmailBtn.UseVisualStyleBackColor = false;
            this.sendEmailBtn.Click += new System.EventHandler(this.sendEmailBtn_Click);
            // 
            // editTaskBtn
            // 
            this.editTaskBtn.Location = new System.Drawing.Point(115, 23);
            this.editTaskBtn.Margin = new System.Windows.Forms.Padding(2);
            this.editTaskBtn.Name = "editTaskBtn";
            this.editTaskBtn.Size = new System.Drawing.Size(84, 48);
            this.editTaskBtn.TabIndex = 4;
            this.editTaskBtn.Text = "修改任务";
            this.editTaskBtn.UseVisualStyleBackColor = true;
            this.editTaskBtn.Click += new System.EventHandler(this.editTskBtn_Click);
            // 
            // selectToUserListBtn
            // 
            this.selectToUserListBtn.Location = new System.Drawing.Point(12, 26);
            this.selectToUserListBtn.Margin = new System.Windows.Forms.Padding(2);
            this.selectToUserListBtn.Name = "selectToUserListBtn";
            this.selectToUserListBtn.Size = new System.Drawing.Size(74, 22);
            this.selectToUserListBtn.TabIndex = 5;
            this.selectToUserListBtn.Text = "选择收件人";
            this.selectToUserListBtn.UseVisualStyleBackColor = true;
            this.selectToUserListBtn.Click += new System.EventHandler(this.batchInputTask_Click);
            // 
            // selectAttachmentBtn
            // 
            this.selectAttachmentBtn.Enabled = false;
            this.selectAttachmentBtn.Location = new System.Drawing.Point(12, 53);
            this.selectAttachmentBtn.Margin = new System.Windows.Forms.Padding(2);
            this.selectAttachmentBtn.Name = "selectAttachmentBtn";
            this.selectAttachmentBtn.Size = new System.Drawing.Size(74, 22);
            this.selectAttachmentBtn.TabIndex = 6;
            this.selectAttachmentBtn.Text = "选择附件";
            this.selectAttachmentBtn.UseVisualStyleBackColor = true;
            this.selectAttachmentBtn.Click += new System.EventHandler(this.selectAttachmentBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.editTaskBtn);
            this.groupBox1.Controls.Add(this.sendEmailBtn);
            this.groupBox1.Controls.Add(this.DelTaskBtn);
            this.groupBox1.Controls.Add(this.AddTaskBtn);
            this.groupBox1.Controls.Add(this.TaskListView);
            this.groupBox1.Location = new System.Drawing.Point(9, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(682, 341);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "任务详情";
            // 
            // isTimedSending
            // 
            this.isTimedSending.Location = new System.Drawing.Point(171, 20);
            this.isTimedSending.Name = "isTimedSending";
            this.isTimedSending.Size = new System.Drawing.Size(104, 24);
            this.isTimedSending.TabIndex = 7;
            this.isTimedSending.Text = "启用定时发送";
            this.isTimedSending.UseVisualStyleBackColor = true;
            // 
            // timedSendingDate
            // 
            this.timedSendingDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.timedSendingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timedSendingDate.Location = new System.Drawing.Point(11, 20);
            this.timedSendingDate.Name = "timedSendingDate";
            this.timedSendingDate.Size = new System.Drawing.Size(154, 21);
            this.timedSendingDate.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.selectAttachmentTextBox);
            this.groupBox2.Controls.Add(this.selectToUserListTextBox);
            this.groupBox2.Controls.Add(this.selectToUserListBtn);
            this.groupBox2.Controls.Add(this.selectAttachmentBtn);
            this.groupBox2.Location = new System.Drawing.Point(8, 411);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(681, 86);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "批量导入";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(278, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(399, 62);
            this.label1.TabIndex = 9;
            this.label1.Text = "批量导入使用方法:\r\n1. 选择收件人列表,格式: 收件人----抄送人----标题----内容\r\n2. 选择附件目录,将会按照收件人(主)进行自动分类,文件夹与" + "任务编号一致 \r\n   主收件人: 收件人第一个邮件地址\r\n3. 可以手动处理未正确映射的附件";
            // 
            // selectAttachmentTextBox
            // 
            this.selectAttachmentTextBox.Location = new System.Drawing.Point(100, 54);
            this.selectAttachmentTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.selectAttachmentTextBox.Name = "selectAttachmentTextBox";
            this.selectAttachmentTextBox.ReadOnly = true;
            this.selectAttachmentTextBox.Size = new System.Drawing.Size(152, 21);
            this.selectAttachmentTextBox.TabIndex = 8;
            // 
            // selectToUserListTextBox
            // 
            this.selectToUserListTextBox.Location = new System.Drawing.Point(100, 29);
            this.selectToUserListTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.selectToUserListTextBox.Name = "selectToUserListTextBox";
            this.selectToUserListTextBox.ReadOnly = true;
            this.selectToUserListTextBox.Size = new System.Drawing.Size(152, 21);
            this.selectToUserListTextBox.TabIndex = 7;
            // 
            // dataGridBoolColumn1
            // 
            this.dataGridBoolColumn1.Width = -1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.timedSendingDate);
            this.groupBox3.Controls.Add(this.isTimedSending);
            this.groupBox3.Location = new System.Drawing.Point(9, 351);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(682, 55);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "定时发送";
            // 
            // TaskDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 516);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "TaskDetailsForm";
            this.Text = "Task Details";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TaskDetailsForm_FormClosed);
            this.Load += new System.EventHandler(this.TaskDetailsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox groupBox3;

        private System.Windows.Forms.CheckBox isTimedSending;

        private System.Windows.Forms.DateTimePicker timedSendingDate;

        private System.Windows.Forms.DataGridBoolColumn dataGridBoolColumn1;

        private System.Windows.Forms.Button selectAttachmentBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox selectToUserListTextBox;
        private System.Windows.Forms.TextBox selectAttachmentTextBox;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Button selectToUserListBtn;

        private System.Windows.Forms.Button editTaskBtn;

        private System.Windows.Forms.ColumnHeader taskNumber;
        private System.Windows.Forms.ColumnHeader taskTitle;
        private System.Windows.Forms.ColumnHeader emailCount;
        private System.Windows.Forms.ColumnHeader taskSchedule;

        private System.Windows.Forms.Button sendEmailBtn;

        private System.Windows.Forms.Button AddTaskBtn;
        private System.Windows.Forms.Button DelTaskBtn;

        private System.Windows.Forms.ListView TaskListView;

        #endregion
    }
}