 using apbd_12_cw2.Containers;
 using apbd_12_cw2.Ships;

 class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Container Management System");
                
                var liquidContainer = new LiquidContainer(250, 1500, 200, 20000, false);
                var hazardousLiquidContainer = new LiquidContainer(250, 1800, 200, 25000, true);
                var gasContainer = new GasContainer(300, 2000, 220, 18000, 1.5);
                var refrigeratedContainer = new RefrigeratedContainer(280, 2500, 240, 22000, "Bananas");
                
                Console.WriteLine("Created containers:");
                Console.WriteLine(liquidContainer);
                Console.WriteLine(hazardousLiquidContainer);
                Console.WriteLine(gasContainer);
                Console.WriteLine(refrigeratedContainer);
                
                Console.WriteLine("\nLoading cargo into containers...");
                
                liquidContainer.LoadCargo(15000);
                Console.WriteLine($"Loaded 15000kg into {liquidContainer.SerialNumber}");
                
                try
                {
                    hazardousLiquidContainer.LoadCargo(15000);
                }
                catch (OverfillException ex)
                {
                    Console.WriteLine($"Expected exception: {ex.Message}");
                    hazardousLiquidContainer.LoadCargo(12000);
                    Console.WriteLine($"Loaded 12000kg into {hazardousLiquidContainer.SerialNumber}");
                }
                
                gasContainer.LoadCargo(16000);
                Console.WriteLine($"Loaded 16000kg into {gasContainer.SerialNumber}");
                
                refrigeratedContainer.LoadCargo(20000);
                Console.WriteLine($"Loaded 20000kg into {refrigeratedContainer.SerialNumber}");
                
                Console.WriteLine("\nCreating container ships...");
                var ship1 = new ContainerShip("Evergreen", 25, 1000, 50000);
                var ship2 = new ContainerShip("Maersk Line", 22, 800, 40000);
                
                Console.WriteLine(ship1);
                Console.WriteLine(ship2);
                
                Console.WriteLine("\nLoading containers onto ships...");
                ship1.LoadContainer(liquidContainer);
                ship1.LoadContainer(hazardousLiquidContainer);
                ship2.LoadContainer(gasContainer);
                ship2.LoadContainer(refrigeratedContainer);
                
                Console.WriteLine("\nShips after loading:");
                Console.WriteLine(ship1);
                Console.WriteLine(ship2);
                
                Console.WriteLine("\nContainer details on ships:");
                ship1.PrintContainerInfo();
                ship2.PrintContainerInfo();
                
                Console.WriteLine("\nTransferring container between ships...");
                ship1.TransferContainer(hazardousLiquidContainer.SerialNumber, ship2);
                
                Console.WriteLine("\nShips after transfer:");
                ship1.PrintContainerInfo();
                ship2.PrintContainerInfo();
                
                Console.WriteLine("\nEmptying gas container and checking remaining cargo...");
                gasContainer.EmptyCargo();
                Console.WriteLine($"Gas container after emptying: {gasContainer}");
                
                Console.WriteLine("\nReplacing a container on ship2...");
                var newRefContainer = new RefrigeratedContainer(280, 2200, 240, 20000, "Fish");
                newRefContainer.LoadCargo(18000);
                ship2.ReplaceContainer(refrigeratedContainer.SerialNumber, newRefContainer);
                
                Console.WriteLine("\nShip2 after replacement:");
                ship2.PrintContainerInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
