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
            this.SuspendLayout();
            // 
            // TaskListView
            // 
            this.TaskListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.taskNumber, this.taskTitle, this.emailCount, this.taskSchedule });
            this.TaskListView.FullRowSelect = true;
            this.TaskListView.HideSelection = false;
            this.TaskListView.Location = new System.Drawing.Point(12, 68);
            this.TaskListView.Name = "TaskListView";
            this.TaskListView.Size = new System.Drawing.Size(876, 293);
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
            this.DelTaskBtn.Location = new System.Drawing.Point(248, 12);
            this.DelTaskBtn.Name = "DelTaskBtn";
            this.DelTaskBtn.Size = new System.Drawing.Size(112, 41);
            this.DelTaskBtn.TabIndex = 2;
            this.DelTaskBtn.Text = "删除任务";
            this.DelTaskBtn.UseVisualStyleBackColor = true;
            this.DelTaskBtn.Click += new System.EventHandler(this.DelTaskBtn_Click);
            // 
            // AddTaskBtn
            // 
            this.AddTaskBtn.Location = new System.Drawing.Point(12, 12);
            this.AddTaskBtn.Name = "AddTaskBtn";
            this.AddTaskBtn.Size = new System.Drawing.Size(112, 41);
            this.AddTaskBtn.TabIndex = 1;
            this.AddTaskBtn.Text = "新增任务";
            this.AddTaskBtn.UseVisualStyleBackColor = true;
            this.AddTaskBtn.Click += new System.EventHandler(this.AddTaskBtn_Click);
            // 
            // sendEmailBtn
            // 
            this.sendEmailBtn.Location = new System.Drawing.Point(776, 12);
            this.sendEmailBtn.Name = "sendEmailBtn";
            this.sendEmailBtn.Size = new System.Drawing.Size(112, 41);
            this.sendEmailBtn.TabIndex = 3;
            this.sendEmailBtn.Text = "发送";
            this.sendEmailBtn.UseVisualStyleBackColor = true;
            this.sendEmailBtn.Click += new System.EventHandler(this.sendEmailBtn_Click);
            // 
            // editTaskBtn
            // 
            this.editTaskBtn.Location = new System.Drawing.Point(130, 12);
            this.editTaskBtn.Name = "editTaskBtn";
            this.editTaskBtn.Size = new System.Drawing.Size(112, 41);
            this.editTaskBtn.TabIndex = 4;
            this.editTaskBtn.Text = "修改任务";
            this.editTaskBtn.UseVisualStyleBackColor = true;
            this.editTaskBtn.Click += new System.EventHandler(this.editTskBtn_Click);
            // 
            // TaskDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 372);
            this.Controls.Add(this.editTaskBtn);
            this.Controls.Add(this.sendEmailBtn);
            this.Controls.Add(this.DelTaskBtn);
            this.Controls.Add(this.AddTaskBtn);
            this.Controls.Add(this.TaskListView);
            this.MaximizeBox = false;
            this.Name = "TaskDetailsForm";
            this.Text = "MassSendingForm";
            this.ResumeLayout(false);
        }

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