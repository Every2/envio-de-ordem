namespace OMSSample;

public class OmsSample
{
    private uint _orderAmount;
    public uint OrderAmount
    {
        get => _orderAmount;
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

    public string OrderSymbol { get; set; }
    public decimal Price { get; set; }
}