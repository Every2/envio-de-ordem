namespace OMSSample.Models;

public class Model
{
    private Dictionary<string, List<OMSSample>> _database;

    public Model(Dictionary<string, List<OMSSample>> database)
    {
        this._database = database;
    }

    public void AddToDb(string clOrdId, List<OMSSample> list)
    {
        this._database.Add(clOrdId, list);
    }

    public void Delete(string key, List<OMSSample> value)
    {
        this._database.Remove(key, out value);
    }
    
    public void Delete(string key)
    {
            this._database.Remove(key);
    }
}