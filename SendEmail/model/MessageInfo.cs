using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using SendEmail.Util;

namespace SendEmail.model
{
    /// <summary>
    /// 邮件的信息类
    /// </summary>
    public class MessageInfo
    {
        
        private string fromEmailAddress;//发送人信息
        private List<String> toEmailAddressList;//收件人列表
        private List<String> ccEmailAddressList;//抄送人列表
        private string emailTitle;//邮件标题
        private string emailMessage;//邮件内容
        private List<FileDetails> attachmentList; //附件列表
        
        /// <summary>
        /// 构造对象
        /// </summary>
        /// <param name="fromEmailAddress"></param>
        /// <param name="toEmailAddressList"></param>
        /// <param name="ccEmailAddressList"></param>
        /// <param name="emailTitle"></param>
        /// <param name="emailMessage"></param>
        public MessageInfo(string fromEmailAddress, List<string> toEmailAddressList, List<string> ccEmailAddressList, string emailTitle, string emailMessage)
        {
            this.fromEmailAddress = fromEmailAddress;
            this.toEmailAddressList = toEmailAddressList;
            this.ccEmailAddressList = ccEmailAddressList;
            this.emailTitle = emailTitle;
            this.emailMessage = emailMessage;
        }
        
        /// <summary>
        /// 创建多附件邮件
        /// </summary>
        /// <param name="attachmentList"></param>
        /// <returns></returns>
        public MailMessage getMailMessage(List<FileDetails> attachmentList)
        {
            //构造
            MailMessage mailMessage = getMailMessage();
            
            //添加附件
            foreach (var fileDetails in attachmentList)
            {
                if (!String.IsNullOrEmpty(fileDetails.FilePath))
                {
                    if (File.Exists(fileDetails.FilePath))
                    {
                        Attachment attachment = new Attachment(fileDetails.FilePath, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = attachment.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(fileDetails.FilePath);
                        disposition.ModificationDate = File.GetLastWriteTime(fileDetails.FilePath);
                        disposition.ReadDate = File.GetLastAccessTime(fileDetails.FilePath);
                        mailMessage.Attachments.Add(attachment);
                    }
                }
            }
            return mailMessage;
        }
        
        /// <summary>
        /// 创建单一附件邮件
        /// </summary>
        /// <param name="fileDetails"></param>
        /// <returns></returns>
        public MailMessage getMailMessage(FileDetails fileDetails)
        {
            //构造
            MailMessage mailMessage = getMailMessage();
            
            //添加附件
            if (!String.IsNullOrEmpty(fileDetails.FilePath))
            {
                if (File.Exists(fileDetails.FilePath))
                {
                    Attachment attachment = new Attachment(fileDetails.FilePath, MediaTypeNames.Application.Octet);
                    ContentDisposition disposition = attachment.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(fileDetails.FilePath);
                    disposition.ModificationDate = File.GetLastWriteTime(fileDetails.FilePath);
                    disposition.ReadDate = File.GetLastAccessTime(fileDetails.FilePath);
                    mailMessage.Attachments.Add(attachment);
                }
            }
            return mailMessage;
        }
        
        /// <summary>
        /// 创建无附件邮件
        /// </summary>
        /// <returns></returns>
        public MailMessage getMailMessage()
        {
            //优先检查必须项目
            if (string.IsNullOrEmpty(fromEmailAddress)||UtilTools.checkListIsNull(toEmailAddressList)||
                string.IsNullOrEmpty(emailTitle)||string.IsNullOrEmpty(emailMessage))
            {
                Console.WriteLine("必须属性不能为空~!");
                return null;
            }
            //构造
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmailAddress),
                Subject = emailTitle,
                Body = emailMessage
            };

            //添加发送人
            foreach (var toEmailAddress in toEmailAddressList)
            {
                mailMessage.To.Add(toEmailAddress);
            }
            
            //添加抄送人
            if (!UtilTools.checkListIsNull(ccEmailAddressList))
            {
                foreach (var ccEmailAddress in ccEmailAddressList)
                {
                    mailMessage.CC.Add(ccEmailAddress);
                }
            }
            return mailMessage;
        }
    }
}