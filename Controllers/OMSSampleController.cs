using Microsoft.AspNetCore.Mvc;
using QuickFix;
using QuickFix.Fields;
using OMSSample.Models;
using QuickFix.FIX44;
using QuickFix.Transport;
using Message = QuickFix.Message;

namespace OMSSample.Controllers;

[ApiController]
[Route("v1/")]
public class OmsSampleController : ControllerBase, IDisposable
{
    private readonly SocketInitiator _initiator;
    private readonly Context _context;
    public OmsSampleController(Context context)
    {
        _context = context;
        var settings =
            new SessionSettings(
                "quickfix.cfg");
        var storeFactory = new FileStoreFactory(settings);
        var logFactory = new FileLogFactory(settings);
        var messageFactory = new DefaultMessageFactory();
        _initiator = new SocketInitiator(new ConnectionHandler(), storeFactory, settings, logFactory,
            messageFactory);
        _initiator.Start();
        _initiator.GetSessionIDs();
    }

    [HttpPost]
    [Route("sendNewOrder")]
    public ActionResult SendNewOrder([FromBody] OmsSample fields)
    {
        try
        {
            var newOrderSingle = new NewOrderSingle(
                new ClOrdID(Guid.NewGuid().ToString()),
                new Symbol(fields.OrderSymbol),
                new Side(Side.BUY),
                new TransactTime(DateTime.UtcNow),
                new OrdType(OrdType.LIMIT)
            );

            newOrderSingle.Set(new Price(fields.Price));
            newOrderSingle.Set(new OrderQty(fields.OrderAmount));
            var sessionid = new SessionID("FIX.4.4", "CLIENT1", "EXECUTOR");
            Session.SendToTarget(newOrderSingle, sessionid);
            var orderSingle = new OrderSingle()
            {
                ClOrdId = newOrderSingle.ClOrdID.getValue(),
                Symbol = newOrderSingle.Symbol.getValue(),
                Side = newOrderSingle.Side.getValue(),
                TransactTime = newOrderSingle.TransactTime.getValue(),
                OrdType = newOrderSingle.OrdType.getValue(),
                Price = newOrderSingle.Price.getValue(),
                OrdQty = (uint)newOrderSingle.OrderQty.getValue()
            };
            _context.Add(orderSingle);
          
            _context.SaveChanges();
            return Ok(new { success = true, description = "Order placed successfully" });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { success = false, description = e.Message });
        }
    }

    private class ConnectionHandler : MessageCracker, IApplication
    {
        
        public void FromAdmin(Message message, SessionID sessionId)
        {
            Crack(message, sessionId);
        }

        public void ToAdmin(Message message, SessionID sessionId)
        {
            OnLogon(sessionId);
        }

        public void FromApp(Message message, SessionID sessionId)
        {
            Console.WriteLine($"In: {message}");
            try
            {
                Crack(message, sessionId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
        
        private static void SendLogonMessage(SessionID sessionId)
        {
            
            var logon = new Logon(
                new EncryptMethod(0),
                new HeartBtInt(30)
            );
            
            Session.SendToTarget(logon, sessionId);
        }

        public void ToApp(Message message, SessionID sessionId)
        {
            try
            {
                bool possDupFlag = false;
                if (message.Header.IsSetField(Tags.PossDupFlag))
                {
                    possDupFlag = QuickFix.Fields.Converters.BoolConverter.Convert(
                        message.Header.GetString(Tags.PossDupFlag));
                }

                if (possDupFlag)
                {
                    throw new DoNotSend();
                }
            }
            catch (FieldNotFoundException)
            {
            }

            Console.WriteLine();
            Console.WriteLine($"OUT: {message}");
        }

        public void OnCreate(SessionID sessionId)
        {
            SendLogonMessage(sessionId);
            Session.LookupSession(sessionId);
        }

        public void OnLogout(SessionID sessionId)
        {
            Console.WriteLine($"Logout - {sessionId}");
        }

        public void OnLogon(SessionID sessionId)
        {
            Console.WriteLine($"Logon - {sessionId}");
        }
        
    }

    public void Dispose()
    {
        _initiator.Stop();
        _initiator.Dispose();
    }
}