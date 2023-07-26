namespace Codecool.DiligentDeliveries;

public class Rider
{
    public int Id { get; }
    public string Name => $"Rider{Id}";

    public Rider(int id, int reattemptPerAddress, int maximumReattempts)
    {
        //Initialize members
    }

    public void AddParcels(List<Parcel> parcels)
    {
        //Implement
    }

    public void StartRoutine()
    {
        //Implement
    }

    private bool Reattempt(Parcel parcel)
    {
        //Implement
    }

    private void HandleSuccessfulDelivery(Parcel parcel)
    {
        //Implement
    }
    
    public Report GetReport()
    {
        //Implement
    }
}