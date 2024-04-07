using System;

namespace SendEmail.Util
{
    public class MagicVariable
    {
        public static string ReplaceMagicValues(string input, string fileName, string countNumber, string indexNumber, string userName)
        {
            if (string.IsNullOrEmpty(input)) return input;
            input = ReplaceDateMagicValue(input);
            if (!string.IsNullOrEmpty(fileName)) input = ReplaceFileNameMagicValue(input, fileName);
            if (!string.IsNullOrEmpty(countNumber)) input = ReplaceCountMagicValue(input, countNumber);
            if (!string.IsNullOrEmpty(indexNumber)) input = ReplaceIndexMagicValue(input, indexNumber);
            if (!string.IsNullOrEmpty(userName)) input = ReplaceUserNameMagicValue(input, userName);
            return input;
        }
        
        private static string ReplaceDateMagicValue(string input)
        {
            return input.Replace("{date}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private static string ReplaceFileNameMagicValue(string input, string fileName)
        {
            return input.Replace("{fileName}", fileName);
        }
        
        private static string ReplaceCountMagicValue(string input, string countNumber)
        {
            return input.Replace("{count}", countNumber);
        }

        private static string ReplaceIndexMagicValue(string input, string indexNumber)
        {
            return input.Replace("{number}", indexNumber);
        }

        private static string ReplaceUserNameMagicValue(string input, string userName)
        {
            return input.Replace("{userName}", userName);
        }
    }
}