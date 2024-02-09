using Microsoft.AspNetCore.Mvc;
using OMSSample.Models;

namespace OMSSample.Controllers;

[Route("v1/[controller]")]
[ApiController]
public class OMSSampleController : ControllerBase
{
    private readonly Model<int> _order;
    private OMSSample _sample = new OMSSample();
    [HttpPost("v1/sendNewOrder")]
    public ActionResult<OMSSample> sendNewOrder(string order, int value)
    {
        _order.AddToDb(order, new List<int>(value));
        return CreatedAtAction(new string("post"), new {  });
    }
}

