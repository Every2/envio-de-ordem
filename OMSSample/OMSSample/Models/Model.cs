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

        public void AddToDb(string key, List<OMSSample> list)
        {
            _database.Add(key, list);
        }

        public bool ContainsKey(string key)
        {
            return _database.ContainsKey(key);
        }
        public bool Delete(string key)
        {
            return _database.Remove(key);
        }

        public List<OMSSample>? GetFromDb(string key)
        {
            return _database.TryGetValue(key, out List<OMSSample> value) ? value : null;
        }
    }
    
}