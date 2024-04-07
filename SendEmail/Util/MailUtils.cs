using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SendEmail.model;

namespace SendEmail.Util
{
    public class MailUtils
    {

        /// <summary>
        /// 尝试发送测试邮件
        /// </summary>
        /// <param name="client">邮件链接</param>
        /// <param name="userAddressStr">收件人地址</param>
        public async Task<Boolean> sendTestEmail(SmtpClient client,String userAddressStr)
        {
            Boolean falg = false;
            if (client==null)
            {
                return falg;
            }
            HashSet<String> toEmailAddressList = new HashSet<String>();
            toEmailAddressList.Add(userAddressStr);
            var mailMessage = new MessageInfo(userAddressStr,toEmailAddressList,
                null,"Test Email","如果您看到这条消息,意味着您的邮件配置一切正常.")
                .getMailMessage(1,1);
            var messageStr = sendEmail(client,mailMessage);
            if (messageStr.Result.Equals("Success"))
            {
                falg = true;
            }
            return falg;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<String> sendEmail(SmtpClient client,MailMessage message )
        {
            try
            {
                if (client==null)
                {
                    return "连接不能为空!";
                }

                if (message == null)
                {
                    return "消息不能为空!";
                }
                await Task.Delay(2000); // 延迟2秒
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