using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Dwarfpool_Mining_Monitoring_Tool
{
    public class SMSSender
    {

        private const string sid = "ACb028786f651f30a3dc76c7a4194b0a8b";
        private const string token = "51a797a47729ebeba15d6771db35dd2c";
        private string message;
        private string number;

        public SMSSender(string number, string message)
        {

            this.message = message;
            this.number = number;

        }

        public void send()
        {

            TwilioClient.Init(sid, token);

            PhoneNumber numberTo = new PhoneNumber(number);
            PhoneNumber numberFrom = new PhoneNumber("+16476944123");

            MessageResource.Create(to: numberTo, from: numberFrom, body: message);



        }
    }
}
