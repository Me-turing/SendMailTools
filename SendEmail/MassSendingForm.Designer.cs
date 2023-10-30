using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SendEmail
{
    partial class MassSendingForm
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
            this.TaskQueueListView = new System.Windows.Forms.ListView();
            this.numbers = new System.Windows.Forms.ColumnHeader();
            this.toUserAddress = new System.Windows.Forms.ColumnHeader();
            this.AttachmentPath = new System.Windows.Forms.ColumnHeader();
            this.NumberofAttachments = new System.Windows.Forms.ColumnHeader();
            this.completeness = new System.Windows.Forms.ColumnHeader();
            this.TaskDetailsListView = new System.Windows.Forms.ListView();
            this.serialNo = new System.Windows.Forms.ColumnHeader();
            this.attachmentName = new System.Windows.Forms.ColumnHeader();
            this.size = new System.Windows.Forms.ColumnHeader();
            this.status = new System.Windows.Forms.ColumnHeader();
            this.TaskQueueGroup = new System.Windows.Forms.GroupBox();
            this.TaskDetailsListGroup = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.selectToUserListTextBox = new System.Windows.Forms.TextBox();
            this.selectAttachmentTextBox = new System.Windows.Forms.TextBox();
            this.selectToUserListBtn = new System.Windows.Forms.Button();
            this.selectAttachmentBtn = new System.Windows.Forms.Button();
            this.sendBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mailTitleTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TaskQueueGroup.SuspendLayout();
            this.TaskDetailsListGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TaskQueueListView
            // 
            this.TaskQueueListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.numbers, this.toUserAddress, this.AttachmentPath, this.NumberofAttachments, this.completeness });
            this.TaskQueueListView.FullRowSelect = true;
            this.TaskQueueListView.HideSelection = false;
            this.TaskQueueListView.Location = new System.Drawing.Point(17, 24);
            this.TaskQueueListView.MultiSelect = false;
            this.TaskQueueListView.Name = "TaskQueueListView";
            this.TaskQueueListView.Size = new System.Drawing.Size(669, 197);
            this.TaskQueueListView.TabIndex = 0;
            this.TaskQueueListView.UseCompatibleStateImageBehavior = false;
            this.TaskQueueListView.View = System.Windows.Forms.View.Details;
            this.TaskQueueListView.SelectedIndexChanged += new System.EventHandler(this.TaskQueueListView_SelectedIndexChanged);
            this.TaskQueueListView.Click += new System.EventHandler(this.TaskQueueListView_Click);
            this.TaskQueueListView.DoubleClick += new System.EventHandler(this.TaskQueueListView_DoubleClick);
            // 
            // numbers
            // 
            this.numbers.Text = "编号";
            // 
            // toUserAddress
            // 
            this.toUserAddress.Text = "收件人地址";
            this.toUserAddress.Width = 140;
            // 
            // AttachmentPath
            // 
            this.AttachmentPath.Text = "附件地址";
            this.AttachmentPath.Width = 180;
            // 
            // NumberofAttachments
            // 
            this.NumberofAttachments.Text = "附件数量";
            // 
            // completeness
            // 
            this.completeness.Text = "完成进度";
            this.completeness.Width = 80;
            // 
            // TaskDetailsListView
            // 
            this.TaskDetailsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.serialNo, this.attachmentName, this.size, this.status });
            this.TaskDetailsListView.HideSelection = false;
            this.TaskDetailsListView.Location = new System.Drawing.Point(14, 24);
            this.TaskDetailsListView.Name = "TaskDetailsListView";
            this.TaskDetailsListView.Size = new System.Drawing.Size(669, 260);
            this.TaskDetailsListView.TabIndex = 1;
            this.TaskDetailsListView.UseCompatibleStateImageBehavior = false;
            this.TaskDetailsListView.View = System.Windows.Forms.View.Details;
            // 
            // serialNo
            // 
            this.serialNo.Text = "编号";
            // 
            // attachmentName
            // 
            this.attachmentName.Text = "附件名称";
            this.attachmentName.Width = 140;
            // 
            // size
            // 
            this.size.Text = "大小";
            // 
            // status
            // 
            this.status.Text = "状态";
            this.status.Width = 80;
            // 
            // TaskQueueGroup
            // 
            this.TaskQueueGroup.Controls.Add(this.TaskQueueListView);
            this.TaskQueueGroup.Location = new System.Drawing.Point(604, 12);
            this.TaskQueueGroup.Name = "TaskQueueGroup";
            this.TaskQueueGroup.Size = new System.Drawing.Size(696, 236);
            this.TaskQueueGroup.TabIndex = 2;
            this.TaskQueueGroup.TabStop = false;
            this.TaskQueueGroup.Text = "任务队列";
            // 
            // TaskDetailsListGroup
            // 
            this.TaskDetailsListGroup.Controls.Add(this.TaskDetailsListView);
            this.TaskDetailsListGroup.Location = new System.Drawing.Point(607, 259);
            this.TaskDetailsListGroup.Name = "TaskDetailsListGroup";
            this.TaskDetailsListGroup.Size = new System.Drawing.Size(693, 296);
            this.TaskDetailsListGroup.TabIndex = 3;
            this.TaskDetailsListGroup.TabStop = false;
            this.TaskDetailsListGroup.Text = "任务详情列表";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(139, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "收件列表:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(139, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "附件目录:";
            // 
            // messageTextBox
            // 
            this.messageTextBox.AcceptsReturn = true;
            this.messageTextBox.Location = new System.Drawing.Point(16, 157);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.messageTextBox.Size = new System.Drawing.Size(545, 374);
            this.messageTextBox.TabIndex = 6;
            // 
            // selectToUserListTextBox
            // 
            this.selectToUserListTextBox.Location = new System.Drawing.Point(218, 31);
            this.selectToUserListTextBox.Name = "selectToUserListTextBox";
            this.selectToUserListTextBox.ReadOnly = true;
            this.selectToUserListTextBox.Size = new System.Drawing.Size(250, 25);
            this.selectToUserListTextBox.TabIndex = 7;
            // 
            // selectAttachmentTextBox
            // 
            this.selectAttachmentTextBox.Location = new System.Drawing.Point(218, 71);
            this.selectAttachmentTextBox.Name = "selectAttachmentTextBox";
            this.selectAttachmentTextBox.ReadOnly = true;
            this.selectAttachmentTextBox.Size = new System.Drawing.Size(250, 25);
            this.selectAttachmentTextBox.TabIndex = 8;
            // 
            // selectToUserListBtn
            // 
            this.selectToUserListBtn.Location = new System.Drawing.Point(486, 29);
            this.selectToUserListBtn.Name = "selectToUserListBtn";
            this.selectToUserListBtn.Size = new System.Drawing.Size(75, 27);
            this.selectToUserListBtn.TabIndex = 9;
            this.selectToUserListBtn.Text = "浏览";
            this.selectToUserListBtn.UseVisualStyleBackColor = true;
            this.selectToUserListBtn.Click += new System.EventHandler(this.selectToUserListBtn_Click);
            // 
            // selectAttachmentBtn
            // 
            this.selectAttachmentBtn.Location = new System.Drawing.Point(486, 69);
            this.selectAttachmentBtn.Name = "selectAttachmentBtn";
            this.selectAttachmentBtn.Size = new System.Drawing.Size(75, 28);
            this.selectAttachmentBtn.TabIndex = 10;
            this.selectAttachmentBtn.Text = "浏览";
            this.selectAttachmentBtn.UseVisualStyleBackColor = true;
            this.selectAttachmentBtn.Click += new System.EventHandler(this.selectAttachmentBtn_Click);
            // 
            // sendBtn
            // 
            this.sendBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.sendBtn.Location = new System.Drawing.Point(15, 24);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(118, 78);
            this.sendBtn.TabIndex = 11;
            this.sendBtn.Text = "发 送";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mailTitleTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.sendBtn);
            this.groupBox1.Controls.Add(this.messageTextBox);
            this.groupBox1.Controls.Add(this.selectAttachmentBtn);
            this.groupBox1.Controls.Add(this.selectToUserListBtn);
            this.groupBox1.Controls.Add(this.selectAttachmentTextBox);
            this.groupBox1.Controls.Add(this.selectToUserListTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 543);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "邮件详情";
            // 
            // mailTitleTextBox
            // 
            this.mailTitleTextBox.Location = new System.Drawing.Point(96, 116);
            this.mailTitleTextBox.Name = "mailTitleTextBox";
            this.mailTitleTextBox.Size = new System.Drawing.Size(465, 25);
            this.mailTitleTextBox.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "邮件标题:";
            // 
            // MassSendingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 564);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TaskDetailsListGroup);
            this.Controls.Add(this.TaskQueueGroup);
            this.Name = "MassSendingForm";
            this.Text = "MassSendingForm";
            this.Load += new System.EventHandler(this.MassSendingForm_Load);
            this.TaskQueueGroup.ResumeLayout(false);
            this.TaskDetailsListGroup.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mailTitleTextBox;

        private System.Windows.Forms.ColumnHeader serialNo;
        private System.Windows.Forms.ColumnHeader attachmentName;
        private System.Windows.Forms.ColumnHeader size;
        private System.Windows.Forms.ColumnHeader status;

        private System.Windows.Forms.ColumnHeader completeness;

        private System.Windows.Forms.ColumnHeader numbers;
        private System.Windows.Forms.ColumnHeader toUserAddress;
        private System.Windows.Forms.ColumnHeader AttachmentPath;
        private System.Windows.Forms.ColumnHeader NumberofAttachments;

        private System.Windows.Forms.GroupBox groupBox1;

        private System.Windows.Forms.TextBox selectToUserListTextBox;
        private System.Windows.Forms.TextBox selectAttachmentTextBox;
        private System.Windows.Forms.Button selectToUserListBtn;
        private System.Windows.Forms.Button selectAttachmentBtn;
        private System.Windows.Forms.Button sendBtn;

        private System.Windows.Forms.TextBox messageTextBox;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.GroupBox TaskDetailsListGroup;

        private System.Windows.Forms.ListView TaskDetailsListView;
        private System.Windows.Forms.GroupBox TaskQueueGroup;

        private System.Windows.Forms.ListView TaskQueueListView;

        #endregion
    }
}