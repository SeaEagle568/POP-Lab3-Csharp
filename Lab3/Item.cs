public class Item(string producedBy, int serialNumber)
{
    public string ProducedBy { get; } = producedBy;
    public string SerialNumber { get; } = $"{producedBy[0]}#{serialNumber}";
}
