using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat_Project.Errors;

namespace Talabat_Project.Controllers
{
    [Route("Errors/{Code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : ControllerBase
    {
       public ActionResult Error(int Code)
        {
            return NotFound(new ApiResponse(Code));
        }
    }
}
