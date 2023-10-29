namespace SendEmail
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.userPwdTextBox = new System.Windows.Forms.TextBox();
            this.loginBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.protTextBox = new System.Windows.Forms.TextBox();
            this.smtpAddressTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.mode = new System.Windows.Forms.GroupBox();
            this.MassSendingBtn = new System.Windows.Forms.RadioButton();
            this.BatchSendingBtn = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.mode.SuspendLayout();
            this.SuspendLayout();
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(121, 39);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(216, 25);
            this.userNameTextBox.TabIndex = 0;
            // 
            // userPwdTextBox
            // 
            this.userPwdTextBox.Location = new System.Drawing.Point(121, 83);
            this.userPwdTextBox.Name = "userPwdTextBox";
            this.userPwdTextBox.Size = new System.Drawing.Size(216, 25);
            this.userPwdTextBox.TabIndex = 1;
            this.userPwdTextBox.UseSystemPasswordChar = true;
            // 
            // loginBtn
            // 
            this.loginBtn.Location = new System.Drawing.Point(354, 39);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(126, 68);
            this.loginBtn.TabIndex = 2;
            this.loginBtn.Text = "登录";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "登录用户:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "用户密码:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.userNameTextBox);
            this.groupBox1.Controls.Add(this.userPwdTextBox);
            this.groupBox1.Controls.Add(this.loginBtn);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(492, 137);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.protTextBox);
            this.groupBox2.Controls.Add(this.smtpAddressTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(10, 216);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(492, 65);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "配置";
            // 
            // protTextBox
            // 
            this.protTextBox.Location = new System.Drawing.Point(424, 24);
            this.protTextBox.Name = "protTextBox";
            this.protTextBox.Size = new System.Drawing.Size(56, 25);
            this.protTextBox.TabIndex = 3;
            // 
            // smtpAddressTextBox
            // 
            this.smtpAddressTextBox.Location = new System.Drawing.Point(121, 24);
            this.smtpAddressTextBox.Name = "smtpAddressTextBox";
            this.smtpAddressTextBox.Size = new System.Drawing.Size(216, 25);
            this.smtpAddressTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "服务器地址:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(354, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 22);
            this.label4.TabIndex = 2;
            this.label4.Text = "端 口:";
            // 
            // mode
            // 
            this.mode.Controls.Add(this.MassSendingBtn);
            this.mode.Controls.Add(this.BatchSendingBtn);
            this.mode.Location = new System.Drawing.Point(11, 155);
            this.mode.Name = "mode";
            this.mode.Size = new System.Drawing.Size(491, 55);
            this.mode.TabIndex = 7;
            this.mode.TabStop = false;
            this.mode.Text = "模式";
            // 
            // MassSendingBtn
            // 
            this.MassSendingBtn.Location = new System.Drawing.Point(141, 24);
            this.MassSendingBtn.Name = "MassSendingBtn";
            this.MassSendingBtn.Size = new System.Drawing.Size(104, 24);
            this.MassSendingBtn.TabIndex = 2;
            this.MassSendingBtn.Text = "群发模式";
            this.MassSendingBtn.UseVisualStyleBackColor = true;
            this.MassSendingBtn.CheckedChanged += new System.EventHandler(this.MassSendingBtn_CheckedChanged);
            // 
            // BatchSendingBtn
            // 
            this.BatchSendingBtn.Checked = true;
            this.BatchSendingBtn.Location = new System.Drawing.Point(15, 24);
            this.BatchSendingBtn.Name = "BatchSendingBtn";
            this.BatchSendingBtn.Size = new System.Drawing.Size(120, 24);
            this.BatchSendingBtn.TabIndex = 1;
            this.BatchSendingBtn.TabStop = true;
            this.BatchSendingBtn.Text = "批量发送附件";
            this.BatchSendingBtn.UseVisualStyleBackColor = true;
            this.BatchSendingBtn.CheckedChanged += new System.EventHandler(this.BatchSendingBtn_CheckedChanged);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 294);
            this.Controls.Add(this.mode);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.Text = "SendEmail v1.0           By.SEVENTEEN";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoginForm_FormClosed);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.mode.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.RadioButton MassSendingBtn;

        private System.Windows.Forms.RadioButton MassHairBtn;

        private System.Windows.Forms.RadioButton BatchSendingBtn;

        private System.Windows.Forms.GroupBox mode;

        private System.Windows.Forms.TextBox protTextBox;
        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox smtpAddressTextBox;

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TextBox userPwdTextBox;
        private System.Windows.Forms.Button loginBtn;

        #endregion
    }
}