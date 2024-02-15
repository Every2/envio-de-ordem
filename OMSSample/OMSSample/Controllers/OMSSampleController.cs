using Microsoft.AspNetCore.Mvc;
using OMSSample.Models;
using QuickFix;
using QuickFix.Fields;

namespace OMSSample.Controllers;

[Route("v1/")]
[ApiController]
public class OmsSampleController : ControllerBase
{
    private Model _model = new Model();

    [HttpPost("sendNewOrder")]
    public ActionResult<OmsSample> SendNewOrder(string symbol, uint amount, decimal price)
    {
        
        return Ok();
    }
}