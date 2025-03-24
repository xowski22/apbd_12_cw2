using apbd_12_cw2.Interfaces;

namespace apbd_12_cw2.Containers;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; private set; }
    
    public GasContainer(int height, double emptyWeight, int depth, double maxCapacity, double pressure) 
        : base(height, emptyWeight, depth, maxCapacity, "G")
    {
        Pressure = pressure;
    }
    
    public override void EmptyCargo()
    {
        CargoMass = MaxCapacity * 0.05;
    }
    
    public override void LoadCargo(double mass)
    {
        if (mass > MaxCapacity)
        {
            NotifyHazard(SerialNumber, $"Attempted to load {mass}kg which exceeds the capacity of {MaxCapacity}kg");
            throw new OverfillException($"Cannot load {mass}kg into container with capacity of {MaxCapacity}kg");
        }
        
        base.LoadCargo(mass);
    }
    
    public void NotifyHazard(string serialNumber, string message)
    {
        Console.WriteLine($"HAZARD ALERT for {serialNumber}: {message}");
    }
    
    public override string ToString()
    {
        return base.ToString() + $", Type: Gas, Pressure: {Pressure} atm";
    }
}