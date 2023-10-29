using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SendEmail.model;

namespace SendEmail.Util
{
    public class UtilTools
    {
        /// <summary>
        /// 校验是否为邮件格式
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            // 使用正则表达式进行邮件格式验证
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        /// <summary>
        /// 校验是否是端口
        /// </summary>
        /// <param name="portStr"></param>
        /// <returns></returns>
        public static bool IsValidPort(string portStr)
        {
            if (int.TryParse(portStr, out int port))
            {
                // 检查端口范围，通常端口号范围在 1 到 65535 之间
                return (port >= 1 && port <= 65535);
            }
            return false;
        }
        
        /// <summary>
        /// 将邮件Set转换成说明性字符串
        /// </summary>
        /// <param name="addresssSet"></param>
        /// <returns></returns>
        public static String formatMailingList(HashSet<String> addresssSet)
        {
            if (addresssSet.Count==0)
            {
                return "";
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var addressStr in addresssSet)
            {
                stringBuilder.Append("\"" + addressStr + "\",");
            }
            var addressListStr = stringBuilder.ToString();
            return addressListStr.Substring(0, addressListStr.Length - 1);
        }
        
        /// <summary>
        /// 选择目录
        /// </summary>
        /// <returns></returns>
        public static String getSelectPath()
        {
            string selectedDirectory = "";
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.Description = "选择一个目录";
            DialogResult result = folderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                selectedDirectory = folderBrowser.SelectedPath;
            }

            return selectedDirectory;
        }

        /// <summary>
        /// 获取指定目录下面的文件对象
        /// </summary>
        /// <param name="selectedDirectory"></param>
        /// <returns></returns>
        public static List<FileDetails> getFileList(string selectedDirectory)
        {
            List<FileDetails> fileDetailsList = new List<FileDetails>();
            if (Directory.Exists(selectedDirectory))
            {
                string[] files = Directory.GetFiles(selectedDirectory);

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    string fileName = fileInfo.Name;
                    long fileSize = fileInfo.Length;
                    fileDetailsList.Add(new FileDetails(fileName,fileSize,"Ready"));
                }
            }
            return fileDetailsList;
        }
    }
}