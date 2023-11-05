using System;

namespace SendEmail.Util
{
    public class MagicVariable
    {
        public static string ReplaceMagicValues(string input, string fileName, string number, string userName)
        {
            if (string.IsNullOrEmpty(input)) return input;
            input = ReplaceDateMagicValue(input);
            if (!string.IsNullOrEmpty(fileName)) input = ReplaceFileNameMagicValue(input, fileName);
            if (!string.IsNullOrEmpty(number)) input = ReplaceNumberMagicValue(input, number);
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

        private static string ReplaceNumberMagicValue(string input, string number)
        {
            return input.Replace("{number}", number);
        }

        private static string ReplaceUserNameMagicValue(string input, string userName)
        {
            return input.Replace("{userName}", userName);
        }
    }
}