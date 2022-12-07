using Twilio.Rest.Api.V2010.Account;

namespace SendSMSWithTwilio.Services {
    public interface ISMSService {
        MessageResource SendSMS(string Mobile, string body);
    }
}
