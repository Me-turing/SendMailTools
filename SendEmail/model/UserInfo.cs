using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using SendEmail.Util;

namespace SendEmail.model
{
    public class UserInfo : IUser
    {
        private MailMessage toUserMessage = new MailMessage();
        private String attachmentPath;
        private List<FileDetails> fileDetailsList = new List<FileDetails>();
        public string ToUserAddress { get; set; }
        public string NickName { get; set; }
        
        public String AttachmentPath
        {
            get => attachmentPath;
            set => attachmentPath = value;
        }

        public MailMessage ToUserMessage
        {
            get => toUserMessage;
            set => toUserMessage = value;
        }

        public List<FileDetails> FileDetailsList
        {
            get => fileDetailsList;
            set => fileDetailsList = value;
        }
        
        // 将构造函数设置为私有，以防止直接实例化
        private UserInfo() { }
        
        public class UserFactory
        {
            private Dictionary<string, IUser> users = new Dictionary<string, IUser>();
        
            /// <summary>
            /// 获取指定的UserAddress
            /// </summary>
            /// <param name="toUserAddress"></param>
            /// <returns></returns>
            public IUser GetUser(string toUserAddress)
            {
                if (!users.ContainsKey(toUserAddress))
                {
                    UserInfo user = new UserInfo
                    {
                        ToUserAddress = toUserAddress,
                        NickName = UtilTools.SanitizeEmailToLocalPart(toUserAddress)
                    };
                    users[toUserAddress] = user;
                }
                return users[toUserAddress];
            }
            
            /// <summary>
            /// 返回所有的UserAddress
            /// </summary>
            /// <returns></returns>
            public List<UserInfo> GetAllUser()
            {
                return users.Values.Cast<UserInfo>().ToList();
            }
        }
    }
}