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
