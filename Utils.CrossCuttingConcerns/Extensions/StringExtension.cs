using System.Text;
using System.Text.RegularExpressions;

namespace Utils.CrossCuttingConcerns.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Compare string with ignoring invariant case sensitive.
        /// </summary>
        public static bool EqualsInvariant(this string s, string value)
        {
            return !string.IsNullOrEmpty(s) && s.Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Find keywords in a string with ignoring invariant case sensitive.
        /// </summary>
        public static bool ContainKeyWordInvariant(this string text, string keyword)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            return text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Generate MD5
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertToMD5(this string text)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(text);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Check is phone number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(this string phoneNumber)
        {
            return Regex.Match(phoneNumber, @"^(\+[0-9]{9})$").Success;
        }

        /// <summary>
        /// Check is email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
