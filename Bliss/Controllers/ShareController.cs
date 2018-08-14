using System.Text.RegularExpressions;
using System.Web.Http;

namespace Bliss.Controllers
{
    public class ShareController : ApiController
    {
        public IHttpActionResult PostShare(string destination_email, string content_url)
        {
            
                if (!string.IsNullOrEmpty(destination_email)&&!string.IsNullOrEmpty(content_url))
                {
                    Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

                    if (rg.IsMatch(destination_email))
                    {
                        return Ok(new { status = "OK" });
                    }
                    else
                    {
                        return BadRequest("Bad Request. Either destination_email not valid or empty content_url");
                    }
                }
  
                return BadRequest( "Bad Request. Either destination_email not valid or empty content_url" );
            
            
        }
    }
}
