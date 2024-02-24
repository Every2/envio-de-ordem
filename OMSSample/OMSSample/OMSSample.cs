namespace OMSSample;

public class OmsSample
{
    private uint _orderAmount;
    private string? _orderSymbol;
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

    public string? OrderSymbol
    {
        get => _orderSymbol;
        set
        {
            if (value != null)
            {
                _orderSymbol = value;
                
            }
            else
            {
                throw new ArgumentNullException($"Can't be null {nameof(value)}");
            }
        }
         }
    public decimal Price { get; set; }
}