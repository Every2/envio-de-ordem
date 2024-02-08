namespace OMSSample.Models;

public class Model<T>
{
    private Dictionary<string, List<T>> _database;

    public Model(Dictionary<string, List<T>> database)
    {
        this._database = database;
    }

    public void AddToDb(string clOrdId, List<T> list)
    {
        this._database.Add(clOrdId, list);
    }

    public void Delete(string key, List<T> value)
    {
        this._database.Remove(key, out value);
    }
    
    public void Delete(string key)
    {
            this._database.Remove(key);
    }
}