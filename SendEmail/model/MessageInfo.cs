using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using SendEmail.Util;

namespace SendEmail.model
{
    /// <summary>
    /// 邮件的信息类
    /// </summary>
    public class MessageInfo
    {
        private string fromEmailAddress;//发送人信息
        private HashSet<String> toEmailAddressList = new HashSet<string>();//收件人列表
        private HashSet<String> ccEmailAddressList = new HashSet<string>();//抄送人列表
        private string emailTitle;//邮件标题
        private string emailMessage;//邮件内容
        
        public string FromEmailAddress
        {
            get => fromEmailAddress;
            set => fromEmailAddress = value;
        }

        public HashSet<string> ToEmailAddressList
        {
            get => toEmailAddressList;
            set => toEmailAddressList = value;
        }

        public HashSet<string> CcEmailAddressList
        {
            get => ccEmailAddressList;
            set => ccEmailAddressList = value;
        }

        public string EmailTitle
        {
            get => emailTitle;
            set => emailTitle = value;
        }

        public string EmailMessage
        {
            get => emailMessage;
            set => emailMessage = value;
        }
        
        /// <summary>
        /// 构造对象
        /// </summary>
        /// <param name="fromEmailAddress"></param>
        /// <param name="toEmailAddressList"></param>
        /// <param name="ccEmailAddressList"></param>
        /// <param name="emailTitle"></param>
        /// <param name="emailMessage"></param>
        public MessageInfo(string fromEmailAddress, HashSet<string> toEmailAddressList, HashSet<string> ccEmailAddressList, string emailTitle, string emailMessage)
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
        public MailMessage getMailMessage(List<FileDetails> attachmentList,int emailCount,int emailIndex)
        {
            //构造
            MailMessage mailMessage = getMailMessage(emailCount,emailIndex);
            var stringBuilder = new StringBuilder();
            //添加附件
            foreach (var fileDetails in attachmentList)
            {
                if (!String.IsNullOrEmpty(fileDetails.FilePath))
                {
                    if (File.Exists(fileDetails.FilePath))
                    {
                        stringBuilder.Append(fileDetails.FileName + "/");
                        Attachment attachment = new Attachment(fileDetails.FilePath, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = attachment.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(fileDetails.FilePath);
                        disposition.ModificationDate = File.GetLastWriteTime(fileDetails.FilePath);
                        disposition.ReadDate = File.GetLastAccessTime(fileDetails.FilePath);
                        mailMessage.Attachments.Add(attachment);
                    }
                }
            }
            var fileNames = stringBuilder.ToString();
            fileNames = fileNames.Substring(0, fileNames.Length - 1);
            mailMessage.Body = MagicVariable.ReplaceMagicValues(emailMessage, fileNames,
                emailCount.ToString(),emailIndex.ToString(), UtilTools.SanitizeEmailToLocalPart(toEmailAddressList));
            return mailMessage;
        }
        
        /// <summary>
        /// 创建单一附件邮件
        /// </summary>
        /// <param name="fileDetails"></param>
        /// <returns></returns>
        public MailMessage getMailMessage(FileDetails fileDetails,int emailCount,int emailIndex)
        {
            //构造
            MailMessage mailMessage = getMailMessage(emailCount,emailIndex);
            //添加附件
            if (!String.IsNullOrEmpty(fileDetails.FilePath) && File.Exists(fileDetails.FilePath))
            {
                mailMessage.Body = MagicVariable.ReplaceMagicValues(emailMessage, fileDetails.FileName,
                    emailCount.ToString(),emailIndex.ToString(), UtilTools.SanitizeEmailToLocalPart(toEmailAddressList));
                Attachment attachment = new Attachment(fileDetails.FilePath, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = attachment.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(fileDetails.FilePath);
                disposition.ModificationDate = File.GetLastWriteTime(fileDetails.FilePath);
                disposition.ReadDate = File.GetLastAccessTime(fileDetails.FilePath);
                mailMessage.Attachments.Add(attachment);
            }
            else
            {
                //如果附件不存在则返回空
                return null;
            }
            return mailMessage;
        }
        
        /// <summary>
        /// 创建无附件邮件
        /// </summary>
        /// <returns></returns>
        public MailMessage getMailMessage(int emailCount,int emailIndex)
        {
            //优先检查必须项目
            if (string.IsNullOrEmpty(fromEmailAddress)||UtilTools.checkListOrSetIsNull(toEmailAddressList)||
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
                Body = MagicVariable.ReplaceMagicValues(emailMessage,"",emailCount.ToString(),emailIndex.ToString(),UtilTools.SanitizeEmailToLocalPart(toEmailAddressList))
            };

            //添加发送人
            foreach (var toEmailAddress in toEmailAddressList)
            {
                mailMessage.To.Add(toEmailAddress);
            }
            
            //添加抄送人
            if (!UtilTools.checkListOrSetIsNull(ccEmailAddressList))
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