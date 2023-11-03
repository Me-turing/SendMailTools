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
            this.listView1 = new System.Windows.Forms.ListView();
            this.DelTaskBtn = new System.Windows.Forms.Button();
            this.AddTaskBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 68);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(876, 293);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // DelTaskBtn
            // 
            this.DelTaskBtn.Location = new System.Drawing.Point(130, 12);
            this.DelTaskBtn.Name = "DelTaskBtn";
            this.DelTaskBtn.Size = new System.Drawing.Size(112, 41);
            this.DelTaskBtn.TabIndex = 2;
            this.DelTaskBtn.Text = "删除任务";
            this.DelTaskBtn.UseVisualStyleBackColor = true;
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
            // TaskDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 373);
            this.Controls.Add(this.DelTaskBtn);
            this.Controls.Add(this.AddTaskBtn);
            this.Controls.Add(this.listView1);
            this.Name = "TaskDetailsForm";
            this.Text = "MassSendingForm";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button AddTaskBtn;
        private System.Windows.Forms.Button DelTaskBtn;

        private System.Windows.Forms.ListView listView1;

        #endregion
    }
}