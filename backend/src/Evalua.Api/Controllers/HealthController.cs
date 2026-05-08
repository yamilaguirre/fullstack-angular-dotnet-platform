using Microsoft.AspNetCore.Mvc;

namespace Evalua.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public ActionResult<object> Get()
        => Ok(new { status = "ok", service = "Evalua.Api" });
}
