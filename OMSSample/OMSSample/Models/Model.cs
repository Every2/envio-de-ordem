namespace OMSSample.Models
{
    public class Model
    {
        private Dictionary<string, List<OmsSample>?> _database = new();

        private void ShowFieldsAdded(string clOrdId)
        {
            if (_database.TryGetValue(clOrdId, out List<OmsSample>? orderList))
            {
                Console.WriteLine($"Added fields '{clOrdId}':");
                if (orderList != null)
                    foreach (var order in orderList)
                    {
                        Console.WriteLine(
                            $"OrderSymbol: {order.OrderSymbol}, Price: {order.Price}, OrderAmount: {order.OrderAmount}");
                    }
            }
            else
            {
                Console.WriteLine($"No order found to ClOrdID '{clOrdId}'.");
            }
        }
        public void AddToDb(string? key, List<OmsSample>? list)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (_database.ContainsKey(key))
            {
                throw new ArgumentException($"Key already exist {nameof(key)}");
            } 

            _database.Add(key, list);
            ShowFieldsAdded(key);
        }

        public bool ContainsKey(string? key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _database.ContainsKey(key);
        }

        
    }
}