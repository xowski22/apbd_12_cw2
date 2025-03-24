using apbd_12_cw2.Interfaces;

namespace apbd_12_cw2.Containers;


public class LiquidContainer : Container, IHazardNotifier
{
    public bool ContainsHazardousMaterial { get; private set; }
    
    public LiquidContainer(int height, double emptyWeight, int depth, double maxCapacity, bool hazardous) 
        : base(height, emptyWeight, depth, maxCapacity, "L")
    {
        ContainsHazardousMaterial = hazardous;
    }
    
    public override void LoadCargo(double mass)
    {
        double maxAllowedFill = ContainsHazardousMaterial ? MaxCapacity * 0.5 : MaxCapacity * 0.9;
        
        if (mass > maxAllowedFill)
        {
            NotifyHazard(SerialNumber, $"Attempted to load {mass}kg which exceeds the safe limit of {maxAllowedFill}kg");
            throw new OverfillException($"Safety limit exceeded: tried to load {mass}kg into container with safe limit of {maxAllowedFill}kg");
        }
        
        base.LoadCargo(mass);
    }
    
    public void NotifyHazard(string serialNumber, string message)
    {
        Console.WriteLine($"HAZARD ALERT for {serialNumber}: {message}");
    }
    
    public override string ToString()
    {
        return base.ToString() + $", Type: Liquid, Hazardous: {ContainsHazardousMaterial}";
    }
}