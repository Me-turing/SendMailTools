using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SendEmail
{
    partial class BatchSendingForm
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
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toUserAddressList = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ccUserAddressList = new System.Windows.Forms.TextBox();
            this.SendBtn = new System.Windows.Forms.Button();
            this.MailInfoText = new System.Windows.Forms.TextBox();
            this.FilePathText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.selectFileBtn = new System.Windows.Forms.Button();
            this.FileListView = new System.Windows.Forms.ListView();
            this.FileNumber = new System.Windows.Forms.ColumnHeader();
            this.FileName = new System.Windows.Forms.ColumnHeader();
            this.FileSize = new System.Windows.Forms.ColumnHeader();
            this.Status = new System.Windows.Forms.ColumnHeader();
            this.addToUserList = new System.Windows.Forms.Button();
            this.addCCUserList = new System.Windows.Forms.Button();
            this.delToUserListBtn = new System.Windows.Forms.Button();
            this.delCCUserListBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(198, 6);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(414, 25);
            this.titleTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(119, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "主  题 : ";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(119, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "收件人 : ";
            // 
            // toUserAddressList
            // 
            this.toUserAddressList.Location = new System.Drawing.Point(198, 37);
            this.toUserAddressList.Name = "toUserAddressList";
            this.toUserAddressList.ReadOnly = true;
            this.toUserAddressList.Size = new System.Drawing.Size(284, 25);
            this.toUserAddressList.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(119, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "抄送人 : ";
            // 
            // ccUserAddressList
            // 
            this.ccUserAddressList.Location = new System.Drawing.Point(198, 68);
            this.ccUserAddressList.Name = "ccUserAddressList";
            this.ccUserAddressList.ReadOnly = true;
            this.ccUserAddressList.Size = new System.Drawing.Size(284, 25);
            this.ccUserAddressList.TabIndex = 4;
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(22, 9);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(88, 84);
            this.SendBtn.TabIndex = 6;
            this.SendBtn.Text = "发送";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // MailInfoText
            // 
            this.MailInfoText.Location = new System.Drawing.Point(22, 113);
            this.MailInfoText.Multiline = true;
            this.MailInfoText.Name = "MailInfoText";
            this.MailInfoText.Size = new System.Drawing.Size(590, 276);
            this.MailInfoText.TabIndex = 7;
            // 
            // FilePathText
            // 
            this.FilePathText.Location = new System.Drawing.Point(101, 410);
            this.FilePathText.Name = "FilePathText";
            this.FilePathText.ReadOnly = true;
            this.FilePathText.Size = new System.Drawing.Size(417, 25);
            this.FilePathText.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(22, 413);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "附 件: ";
            // 
            // selectFileBtn
            // 
            this.selectFileBtn.Location = new System.Drawing.Point(524, 409);
            this.selectFileBtn.Name = "selectFileBtn";
            this.selectFileBtn.Size = new System.Drawing.Size(88, 26);
            this.selectFileBtn.TabIndex = 10;
            this.selectFileBtn.Text = "浏览";
            this.selectFileBtn.UseVisualStyleBackColor = true;
            this.selectFileBtn.Click += new System.EventHandler(this.selectFileBtn_Click);
            // 
            // FileListView
            // 
            this.FileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.FileNumber, this.FileName, this.FileSize, this.Status });
            this.FileListView.HideSelection = false;
            this.FileListView.Location = new System.Drawing.Point(22, 450);
            this.FileListView.Name = "FileListView";
            this.FileListView.Size = new System.Drawing.Size(590, 234);
            this.FileListView.TabIndex = 11;
            this.FileListView.UseCompatibleStateImageBehavior = false;
            this.FileListView.View = System.Windows.Forms.View.Details;
            // 
            // FileNumber
            // 
            this.FileNumber.Text = "No.";
            this.FileNumber.Width = 40;
            // 
            // FileName
            // 
            this.FileName.Text = "FileName";
            this.FileName.Width = 178;
            // 
            // FileSize
            // 
            this.FileSize.Text = "FileSize";
            this.FileSize.Width = 100;
            // 
            // Status
            // 
            this.Status.Text = "Status";
            this.Status.Width = 80;
            // 
            // addToUserList
            // 
            this.addToUserList.Location = new System.Drawing.Point(488, 38);
            this.addToUserList.Name = "addToUserList";
            this.addToUserList.Size = new System.Drawing.Size(59, 26);
            this.addToUserList.TabIndex = 12;
            this.addToUserList.Text = "新增";
            this.addToUserList.UseVisualStyleBackColor = true;
            this.addToUserList.Click += new System.EventHandler(this.addToUserList_Click);
            // 
            // addCCUserList
            // 
            this.addCCUserList.Location = new System.Drawing.Point(488, 68);
            this.addCCUserList.Name = "addCCUserList";
            this.addCCUserList.Size = new System.Drawing.Size(59, 26);
            this.addCCUserList.TabIndex = 13;
            this.addCCUserList.Text = "新增";
            this.addCCUserList.UseVisualStyleBackColor = true;
            this.addCCUserList.Click += new System.EventHandler(this.addCCUserList_Click);
            // 
            // delToUserListBtn
            // 
            this.delToUserListBtn.Location = new System.Drawing.Point(553, 38);
            this.delToUserListBtn.Name = "delToUserListBtn";
            this.delToUserListBtn.Size = new System.Drawing.Size(59, 26);
            this.delToUserListBtn.TabIndex = 14;
            this.delToUserListBtn.Text = "删除";
            this.delToUserListBtn.UseVisualStyleBackColor = true;
            this.delToUserListBtn.Click += new System.EventHandler(this.delToUserListBtn_Click);
            // 
            // delCCUserListBtn
            // 
            this.delCCUserListBtn.Location = new System.Drawing.Point(553, 70);
            this.delCCUserListBtn.Name = "delCCUserListBtn";
            this.delCCUserListBtn.Size = new System.Drawing.Size(59, 26);
            this.delCCUserListBtn.TabIndex = 15;
            this.delCCUserListBtn.Text = "删除";
            this.delCCUserListBtn.UseVisualStyleBackColor = true;
            this.delCCUserListBtn.Click += new System.EventHandler(this.delCCUserListBtn_Click);
            // 
            // BatchSendingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 696);
            this.Controls.Add(this.delCCUserListBtn);
            this.Controls.Add(this.delToUserListBtn);
            this.Controls.Add(this.addCCUserList);
            this.Controls.Add(this.addToUserList);
            this.Controls.Add(this.FileListView);
            this.Controls.Add(this.selectFileBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FilePathText);
            this.Controls.Add(this.MailInfoText);
            this.Controls.Add(this.SendBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ccUserAddressList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toUserAddressList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.titleTextBox);
            this.MaximizeBox = false;
            this.Name = "BatchSendingForm";
            this.Text = "SendEmail v1.0           By.SEVENTEEN";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MailInfo_FormClosed);
            this.Load += new System.EventHandler(this.BatchSendingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button delToUserListBtn;
        private System.Windows.Forms.Button delCCUserListBtn;

        private System.Windows.Forms.Button addToUserList;
        private System.Windows.Forms.Button addCCUserList;

        private System.Windows.Forms.ColumnHeader FileNumber;
        private System.Windows.Forms.ColumnHeader FileName;
        private System.Windows.Forms.ColumnHeader FileSize;
        private System.Windows.Forms.ColumnHeader Status;

        private System.Windows.Forms.ListView FileListView;

        private System.Windows.Forms.Button selectFileBtn;

        private System.Windows.Forms.TextBox FilePathText;
        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.TextBox MailInfoText;

        private System.Windows.Forms.Button SendBtn;

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ccUserAddressList;

        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox toUserAddressList;

        #endregion
    }
}