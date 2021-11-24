using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace UniLibrary.Helper
{
    public static class TextHelper
    {
        /// <summary>
        /// Lấy ra chuỗi ngẫu nhiên
        /// </summary>
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool NullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        /// <summary>
        /// Lấy ra chuỗi ngẫu nhiên
        /// </summary>
        public static string RandomStringNotNumber(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Lấy ra chuỗi ngẫu nhiên
        /// </summary>
        public static string RandomStringNumber(int length)
        {
            Random random = new Random();
            const string chars = "123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Kiểm tra ký tự tồn tại trong chuỗi cho trước
        /// </summary>
        /// <param name="input">Chuỗi cần check</param>
        /// <param name="check">Chuỗi luật</param>
        /// <returns>true: có tồn tại, false: không tồn tại</returns>
        public static bool CheckAllChar(string input, string check)
        {
            return input.Any(t => check.IndexOf(t) < 0);
        }

        /// <summary>
        /// Xử lý biển số xe
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ExecuteLicensePlate(this string input)
        {
            try
            {
                input = input.ToLower();
                if (string.IsNullOrEmpty(input)) return "";
                input = Regex.Replace(input, "\\W+", "");
                input = Regex.Replace(input, " ", "");
                return input;
            }
            catch
            {
                return input;
            }
        }

        public static string Utf8Convert(this string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input)) return "";
                Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string temp = input.Normalize(NormalizationForm.FormD);
                input = regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
                return input;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Xóa định dạng Font
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StripFont(this string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            input = Regex.Replace(input, $@"font-size:\s*(\d+(\.\d+)*)(pt|px)\s*;*", "");
            input = Regex.Replace(input, "(\\w+\\-)*font\\-family:(\\s*)((\'|(\\&quot\\;))?(\\w+(\\s*)|\\-)+(\'|(\\&quot\\;))?\\s*\\,*\\s*)+(\\;*)", "");
            input = Regex.Replace(input, "(\\w+\\-)*font\\-family:(\\s*)(\"(\\w+(\\s*)|\\-)+\"\\s*\\,*\\s*)+(\\;*)", "");
            input = Regex.Replace(input, "((\\w+\\-)*font\\-family:)", "");
            return input;
        }

        /// <summary>
        /// Lấy ra HTML
        /// </summary>
        public static string ToStringPageSize(this int pageSize)
        {
            StringBuilder htmlText = new StringBuilder();
            if (pageSize == 10)
            {
                htmlText.AppendLine("<option value=\"10\" selected=\"selected\">10</option>");
                htmlText.AppendLine("<option value=\"20\">20</option>");
                htmlText.AppendLine("<option value=\"50\">50</option>");
                htmlText.AppendLine("<option value=\"100\">100</option>");
                htmlText.AppendLine("<option value=\"150\">150</option>");
            }
            else if (pageSize == 20)
            {
                htmlText.AppendLine("<option value=\"10\">10</option>");
                htmlText.AppendLine("<option value=\"20\" selected=\"selected\">20</option>");
                htmlText.AppendLine("<option value=\"50\">50</option>");
                htmlText.AppendLine("<option value=\"100\">100</option>");
                htmlText.AppendLine("<option value=\"150\">150</option>");
            }
            else if (pageSize == 50)
            {
                htmlText.AppendLine("<option value=\"10\">10</option>");
                htmlText.AppendLine("<option value=\"20\">20</option>");
                htmlText.AppendLine("<option value=\"50\" selected=\"selected\">50</option>");
                htmlText.AppendLine("<option value=\"100\">100</option>");
                htmlText.AppendLine("<option value=\"150\">150</option>");
            }
            else if (pageSize == 100)
            {
                htmlText.AppendLine("<option value=\"10\">10</option>");
                htmlText.AppendLine("<option value=\"20\">20</option>");
                htmlText.AppendLine("<option value=\"50\">50</option>");
                htmlText.AppendLine("<option value=\"100\" selected=\"selected\">100</option>");
                htmlText.AppendLine("<option value=\"150\">150</option>");
            }
            else if (pageSize == 150)
            {
                htmlText.AppendLine("<option value=\"10\">10</option>");
                htmlText.AppendLine("<option value=\"20\">20</option>");
                htmlText.AppendLine("<option value=\"50\">50</option>");
                htmlText.AppendLine("<option value=\"100\">100</option>");
                htmlText.AppendLine("<option value=\"150\" selected=\"selected\">150</option>");
            }
            else
            {
                htmlText.AppendLine("<option value=\"10\">10</option>");
                htmlText.AppendLine("<option value=\"20\">20</option>");
                htmlText.AppendLine("<option value=\"50\">50</option>");
                htmlText.AppendLine("<option value=\"100\">100</option>");
                htmlText.AppendLine("<option value=\"" + pageSize + "\" selected=\"selected\">" + pageSize + "</option>");
            }
            return htmlText.ToString();
        }

        /// <summary>
        /// Làm sạch text, xóa nhiều khoảng trắng
        /// </summary>
        public static string ClearText(this string input)
        {
            while (input.Contains("...."))
            {
                input = input.Replace("....", "-");
            }
            while (input.Contains("---"))
            {
                input = input.Replace("---", "=");
            }
            input = Regex.Replace(input, @"(&nbsp;)+", " ");
            input = input.Replace(@"\n", "");
            input = Regex.Replace(input, @"\s+", " ");
            return input.DecodeHTML().Trim();
        }

        /// <summary>
        /// Decode HTML
        /// </summary>
        public static string DecodeHTML(this string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return HttpUtility.HtmlDecode(input);
        }

        /// <summary>
        /// Thực hiện EncodeBase64
        /// </summary>
        public static string EncodeBase64(this string plainText)
        {
            try
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(plainTextBytes);
            }
            catch
            {
                return "";
            }
        }

        /// <authors>
        /// Thực hiện DecodeBase64
        /// </authors>
        public static string DecodeBase64(this string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Xóa ký tự đặc biệt
        /// </summary>
        public static string RemoveSpecialChar(this string input)
        {
            try
            {
                input = Regex.Replace(input, @"\W+", " ");
                return input;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Xóa ký tự đặc biệt
        /// </summary>
        public static string CreateNickNameRandom()
        {
            try
            {
                string[] myName = new string[] { "Cà rốt", "Táo Tàu", "Su hào", "Bắp cải", "Cà chua", "Bí ngô", "Cu Tí", "Cu Tèo", "Nhỏ Xuka", "Thằng Tũn", "Thằng Tít", "Thằng Bờm", "Shin", "Tẹt", "Mén", "Jerry", "Cái Bống" };
                Random rnd = new Random();
                return myName[rnd.Next(1, myName.Length)];
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Decode chuỗi URL
        /// </summary>
        public static string DecodeURL(this string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return HttpUtility.UrlDecode(input);
        }

        /// <summary>
        /// Encode chuỗi URL
        /// </summary>
        public static string EncodeURL(this string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return HttpUtility.UrlEncode(input);
        }

        /// <summary>
        /// Thực hiện Md5
        /// </summary>
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string Md5Hash(this string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        /***********Encrypt - Decrypt*****************/

        public static string EncryptString(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return "";
            try
            {
                var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
                var passwordBytes = Encoding.UTF8.GetBytes("phudev1996");
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);
                return Convert.ToBase64String(bytesEncrypted);
            }
            catch
            {
                return "";
            }
        }

        public static string DecryptString(this string encryptedText)
        {
            try
            {
                if (string.IsNullOrEmpty(encryptedText)) return "";
                var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
                var passwordBytes = Encoding.UTF8.GetBytes("phudev1996");
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);
                return Encoding.UTF8.GetString(bytesDecrypted);
            }
            catch
            {
                return "";
            }
        }

        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            try
            {
                byte[] decryptedBytes;
                var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                        AES.KeySize = 256;
                        AES.BlockSize = 128;
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);
                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }

                        decryptedBytes = ms.ToArray();
                    }
                }

                return decryptedBytes;
            }
            catch
            {
                throw;
            }
        }

        //GUID
        public static string StringToGuid(string guid, out Guid value)
        {
            return ObjectToGuid(guid, out value);
        }

        public static string ReadMoneyToText(string str)
        {
            try
            {
                str = str.Replace("-", "");
                string[] word = { "", " Một", " Hai", " Ba", " Bốn", " Năm", " Sáu", " Bẩy", " Tám", " Chín" };
                string[] million = { "", " Mươi", " Trăm", "" };
                string[] billion = { "", "", "", " Nghìn", "", "", " Triệu", "", "" };
                string result = "{0}";
                int count = 0;
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    if (count > 0 && count % 9 == 0)
                        result = string.Format(result, "{0} Tỷ");
                    if (!(count < str.Length - 3 && count > 2 && str[i].Equals('0') && str[i - 1].Equals('0') && str[i - 2].Equals('0')))
                        result = string.Format(result, "{0}" + billion[count % 9]);
                    if (!str[i].Equals('0'))
                        result = string.Format(result, "{0}" + million[count % 3]);
                    else if (count % 3 == 1 && count > 1 && !str[i - 1].Equals('0') && !str[i + 1].Equals('0'))
                        result = string.Format(result, "{0} Lẻ");
                    var num = Convert.ToInt16(str[i].ToString());
                    result = string.Format(result, "{0}" + word[num]);
                    count++;
                }
                result = result.Replace("{0}", "");
                result = result.Replace("Một Mươi", "Mười");
                return result.Trim() + " Đồng";
            }
            catch
            {
                return "";
            }
        }

        public static string ObjectToGuid(object guid, out Guid value)
        {
            value = Guid.Empty;
            try
            {
                value = new Guid(guid.ToString());
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return "";
        }

        public static string HashSha1(string input)
        {
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }

        public static string FirstCharToUpper(string input)
        {
            try
            {
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
            }
            catch
            {
                return input;
            }
        }

        public static string HexDecode(this string hex)
        {
            hex = hex.Replace("\\x22", "\"");
            hex = hex.Replace("\\x7b", "{");
            hex = hex.Replace("\\x5b", "[");
            hex = hex.Replace("\\x7d", "}");
            hex = hex.Replace("\\x5d", "]");
            hex = hex.Replace("\\x3d", "=");
            return hex;
        }

        public static byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        public static string HextoString(this string inputText)
        {
            byte[] bb = Enumerable.Range(0, inputText.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(inputText.Substring(x, 2), 16))
                .ToArray();
            //return Convert.ToBase64String(bb);
            char[] chars = new char[bb.Length / sizeof(char)];
            System.Buffer.BlockCopy(bb, 0, chars, 0, bb.Length);
            return new string(chars);
        }

        public static string EncodeSHA(string pass)

        {

            SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();

            byte[] bs = System.Text.Encoding.UTF8.GetBytes(pass);

            bs = sha.ComputeHash(bs);

            System.Text.StringBuilder s = new System.Text.StringBuilder();

            foreach (byte b in bs)

            {

                s.Append(b.ToString("x1").ToLower());

            }

            pass = s.ToString();

            return pass;

        }
    }
}