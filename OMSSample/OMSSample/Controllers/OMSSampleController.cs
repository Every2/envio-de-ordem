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
        var list = new List<OmsSample>
        {
            new OmsSample { OrderAmount = amount, OrderSymbol = symbol, Price = price }
        };
        
        var order = new QuickFix.FIX44.NewOrderSingle(
            new ClOrdID("1"),
            new Symbol(symbol),
            new Side(Side.BUY),
            new TransactTime(DateTime.Now),
            new OrdType(OrdType.LIMIT));

        order.Price = new Price(price);
        order.OrderQty = new OrderQty(amount);

        
        var sessionId = new SessionID("FIX.4.4",  "CLIENT1", "EXECUTOR");
        
        
        _model.AddToDb(order.ClOrdID.ToString(), list);
        Session.SendToTarget(order, sessionId);
        return Ok(list);
    }
}

