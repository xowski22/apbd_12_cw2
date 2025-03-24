namespace apbd_12_cw2.Containers;

public class RefrigeratedContainer : Container
{
    public string ProductType { get; private set; }
    public double Temperature { get; private set; }
    
    private static readonly Dictionary<string, double> ProductTemperatures = new Dictionary<string, double>
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18 },
        { "Fish", 2 },
        { "Meat", -15 },
        { "Ice cream", -18 },
        { "Frozen pizza", -30 },
        { "Cheese", 7.2 },
        { "Sausages", 5 },
        { "Butter", 20.5 },
        { "Eggs", 19 }
    };
    
    public RefrigeratedContainer(int height, double emptyWeight, int depth, double maxCapacity, string productType) 
        : base(height, emptyWeight, depth, maxCapacity, "C")
    {
        if (!ProductTemperatures.ContainsKey(productType))
        {
            throw new ArgumentException($"Unknown product type: {productType}");
        }
        
        ProductType = productType;
        Temperature = ProductTemperatures[productType];
    }
    
    public override string ToString()
    {
        return base.ToString() + $", Type: Refrigerated, Product: {ProductType}, Temperature: {Temperature}°C";
    }
}