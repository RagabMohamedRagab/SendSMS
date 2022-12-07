# Send SMS  Messages in .NET

<img src="Help/SMS.jpg" style="width:100%;height:300px"/>

## 1- Create A new Project ASP.Net Core Web API.
> First, you need to create the ASP.Net core web project using the ASP.Net core 5 web API

## 2- AppSettings.json
```bash
 "Twilio": {
    "AccountSID": "xxxxxxxxxxxx",
    "AuthToken": "xxxxxxxxx",
    "TwilioPhoneNumber": "xxxxxxxxxxx"
  }

```
## 3- Class TwilioSetting.cs
```bash
namespace SendSMSWithTwilio.Helpers {
    public class TwilioSetting {
        public string AccountSID { get; set; }
        public string AuthToken { get; set; }
        public string TwilioPhoneNumber { get; set; }
    }
}

```
## 4- Install NuGet Package
```bash
 Install-Package Twilio
```
```bash
using Twilio;
using Twilio.Rest.Api.V2010.Account;
```
## 5- ISMSService.cs
```bash
using Twilio.Rest.Api.V2010.Account;

namespace SendSMSWithTwilio.Services {
    public interface ISMSService {
        MessageResource SendSMS(string Mobile, string body);
    }
}

```
## 6- SMSService.cs
```bash
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

```
## 7 - SMSController 
```bash
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendSMSWithTwilio.Dtos;
using SendSMSWithTwilio.Services;

namespace SendSMSWithTwilio.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SMSController : ControllerBase {
        private readonly ISMSService _sMSService;

        public SMSController(ISMSService sMSService)
        {
            _sMSService = sMSService;
        }
        [HttpPost("Send")]
        public IActionResult SendSMS([FromForm]SendSmsDto model)
        {
            var result = _sMSService.SendSMS(model.Mobile, model.Body);
            if (!String.IsNullOrEmpty(result.ErrorMessage))
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result);
        }
    }
}

```
## 8 - Test 

> Swagger :
 

