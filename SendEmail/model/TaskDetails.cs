using System;
using System.Collections.Generic;
using System.Linq;

namespace SendEmail.model
{
    public class TaskDetails
    {
        private string taskNumber;
        private string taskTitle;
        private int emailCount;
        private string taskSchedule;
        private List<FileDetails> attachmentList = new List<FileDetails>(); //附件列表
        private MessageInfo messageInfo = null; 

        private TaskDetails()
        {
        }
        
        private TaskDetails(string taskNumber,string taskTitle,int emailCount,string taskSchedule,List<FileDetails> attachmentList,MessageInfo messageInfo)
        {
            if (!string.IsNullOrEmpty(taskNumber))
            {
                this.taskNumber = taskNumber;
                TaskTitle = taskTitle;
                EmailCount = emailCount;
                TaskSchedule = taskSchedule;
                AttachmentList = attachmentList;
                MessageInfo = messageInfo;
            }
        }
        
        public int EmailCount
        {
            get => emailCount;
            set => emailCount = value;
        }
        public String TaskSchedule
        {
            get => taskSchedule;
            set => taskSchedule = value;
        }
        public string TaskNumber
        {
            get => taskNumber;
        }

        public MessageInfo MessageInfo
        {
            get => messageInfo;
            set => messageInfo = value;
        }
        
        public string TaskTitle
        {
            get => taskTitle;
            set => taskTitle = value;
        }
        
        public List<FileDetails> AttachmentList
        {
            get => attachmentList;
            set => attachmentList = value;
        }
        
        public class TaskFactory
        {
            private static readonly TaskFactory _instance = new TaskFactory();
            private Dictionary<string, TaskDetails> _taskDetailsDict = new Dictionary<string, TaskDetails>();
        
            private TaskFactory() { }
            
            public static TaskFactory Instance
            {
                get { return _instance; }
            }
            
            public TaskDetails GetTaskDetails(string taskNumber)
            {
                if (!_taskDetailsDict.TryGetValue(taskNumber, out TaskDetails taskDetails))
                {
                    taskDetails = new TaskDetails
                    {
                        taskNumber = taskNumber,
                        TaskTitle = "N/A",
                        EmailCount = 0,
                        TaskSchedule = "0.00%",
                        AttachmentList = new List<FileDetails>(),
                        MessageInfo = null
                    };
                    _taskDetailsDict[taskNumber] = taskDetails;
                }
                return taskDetails;
            }

            public List<TaskDetails> GetAllTaskDetails()
            {
                return _taskDetailsDict.Values.ToList();
            }
            
            public bool RemoveTaskDetail(string taskNumber)
            {
                if (_taskDetailsDict.TryGetValue(taskNumber, out TaskDetails taskDetails))
                {
                    return  _taskDetailsDict.Remove(taskNumber);
                }
                return false;
            }
        }
    }
}