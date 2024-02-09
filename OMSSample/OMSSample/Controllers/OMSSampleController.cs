using Microsoft.AspNetCore.Mvc;
using OMSSample.Models;

namespace OMSSample.Controllers;

[Route("v1/")]
[ApiController]
public class OMSSampleController : ControllerBase
{
    private Model model = new Model();
    
    
    [HttpPost("sendNewOrder")]
    public ActionResult<OMSSample> sendNewOrder(string symbol, uint amount, decimal price)
    {
        var list = new List<OMSSample>
        {
            new OMSSample { orderAmount = amount, orderSymbol = symbol, price = price }
        };
        model.AddToDb("ClOrdID", list);
        
        return Ok(list);
    }
}

