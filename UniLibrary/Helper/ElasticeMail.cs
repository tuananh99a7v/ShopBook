using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace UniLibrary.Helper
{
    public class ElasticeMail
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool Send(string emailTo, string emailCc, string subject, string content, string emailFrom = "mail@loga.vn", string nameFrom = "LogaVN - Mang tri thức đến từng ô cửa")
        {
            if (!IsValidEmail(emailFrom)) return false;
            NameValueCollection values = new NameValueCollection
            {
                {"apikey", "C0A9D28CC209426463954EE5A3D1464A2A968975ABB479C4F8AD0CFDBBD891856DC386F4B917A2A7C53D25D099A056D2"},
                {"from", emailFrom},
                {"fromName", nameFrom},
                {"to", emailTo},
                {"subject", subject},
                {"bodyHtml", content},
                {"isTransactional", true.ToString()}
            };
            string address = "https://api.elasticemail.com/v2/email/send";
            return JsonConvert.DeserializeObject<ElasticeMailResult>(ReturnSend(address, values)).success == "true";
        }

        private static string ReturnSend(string address, NameValueCollection values)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    byte[] apiResponse = client.UploadValues(address, values);
                    return Encoding.UTF8.GetString(apiResponse);
                }
                catch (Exception ex)
                {
                    return "Exception caught: " + ex.Message + "\n" + ex.StackTrace;
                }
            }
        }
    }

    public class ElasticeMailResult
    {
        public string success { get; set; }
    }
}