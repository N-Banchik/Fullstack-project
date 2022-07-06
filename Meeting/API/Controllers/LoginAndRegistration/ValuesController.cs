using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.LoginAndRegistration
{
    
    public class ValuesController : BaseApiController
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Hello World!");
        }
    }
}
