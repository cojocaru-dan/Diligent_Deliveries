namespace Codecool.DiligentDeliveries;

class Program
{
    private static Random Random = new();

    private static string GetRandomStreetAddress() =>
        $"Random Street{Random.Next(100, 1000)} Number {Random.Next(0, 100)}";

    private static int GetRandomZipCode() => Random.Next(1000, 10000);
    private static string GetRandomCustomerName()
    {
        string[] customerNames = 
        {
            "Emily Johnson", "Liam Anderson", "Olivia Williams", "Noah Smith", "Ava Brown",
            "Ethan Davis", "Isabella Martinez", "Aiden Taylor", "Sophia Anderson", "Jackson Robinson",
            "Mia Harris", "Lucas Jackson", "Harper Thompson", "Oliver Davis", "Amelia Martinez",
            "Elijah Wilson", "Charlotte Moore", "Grayson White", "Abigail Turner", "Carter Scott",
            "Evelyn Hall", "Sebastian Walker", "Elizabeth Martinez", "Alexander Allen", "Scarlett Lewis",
            "James Foster", "Luna Carter", "Benjamin Mitchell", "Victoria Green", "Henry Wright",
            "Lily Murphy", "Samuel Hall", "Grace Miller", "Daniel Baker", "Chloe Adams",
            "Matthew Hill", "Zoey Clark", "Joseph Johnson", "Penelope Mitchell", "Michael Turner"
        };

        return customerNames[Random.Next(customerNames.Length)];
    }

    private static List<Parcel> GenerateListOfParcels(int packages)
    {
        List<Parcel> parcels = new List<Parcel>();
        for (int i = 0; i < packages; i++)
        {   

            Address addr = new Address(GetRandomZipCode(), GetRandomStreetAddress(), GetRandomCustomerName());
            Parcel parcel = new Parcel(i, addr);

            parcels.Add(parcel);
        }
        return parcels;
    }

    public static void Main(string[] args)
    {
        const int packages = 20;
        const int maximumReattempts = 3;

        List<Rider> riders = new List<Rider>();
        int[] reattemptsPerAddress = { 1, 2, 5 };

        StartRidersRoutine(riders, reattemptsPerAddress, maximumReattempts, packages);
        GetFinalReport(riders);
    }

    private static void StartRidersRoutine(List<Rider> riders, int[] reattemptsPerAddress, int maximumReattempts, int packages)
    {
        for (int i = 0; i < 3; i++)
        {
            List<Parcel> parcels = GenerateListOfParcels(packages);
            Rider rider = new Rider(i, reattemptsPerAddress[i], maximumReattempts, packages);
            rider.AddParcels(parcels);
            rider.StartRoutine();
            riders.Add(rider);
        }
    }

    private static void GetFinalReport(List<Rider> riders)
    {
        int maxSuccessfullyDeliveries = -1;
        List<Rider> winners = new List<Rider>();

        foreach (var rider in riders)
        {
            if (rider.SuccessfullDeliveries > maxSuccessfullyDeliveries)
            {
                maxSuccessfullyDeliveries = rider.SuccessfullDeliveries;
                winners.Clear();
                winners.Add(rider);
            }
            else if (rider.SuccessfullDeliveries == maxSuccessfullyDeliveries)
            {
                winners.Add(rider);
            }
            Console.WriteLine(rider.GetReport());
        }
        Console.WriteLine($"We have {winners.Count} winner{(winners.Count == 1 ? "" : "s")}");
        foreach (var winner in winners)
        {
            Console.WriteLine($"The winner is {winner.Name}");
        }
    }
}