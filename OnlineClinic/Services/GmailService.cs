using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OnlineClinic.Services
{
    public class GmailService : IMail
    {        
        public bool SendEmail(string recipient, string subject, string body)
        {            
            MailMessage mailMessage = new MailMessage("hmwe98@gmail.com", recipient);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);//465
            smtpClient.EnableSsl = true;
            
            smtpClient.Credentials = new NetworkCredential("hmwe98@gmail.com", "HmU0020599463");
            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
