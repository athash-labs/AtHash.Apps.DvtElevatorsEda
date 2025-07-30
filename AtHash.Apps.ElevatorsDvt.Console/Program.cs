using AtHash.Apps.ElevatorsDvt.Base.Enumerations;
using System.Net.Http.Json;

public class Program
{
    private static readonly HttpClient _client = new HttpClient { BaseAddress = new Uri("http://localhost:5236/api/") };
    //private static readonly HttpClient _client = new HttpClient { BaseAddress = new Uri("https://localhost:5236/api/") };
    private static string _currentElevatorId;

    public static async Task Main(string[] args)
    {
        Console.WriteLine("Elevator Simulation Client");

        while (true)
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Request elevator");
            Console.WriteLine("2. Select floor inside elevator");
            Console.WriteLine("3. Exit");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await RequestElevator();
                    break;
                case "2":
                    await SelectFloor();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    public static async Task RequestElevator()
    {
        Console.Write("Enter your current floor (1-35): ");

        if (!int.TryParse(Console.ReadLine(), out int floor) || floor < 1 || floor > 35)
        {
            Console.WriteLine("Invalid floor number");

            return;
        }

        Console.Write("Enter direction (U for Up, D for Down): ");

        var directionInput = Console.ReadLine().ToUpper();
        ElevatorDirection direction = directionInput == "U" ? ElevatorDirection.GoingUp : ElevatorDirection.GoingDown;

        var request = new
        {
            FloorNumber = floor,
            Direction = direction
        };

        var response = await _client.PostAsJsonAsync("elevator/request", request);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Elevator called successfully. Waiting for elevator...");

            // In a real app, you'd subscribe to events or poll for status
            // For simplicity, we'll just simulate the elevator arrival
            _currentElevatorId = Guid.NewGuid().ToString();
            Console.WriteLine($"Elevator {_currentElevatorId} has arrived. Doors are open.");
        }
        else
        {
            Console.WriteLine("Failed to call elevator");
        }
    }

    public static async Task SelectFloor()
    {
        if (string.IsNullOrEmpty(_currentElevatorId))
        {
            Console.WriteLine("You must call an elevator first");

            return;
        }

        Console.Write("Enter destination floor (1-35): ");
        if (!int.TryParse(Console.ReadLine(), out int floor) || floor < 1 || floor > 35)
        {
            Console.WriteLine("Invalid floor number");

            return;
        }

        var request = new
        {
            ElevatorId = _currentElevatorId,
            DestinationFloor = floor
        };

        var response = await _client.PostAsJsonAsync("elevator/select-floor", request);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Elevator is moving to floor {floor}...");
            Console.WriteLine($"Arrived at floor {floor}. Doors are open.");
            _currentElevatorId = null; // Passenger has exited
        }
        else
        {
            Console.WriteLine("Failed to select floor");
        }
    }
}