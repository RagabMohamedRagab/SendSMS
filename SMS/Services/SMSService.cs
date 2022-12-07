using Microsoft.Extensions.Options;
using SendSMSWithTwilio.Helpers;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SendSMSWithTwilio.Services {
    public class SMSService: ISMSService {
        private readonly TwilioSetting _twilio;

        public SMSService(IOptions<TwilioSetting> twilio)
        {
            _twilio = twilio.Value;
        }

        public MessageResource SendSMS(string Mobile, string body)
        {
            TwilioClient.Init(_twilio.AccountSID, _twilio.AuthToken);
            var result = MessageResource.Create(
                  body:body,
                  from:new Twilio.Types.PhoneNumber(_twilio.TwilioPhoneNumber),
                  to:Mobile
                );
            return result;
        }
    }
}
