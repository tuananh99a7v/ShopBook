using System;
using System.Net;
using System.Net.Mail;

namespace UniLibrary.Helper
{
    public static class Gmail
    {
        /// <summary>
        /// [PhuDX] - [07/05/2018] - [Gửi Gmail]
        /// </summary>
        /// 
        public static bool Send(string emailTo, string emailCc, string subject, string body, string nameFrom)
        {
			try
			{
				using (MailMessage mail = new MailMessage())
				{
					mail.From = new MailAddress("anh30121999it5@gmail.com");
					mail.IsBodyHtml = true;
					emailCc = emailCc.Replace(" ", "").Replace(" ", "");
					emailTo = emailTo.Replace(" ", "").Replace(" ", "");
					if (!string.IsNullOrEmpty(emailCc))
						mail.CC.Add(emailCc);
					mail.To.Add(emailTo);
					mail.Subject = subject;
					mail.Body = body;

					SmtpClient smtpServer = new SmtpClient("anh30121999it5")
					{
						Credentials = new NetworkCredential("anh30121999it5", "MatKhauLan#200"),
						Host = "smtp.gmail.com",
						EnableSsl = true,
						Port = 587,
					};
					smtpServer.Send(mail);
					return true;
				}
			}
			catch (Exception exx)
			{
				string msg = exx.Message;
				return false;
			}
		}
    }
}