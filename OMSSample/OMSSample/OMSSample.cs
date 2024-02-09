namespace OMSSample;

public class OMSSample
{
    private uint _orderAmount;
    public uint orderAmount
    {
        get { return _orderAmount; }
        set
        {
            if (value > 0)
            {
                _orderAmount = value;
            }
            else
            {
                throw new ArgumentException("The value must be greater than 0");
            }
        }
    }

    public string orderSymbol { get; set; }
    public decimal price { get; set; }
}