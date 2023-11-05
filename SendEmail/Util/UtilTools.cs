using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        /// 选择Txt文本
        /// </summary>
        /// <returns></returns>
        public static string getSelectTxtFile()
        {
            string selectedFile = "";
            OpenFileDialog fileDialog = new OpenFileDialog();

            // 设置过滤器只显示.txt文件
            fileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            fileDialog.Title = "选择一个txt文件";

            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                selectedFile = fileDialog.FileName;
            }

            return selectedFile;
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
                    string filePath = fileInfo.DirectoryName + "\\" + fileName;
                    string fileDirectory = fileInfo.DirectoryName;
                    fileDetailsList.Add(new FileDetails(fileName,fileSize,fileDirectory,filePath,"Ready"));
                }
            }
            return fileDetailsList;
        }
        
        /// <summary>
        /// 获取Txt文本中每一行的数据,并判断是否是邮箱地址
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, HashSet<string>>> ProcessEmailFile(string filePath)
        {
            var dictionary = new Dictionary<string, Dictionary<string, HashSet<string>>>();
            foreach (var lineWithIndex in File.ReadLines(filePath).Select((line, index) => new { line, lineNumber = index + 1 }))
            {
                string line = lineWithIndex.line; //当前内容
                string lineNumberKey = lineWithIndex.lineNumber.ToString("D8"); //当前行号格式化为8位数

                // 如果字典中没有这个键，则添加
                if (!dictionary.ContainsKey(lineNumberKey))
                {
                    dictionary[lineNumberKey] = new Dictionary<string, HashSet<string>>
                    {
                        ["toAddress"] = new HashSet<string>(),
                        ["ccAddress"] = new HashSet<string>(),
                        ["invalidAddress"] = new HashSet<string>(),
                        ["titleStr"] = new HashSet<string>(),
                        ["messageInfo"] = new HashSet<string>()
                    };
                }
                var parts = line.Split(new string[] { "----" }, StringSplitOptions.None);
                if (parts.Length == 4)
                {
                    var recipients = parts[0].Split(';');
                    foreach (var recipient in recipients)
                    {
                        if (IsValidEmail(recipient))
                        {
                            dictionary[lineNumberKey]["toAddress"].Add(recipient);
                        }
                        else
                        {
                            dictionary[lineNumberKey]["invalidAddress"].Add(recipient);
                        }
                    }

                    var ccs = parts[1].Split(';');
                    foreach (var cc in ccs)
                    {
                        if (IsValidEmail(cc))
                        {
                            dictionary[lineNumberKey]["ccAddress"].Add(cc);
                        }
                        else
                        {
                            dictionary[lineNumberKey]["invalidAddress"].Add(cc);
                        }
                    }

                    var titleStr = parts[2];
                    if (!string.IsNullOrEmpty(titleStr))
                    {
                        dictionary[lineNumberKey]["titleStr"].Add(titleStr);
                    }
                    
                    var messageInfoStr = parts[3];
                    if (!string.IsNullOrEmpty(messageInfoStr))
                    {
                        dictionary[lineNumberKey]["messageInfo"].Add(messageInfoStr);
                    }
                }
            }
            return dictionary;
        }
        
        /// <summary>
        /// 启用或禁用当前页面（或窗体）上的所有控件
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="enabled"></param>
        public static void SetAllControlsEnabled(Control parent, bool enabled)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Enabled = enabled;

                // 如果控件包含子控件，递归调用
                if (ctrl.Controls.Count > 0)
                    SetAllControlsEnabled(ctrl, enabled);
            }
        }

        private static readonly char[] InvalidChars = new char[] { '"', ':', '<', '>', '|', '*', '?', '\\', '/', '\0' };
        /// <summary>
        /// 清除邮件用户名中的无效字符,使用下划线替代
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string SanitizeEmailToLocalPart(string email)
        {
            string localPart = email.Split('@')[0];
            foreach (var invalidChar in InvalidChars)
            {
                localPart = localPart.Replace(invalidChar.ToString(), "_");  // 使用下划线替换无效字符
            }
            return localPart;
        }
        public static string SanitizeEmailToLocalPart(HashSet<string> emailSet)
        {
            var stringBuilder = new StringBuilder();
            foreach (var email in emailSet)
            {
                string localPart = email.Split('@')[0];
                foreach (var invalidChar in InvalidChars)
                {
                    localPart = localPart.Replace(invalidChar.ToString(), "_");  // 使用下划线替换无效字符
                }
                stringBuilder.Append(localPart + "/");
            }
            var emailStr = stringBuilder.ToString();
            emailStr = emailStr.Substring(0, emailStr.Length - 1);
            return emailStr;
        }
        
        /// <summary>
        /// 创建子文件目录
        /// </summary>
        /// <param name="parentDirectory"></param>
        /// <param name="subfolders"></param>
        public static string CreateDirectoriesForSubfolders(string parentDirectory,string subfolders)
        {
            var directoryPath = Path.Combine(parentDirectory, subfolders);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            return directoryPath;
        }
        
        /// <summary>
        /// 移动文件到指定目录
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationDirectory"></param>
        public static bool MoveFile(string sourceFilePath, string destinationDirectory)
        {
            if (File.Exists(sourceFilePath))
            {
                // Ensure the target directory exists
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }
                string destinationFilePath = Path.Combine(destinationDirectory, Path.GetFileName(sourceFilePath));
                if (!File.Exists(destinationFilePath))
                {
                    File.Copy(sourceFilePath, destinationFilePath);
                }
                return true;
            }
            else
            {
                Console.WriteLine($"Source file '{sourceFilePath}' does not exist.");
            }

            return false;
        }

        /// <summary>
        /// 校验List是否为空
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool checkListOrSetIsNull<T>(List<T> list)
        {
            if (list is null || list.Count == 0)
            {
                return true;
            }
            return false;
        }
        
        public static bool checkListOrSetIsNull<T>(HashSet<T> list)
        {
            if (list is null || list.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}