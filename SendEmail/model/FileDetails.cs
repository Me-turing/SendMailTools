using System;
using Microsoft.VisualBasic.Logging;

namespace SendEmail.model
{
    public class FileDetails
    {
        private String fileName;
        private long fileSize;
        private String fileStatus;

        public FileDetails(string fileName, long fileSize, string status)
        {
            this.fileName = fileName;
            this.fileSize = fileSize;
            fileStatus = status;
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