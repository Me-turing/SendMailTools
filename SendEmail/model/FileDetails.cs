using System;
using Microsoft.VisualBasic.Logging;

namespace SendEmail.model
{
    public class FileDetails
    {
        private String fileName;
        private long fileSize;
        private String filePath;
        private String fileStatus;
        
        public FileDetails(string fileName, long fileSize, string filePath,string status)
        {
            FileName = fileName;
            FileSize = fileSize;
            FileStatus = status;
            FilePath = filePath;
        }

        public string FilePath
        {
            get => filePath;
            set => filePath = value;
        }
        public string FileName
        {
            get => fileName;
            set => fileName = value;
        }

        public long FileSize
        {
            get => fileSize;
            set => fileSize = value;
        }

        public string FileStatus
        {
            get => fileStatus;
            set => fileStatus = value;
        }
    }
}