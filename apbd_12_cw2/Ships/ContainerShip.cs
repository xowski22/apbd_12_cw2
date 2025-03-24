using apbd_12_cw2.Containers;

namespace apbd_12_cw2.Ships;

public class ContainerShip
    {
        public List<Container> Containers { get; private set; }
        public double MaxSpeed { get; private set; }
        public int MaxContainerCount { get; private set; }
        public double MaxWeight { get; private set; }
        public string Name { get; private set; }
        
        public ContainerShip(string name, double maxSpeed, int maxContainerCount, double maxWeight)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            MaxContainerCount = maxContainerCount;
            MaxWeight = maxWeight;
            Containers = new List<Container>();
        }
        
        public bool LoadContainer(Container container)
        {
            if (Containers.Count >= MaxContainerCount)
            {
                Console.WriteLine($"Cannot load container {container.SerialNumber}: ship is at maximum container capacity.");
                return false;
            }
            
            double currentWeight = Containers.Sum(c => c.CargoMass + c.EmptyWeight) / 1000;
            double containerWeight = (container.CargoMass + container.EmptyWeight) / 1000;
            
            if (currentWeight + containerWeight > MaxWeight)
            {
                Console.WriteLine($"Cannot load container {container.SerialNumber}: would exceed ship's weight limit.");
                return false;
            }
            
            Containers.Add(container);
            Console.WriteLine($"Container {container.SerialNumber} loaded onto {Name}.");
            return true;
        }
        
        public void LoadContainers(List<Container> containers)
        {
            foreach (var container in containers)
            {
                LoadContainer(container);
            }
        }
        
        public bool RemoveContainer(string serialNumber)
        {
            Container container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            
            if (container == null)
            {
                Console.WriteLine($"Container {serialNumber} not found on ship {Name}.");
                return false;
            }
            
            Containers.Remove(container);
            Console.WriteLine($"Container {serialNumber} removed from ship {Name}.");
            return true;
        }
        
        public bool ReplaceContainer(string oldSerialNumber, Container newContainer)
        {
            int index = Containers.FindIndex(c => c.SerialNumber == oldSerialNumber);
            
            if (index == -1)
            {
                Console.WriteLine($"Container {oldSerialNumber} not found on ship {Name}.");
                return false;
            }
            
            double currentWeight = Containers.Sum(c => c.CargoMass + c.EmptyWeight) / 1000;
            double oldContainerWeight = (Containers[index].CargoMass + Containers[index].EmptyWeight) / 1000;
            double newContainerWeight = (newContainer.CargoMass + newContainer.EmptyWeight) / 1000;
            
            if (currentWeight - oldContainerWeight + newContainerWeight > MaxWeight)
            {
                Console.WriteLine($"Cannot replace with container {newContainer.SerialNumber}: would exceed ship's weight limit.");
                return false;
            }
            
            Containers[index] = newContainer;
            Console.WriteLine($"Container {oldSerialNumber} replaced with {newContainer.SerialNumber} on ship {Name}.");
            return true;
        }
        
        public bool TransferContainer(string serialNumber, ContainerShip destinationShip)
        {
            Container container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            
            if (container == null)
            {
                Console.WriteLine($"Container {serialNumber} not found on ship {Name}.");
                return false;
            }
            
            if (destinationShip.LoadContainer(container))
            {
                Containers.Remove(container);
                Console.WriteLine($"Container {serialNumber} transferred from {Name} to {destinationShip.Name}.");
                return true;
            }
            
            return false;
        }
        
        public void PrintContainerInfo()
        {
            Console.WriteLine($"Containers on ship {Name}:");
            if (Containers.Count == 0)
            {
                Console.WriteLine("  None");
                return;
            }
            
            foreach (var container in Containers)
            {
                Console.WriteLine($"  {container}");
            }
        }
        
        public override string ToString()
        {
            double currentWeight = Containers.Sum(c => c.CargoMass + c.EmptyWeight) / 1000;
            
            return $"Ship: {Name}, Speed: {MaxSpeed} knots, Containers: {Containers.Count}/{MaxContainerCount}, Weight: {currentWeight}/{MaxWeight} tons";
        }
    }