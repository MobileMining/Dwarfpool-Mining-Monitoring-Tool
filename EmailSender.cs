/**
 * -----Dwarfpool Mining Monitoring Tool-----
 * Class: EmailSender
 * Description: A class that can be used to send
 * an email to a given address from the Gmail address
 * used to send notifications from this application.
 * 
 * Author: David Goguen
 * Email: david.goguen@outlook.com
 */

using System.Net;
using System.Net.Mail;

namespace Dwarfpool_Mining_Monitoring_Tool
{
    public class EmailSender
    {

        // Stores email address this application sends from
        private const string emailAddress = "dwarfpoolminingmonitoringtool@gmail.com";

        // Stores password of the above email address
        private const string emailPassword = "h/C?#=+Y9m9~9aLU";

        // SMTP client used to connect to email service
        private SmtpClient client;

        // Store information about the address we are sending from
        private MailAddress fromAddress;

        public EmailSender()
        {

            // Set up EmailSender to send from Gmail account above
            client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            fromAddress = new MailAddress(emailAddress, "Dwarfpool Mining Monitoring Tool");
            client.Credentials = new NetworkCredential(fromAddress.Address, emailPassword);

        }

        /*
         * Sends an email from gmail account above to the specified
         * address with the specified subject and message.
         */
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
