using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Forms;

namespace SendEmail.Util
{
    public class MailUtils
    {
        // private static string toEmailAddress = "your_email@example.com";//收件人地址
        // private static string ccEmailAddress = "your_email@example.com";//抄送人地址
        // private static string attachmentPath = "your_email@example.com";//附件地址

        /// <summary>
        /// 尝试发送测试邮件
        /// </summary>
        /// <param name="client">邮件链接</param>
        /// <param name="userAddressStr">收件人地址</param>
        public Boolean sendTestEmail(SmtpClient client,String userAddressStr)
        {
            Boolean falg = false;
            if (client==null)
            {
                return falg;
            }
            
            MailMessage message = new MailMessage
            {
                From = new MailAddress(userAddressStr),
                Subject = "Test Email",
                Body = "如果您看到这条消息,意味着您的邮件配置一切正常."
            };
            List<String> toEmailAddressList = new List<String>();
            toEmailAddressList.Add(userAddressStr);
            var messageStr = sendEmail(client,message, toEmailAddressList, null, null);
            if (messageStr.Equals("Success"))
            {
                falg = true;
            }
            return falg;
        }
        
        /// <summary>
        ///  发送邮件
        /// </summary>
        /// <param name="client">邮件链接</param>
        /// <param name="message">邮件内容</param>
        /// <param name="toEmailAddressList">发送地址列表</param>
        /// <param name="ccEmailAddressList">抄送地址列表</param>
        /// <param name="attachmentPath">附件</param>
        /// <returns></returns>
        public String sendEmail(SmtpClient client,MailMessage message,List<String> toEmailAddressList,List<String> ccEmailAddressList,List<String> attachmentPath )
        {
            try
            {
                if (client==null)
                {
                    return "连接不能为空!";
                }
                if (toEmailAddressList!=null && toEmailAddressList.Count>0)
                {
                    foreach (String toEmailAddress in toEmailAddressList)
                    {
                        message.To.Add(toEmailAddress); // 收件人地址
                    }
                }
                else
                {
                    return "收件人不能为空!";
                }

                if (ccEmailAddressList!=null && ccEmailAddressList.Count>0)
                {
                    foreach (String ccEmailAddress in ccEmailAddressList)
                    {
                        message.CC.Add(ccEmailAddress); // 抄送人地址
                    }
                }

                if (attachmentPath!=null && attachmentPath.Count>0)
                {
                    foreach (var attachmentStr in attachmentPath)
                    {
                        //添加附件
                        if (!String.IsNullOrEmpty(attachmentStr))
                        {
                            if (File.Exists(attachmentStr))
                            {
                                Attachment attachment = new Attachment(attachmentStr, MediaTypeNames.Application.Octet);
                                ContentDisposition disposition = attachment.ContentDisposition;
                                disposition.CreationDate = File.GetCreationTime(attachmentStr);
                                disposition.ModificationDate = File.GetLastWriteTime(attachmentStr);
                                disposition.ReadDate = File.GetLastAccessTime(attachmentStr);

                                message.Attachments.Add(attachment);
                            }
                            else
                            {
                                return "附件位置未找到! -> " + attachmentPath;
                            }
                        }
                    }
                }
                client.Send(message);
                return "Success";
            }
            catch (Exception ex)
            {
                return $"出现异常: {ex.Message}";
            }
        }
    }
}