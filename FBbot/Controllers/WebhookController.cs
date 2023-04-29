using Microsoft.AspNetCore.Mvc;

namespace FBbot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebhookController : ControllerBase
    {
        private string VerifyToken = Environment.GetEnvironmentVariable("KEY");
        private readonly ILogger<WebhookController> _logger;

        public WebhookController(ILogger<WebhookController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "hub.mode")] string mode,
                                  [FromQuery(Name = "hub.challenge")] int challenge,
                                  [FromQuery(Name = "hub.verify_token")] string verifyToken)
        {
            if (mode == "subscribe" && verifyToken == VerifyToken)
            {
                Console.WriteLine("Verification is ok");
                return Ok(challenge);
                
            }
            else
            {
                Console.WriteLine("Verification is not ok");
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var payload = reader.ReadToEnd();
            }
            return Ok();
        }
    }
}
