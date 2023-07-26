namespace Codecool.DiligentDeliveries;

class Program
{
    private static Random Random = new();

    private static string GetRandomStreetAddress() =>
        $"Random Street{Random.Next(100, 1000)} Number {Random.Next(0, 100)}";
    

    public static void Main(string[] args)
    {
        const int packages = 10;
        const int maximumReattempts = 5;
    }
}