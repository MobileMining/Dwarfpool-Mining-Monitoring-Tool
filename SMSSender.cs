/**
 * -----Dwarfpool Mining Monitoring Tool-----
 * Class: SMSSender
 * Description: A class that can be used to send
 * an SMS to a given phone number from the Twilio
 * phone number used to send notifications from 
 * this application. This class makes use of the
 * Twilio library.
 * 
 * Author: David Goguen
 * Email: david.goguen@outlook.com
 */

using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Dwarfpool_Mining_Monitoring_Tool
{
    public class SMSSender
    {

        // Stores Twilio SID we are using to send text messages
        private const string sid = "ACb028786f651f30a3dc76c7a4194b0a8b";

        // Stores Twilio token we are using to send text messages
        private const string token = "51a797a47729ebeba15d6771db35dd2c";

        // Stores message we are sending
        private string message;

        // Stores the number we are sending the message to
        private string number;

        public SMSSender(string number, string message)
        {

            this.message = message;
            this.number = number;

        }

        /*
         * Sends the SMS specified in the constructor of SMSSender.
         */
        public void send()
        {

            TwilioClient.Init(sid, token);

            PhoneNumber numberTo = new PhoneNumber(number);

            // Phone number we are using to send messages
            PhoneNumber numberFrom = new PhoneNumber("+16476944123");

            MessageResource.Create(to: numberTo, from: numberFrom, body: message);

        }
    }
}
