using System.Web.Http;

namespace Bliss.Controllers
{
    public class HealthController : ApiController
    {
        public IHttpActionResult GetHealth()
        {            
            return Ok( new { status = "OK" } );
        }
    }
}
