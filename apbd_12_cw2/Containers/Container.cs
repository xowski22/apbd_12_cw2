namespace apbd_12_cw2.Containers;

public abstract class Container
{
    
    public double CargoMass { get; protected set; }
    public int Height { get; private set; }
    public double EmptyWeight { get; private set; }
    public int Depth { get; private set; }
    public string SerialNumber { get; private set; }
    public double MaxCapacity { get; private set; } 
    
    protected Container(int height, double emptyWeight, int depth, double maxCapacity, string type)
    {
        Height = height;
        EmptyWeight = emptyWeight;
        Depth = depth;
        MaxCapacity = maxCapacity;
        SerialNumber = GenerateSerialNumber(type);
        CargoMass = 0;
    }
    
    private static Dictionary<string, int> serialNumberCounter = new Dictionary<string, int>();
    private string GenerateSerialNumber(string type)
    {
        if (!serialNumberCounter.ContainsKey(type))
        {
            serialNumberCounter[type] = 0;
        }
        
        serialNumberCounter[type]++;
        return $"KON-{type}-{serialNumberCounter[type]}";
    }
    
    public virtual void EmptyCargo()
    {
        CargoMass = 0;
    }
    
    public virtual void LoadCargo(double mass)
    {
        if (mass > MaxCapacity)
        {
            throw new OverfillException($"Cannot load {mass}kg into container with capacity of {MaxCapacity}kg");
        }
        
        CargoMass = mass;
    }
    
    public override string ToString()
    {
        return $"Container {SerialNumber}, Load: {CargoMass}kg/{MaxCapacity}kg, Height: {Height}cm, Depth: {Depth}cm, Empty Weight: {EmptyWeight}kg";
    }
}

public class OverfillException : Exception
{
    public OverfillException(string message) : base(message) { }
}