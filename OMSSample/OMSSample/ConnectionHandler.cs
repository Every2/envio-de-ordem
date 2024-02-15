using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;
using QuickFix.Transport;
using Message = QuickFix.Message;

namespace OMSSample;

public class ConnectionHandler : MessageCracker, IApplication
{
    private Session? _session;
    
    public void ToAdmin(Message message, SessionID sessionId) {}

    public void FromAdmin(Message message, SessionID sessionId) {}

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
        catch (FieldNotFoundException) {}
        
        Console.WriteLine();
        Console.WriteLine($"OUT: {message}");
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

    public void OnCreate(SessionID sessionId)
    {
        _session = Session.LookupSession(sessionId);
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

public class Socket
{
    private string _filename;
    
    public Socket(string filename)
    {
        _filename = filename;
    }

    
    public NewOrderSingle CreateOrder(string symbol, decimal price, uint amount)
    {
        int randomId = new Random().Next(1, 999);
        NewOrderSingle order = new NewOrderSingle(
            new ClOrdID(randomId.ToString()),
            new Symbol(symbol),
            new Side(Side.BUY),
            new TransactTime(DateTime.Now),
            new OrdType(OrdType.MARKET)
        );
        
        order.Set(new Price(price));
        order.Set(new OrderQty(amount));

        return order;
    }

    public void SendOrder(NewOrderSingle message, SessionID id)
    {
        Session.SendToTarget(message, id);
    }

    public void Start()
    {
        SessionSettings settings = new SessionSettings(this._filename);
        ConnectionHandler app = new ConnectionHandler();
        IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
        ILogFactory logFactory = new ScreenLogFactory(settings);

        SocketInitiator initiator = new SocketInitiator(app, storeFactory, settings, logFactory);

        try
        {
            initiator.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
        finally
        {
            initiator.Stop();
        }

    } 
}