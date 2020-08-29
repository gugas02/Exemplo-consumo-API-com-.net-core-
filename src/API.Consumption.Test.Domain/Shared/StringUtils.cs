using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace API.Consumption.Test.Domain.Shared
{
    public static class StringUtils
    {
        public static string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            //value += "|54be1d80-b6d0-45c0-b8d7-13b3c798729f";
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder sbString = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sbString.Append(data[i].ToString("x2"));
            return sbString.ToString();
        }

        public static string UrlEncode(string unencodedString)
        {
            var encodedString = new StringBuilder();
            var unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

            foreach (char symbol in unencodedString)
                if (unreservedChars.IndexOf(symbol) != -1)
                    encodedString.Append(symbol);
                else
                    encodedString.Append('%' + string.Format("{0:X2}", (int)symbol));

            return encodedString.ToString();
        }

        public static string EncryptSha1(string text, string key)
        {
            var hmac = new HMACSHA1(Encoding.ASCII.GetBytes(key));
            return Convert.ToBase64String(hmac.ComputeHash(new ASCIIEncoding().GetBytes(text)));
        }

        public static string RandomString(int tamanho)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public static string RemoveSpecialCharacters(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            var str = sbReturn.ToString().Replace("-", "");
            return Regex.Replace(sbReturn.ToString(), "[^0-9a-zA-Z]+", "");
        }

        public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => input.First().ToString().ToUpper() + input.Substring(1)
        };

        public static string FirstCharToUpperAllWords(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            string[] exceptions = { "da", "das", "de", "do", "dos" };
            var output = "";
            var words = input.ToLower().Split(" ");
            foreach (var word in words)
            {
                if (word.Trim().Length > 0)
                {
                    if (exceptions.Contains(word.Trim()))
                        output += word.Trim() + " ";
                    else
                        output += word.Trim().First().ToString().ToUpper() + word.Trim().Substring(1) + " ";
                }
            }

            return output.TrimEnd();
        }
    }
}
