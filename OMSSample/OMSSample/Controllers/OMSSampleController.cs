using Microsoft.AspNetCore.Mvc;
using OMSSample.Models;

namespace OMSSample.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class OMSSampleController : ControllerBase
{
    
    
    [HttpPost("v1/sendNewOrder")]
    public ActionResult<OMSSample> sendNewOrder(string order, int value)
    {
        return Ok();
    }
}

