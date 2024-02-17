namespace OMSSample.Models
{
    public class Model
    {
        private readonly Dictionary<string, List<OmsSample>?> _database = new();

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

            _database[key] = list ?? throw new ArgumentNullException(nameof(list));

        }

        public bool ContainsKey(string? key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _database.ContainsKey(key);
        }

        public bool Delete(string? key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _database.Remove(key);
        }

        public List<OmsSample>? GetFromDb(string? key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _database.TryGetValue(key, out List<OmsSample>? value) ? value : new List<OmsSample>();
        }
    }
}