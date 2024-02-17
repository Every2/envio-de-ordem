using Microsoft.AspNetCore.Mvc;
using QuickFix;
using QuickFix.Fields;
using OMSSample.Models;
using QuickFix.FIX44;
using QuickFix.Transport;
using Message = QuickFix.Message;

namespace OMSSample.Controllers
{
    [ApiController]
    [Route("v1/")]
    public class OmsSampleController : ControllerBase
    {
        public OmsSampleController()
        {
            var settings =
                new SessionSettings(
                    "quickfix.cfg");
            var storeFactory = new FileStoreFactory(settings);
            var logFactory = new FileLogFactory(settings);
            var messageFactory = new DefaultMessageFactory();
            var initiator = new SocketInitiator(new ConnectionHandler(), storeFactory, settings, logFactory,
                messageFactory);
            initiator.Start();
            initiator.GetSessionIDs();
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
                    new OrdType(OrdType.MARKET)
                );

                newOrderSingle.Set(new Price(fields.Price));
                newOrderSingle.Set(new OrderQty(fields.OrderAmount));
                newOrderSingle.Set(new NoPartyIDs(0));
                var sessionid = new SessionID("FIX.4.4", "CLIENT1", "EXECUTOR");
                Session.SendToTarget(newOrderSingle, sessionid);

                return Ok(new { success = true, description = "Order placed successfully" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { success = false, description = e.Message });
            }
        }

        private class ConnectionHandler : MessageCracker, IApplication
        {
            private Model _model = new Model();

            public void FromAdmin(Message message, SessionID sessionId)
            {
            }

            public void ToAdmin(Message message, SessionID sessionId)
            {
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
                    Console.WriteLine(e);
                    Console.WriteLine(e.StackTrace);
                }
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

            public void OnMessage(ExecutionReport message, SessionID sessionId)
            {
                try
                {
                    var clOrdId = message.IsSetClOrdID() ? message.ClOrdID.getValue() : string.Empty;
                    if (!_model.ContainsKey(clOrdId))
                    {
                        _model.AddToDb(clOrdId, new List<OmsSample>());
                    }

                    _model.AddToDb(clOrdId, new List<OmsSample>()
                    {
                        new()
                        {
                            OrderSymbol = message.IsSetSymbol() ? message.Symbol.getValue() : string.Empty,
                            OrderAmount = (uint)(message.IsSetOrderQty() ? message.OrderQty.getValue() : 0),
                            Price = message.IsSetPrice() ? message.Price.getValue() : 0
                        }
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error processing execution report: {e.Message}");
                }
            }
        }
    }
}