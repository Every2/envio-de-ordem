using Microsoft.AspNetCore.Mvc;

namespace OMSSample.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class OMSSampleController : ControllerBase
{
    [HttpPost]
    public ActionResult<OMSSample> postRequest()
    {
        return Ok();
    }
}

