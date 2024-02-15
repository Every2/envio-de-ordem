using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;
using QuickFix.Transport;
using Message = QuickFix.Message;

public class MyApp
{
    public static void Main()
    {
        SessionSettings settings = new SessionSettings("C:\\Users\\janelas\\Documents\\projetos\\envio-de-ordem\\OMSSample\\OMSSample\\quickfix.cfg");
        IApplication myApp = new MyQuickFixApp();
        IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
        ILogFactory logFactory = new FileLogFactory(settings);
        SocketInitiator acceptor = new SocketInitiator(
            myApp,
            storeFactory,
            settings,
            logFactory);

        NewOrderSingle order = new NewOrderSingle(
            new ClOrdID("1234"),
            new Symbol("AAPL"),
            new Side(Side.BUY),
            new TransactTime(DateTime.Now),
            new OrdType(OrdType.MARKET));

        order.Price = new Price(new decimal(22.4));
        order.OrderQty = new OrderQty(10);
        order.Account = new Account("18861112");
        var sessionId = new SessionID("FIX.4.4", "CLIENT1", "EXECUTOR");
        Session.SendToTarget(order, sessionId);
        acceptor.Start();
        var a = Session.SendToTarget(order, sessionId);
        if (a)
        {
            Console.WriteLine("rodou");
        }
    }
}




public class MyQuickFixApp : IApplication
{
    public void FromApp(Message msg, SessionID sessionID) { }
    public void OnCreate(SessionID sessionID) { }
    public void OnLogout(SessionID sessionID) { }
    public void OnLogon(SessionID sessionID) { }
    public void FromAdmin(Message msg, SessionID sessionID) { }
    public void ToAdmin(Message msg, SessionID sessionID) { }
    public void ToApp(Message msg, SessionID sessionID) { }
}


