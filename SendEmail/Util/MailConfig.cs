using System;
using System.Net;
using System.Net.Mail;

namespace SendEmail.Util
{
    public class MailConfig
    {
        private  int protInt; //邮件服务的端口
        private  string smtpAddress; //SMTP地址
        private  string userAddress; //发送人地址
        private  string userPwd; //发送人密码or授权码
        private  Boolean enableSsl; // 是否开启SSL加密
        
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="protStr">端口号</param>
        /// <param name="smtpAddress">SMTP地址</param>
        /// <param name="userAddress">登录用户地址</param>
        /// <param name="userPwd">登录用户密码</param>
        /// <param name="enableSsl">是否开启SSL加密通讯</param>
        public MailConfig(int protInt,String smtpAddress,String userAddress,String userPwd,Boolean enableSsl)
        {
            //这里优先校验是否初始化邮件的配置
            this.ProtInt = protInt;
            this.SmtpAddress = smtpAddress;
            this.UserAddress = userAddress;
            this.UserPwd = userPwd;
            this.EnableSsl = enableSsl;
        }
        
        /// <summary>
        /// 尝试获取邮件服务的连接
        /// </summary>
        public SmtpClient getClient()
        {
            SmtpClient client = new SmtpClient(smtpAddress); // SMTP的服务地址
            client.Port = protInt; // 邮件服务的端口
            client.Credentials = new NetworkCredential(userAddress, userPwd);
            client.EnableSsl = enableSsl; // 是否使用SSL连接
            return client;
        }


        private int ProtInt
        {
            get => protInt;
            set => protInt = value;
        }

        private string SmtpAddress
        {
            get => smtpAddress;
            set => smtpAddress = value;
        }

        public string UserAddress
        {
            get => userAddress;
            set => userAddress = value;
        }

        private string UserPwd
        {
            get => userPwd;
            set => userPwd = value;
        }

        private bool EnableSsl
        {
            get => enableSsl;
            set => enableSsl = value;
        }
    }
}