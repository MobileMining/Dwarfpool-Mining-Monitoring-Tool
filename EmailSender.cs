using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Dwarfpool_Mining_Monitoring_Tool
{
    public class EmailSender
    {

        private const string emailAddress = "dwarfpoolminingmonitoringtool@gmail.com";
        private const string emailPassword = "h/C?#=+Y9m9~9aLU";
        private SmtpClient client;
        private MailAddress fromAddress;

        public EmailSender()
        {

            client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            fromAddress = new MailAddress(emailAddress, "Dwarfpool Mining Monitoring Tool");
            client.Credentials = new NetworkCredential(fromAddress.Address, emailPassword);

        }

        public void sendEmail(string toAddress, string subject, string message)
        {

            MailAddress mailingTo = new MailAddress(toAddress);

            MailMessage email = new MailMessage(fromAddress, mailingTo);

            email.Subject = subject;
            email.Body = message;

            client.Send(email);

        }

    }
}
