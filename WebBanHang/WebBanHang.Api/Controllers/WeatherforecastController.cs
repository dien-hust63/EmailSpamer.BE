using Microsoft.AspNetCore.Mvc;

namespace WebBanHang.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeatherforecastController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "test";
        }
    }
}
