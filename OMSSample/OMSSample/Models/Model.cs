using System.Collections.Generic;

namespace OMSSample.Models
{
    public class Model
    {
        private Dictionary<string, List<OMSSample>> _database;

        public Model()
        {
            _database = new Dictionary<string, List<OMSSample>>();
        }

        public void AddToDb(string clOrdId, List<OMSSample> list)
        {
            _database.Add(clOrdId, list);
        }

        public bool Delete(string key)
        {
            return _database.Remove(key);
        }

        public List<OMSSample>? GetFromDb(string clOrdId)
        {
            if (_database.TryGetValue(clOrdId, out List<OMSSample> value))
            {
                return value;
            }
            return null;
        }
    }
    
}