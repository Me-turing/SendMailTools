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
        private MailMessage toUserMessage;
        private List<FileDetails> FileDetailsList;
        public string ToUserAddress { get; set; }
        
        public MailMessage ToUserMessage
        {
            get => toUserMessage;
            set => toUserMessage = value;
        }

        public List<FileDetails> FileDetailsList1
        {
            get => FileDetailsList;
            set => FileDetailsList = value;
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
                    UserInfo user = new UserInfo { ToUserAddress = toUserAddress };
                    users[toUserAddress] = user;
                }
                return users[toUserAddress];
            }
            
            /// <summary>
            /// 返回所有的UserAddress
            /// </summary>
            /// <returns></returns>
            public IReadOnlyList<IUser> GetAllUser()
            {
                return users.Values.ToList().AsReadOnly();
            }
        }
    }
}