using System.Collections.Generic;

namespace OMSSample.Models
{
    public class Model
    {
        private Dictionary<string, List<OmsSample>> _database;
        private List<OmsSample> _value;

        public Model()
        {
            _database = new Dictionary<string, List<OmsSample>>();
        }

        public void AddToDb(string key, List<OmsSample> list)
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

        public List<OmsSample>? GetFromDb(string key)
        {
            return _database.TryGetValue(key, out List<OmsSample> value) ? value : null;
        }

        
    }
    
}