namespace Codecool.DiligentDeliveries;

public class Rider
{
    public int Id { get; }
    public string Name => $"Rider{Id}";
    public int ReattemptsPerAddress { get; }
    public int MaximumReattempts { get; }
    public int ReattemptsLeft { get; private set; }
    public int SuccessfullDeliveries { get; private set; }
    public List<Parcel> PackagesList { get; private set; }
    public int NrOfPackages { get; }


    public Rider(int id, int reattemptPerAddress, int maximumReattempts, int packages)
    {
        //Initialize members
        Id = id;
        ReattemptsPerAddress = reattemptPerAddress;
        MaximumReattempts = maximumReattempts;
        ReattemptsLeft = maximumReattempts;
        SuccessfullDeliveries = 0;
        PackagesList = new List<Parcel>();
        NrOfPackages = packages;
    }

    public void AddParcels(List<Parcel> parcels)
    {
        //Implement
        PackagesList.Clear();
        PackagesList.AddRange(parcels);
    }

    public void StartRoutine()
    {
        //Implement
        foreach (var parcel in PackagesList)
        {
            if (parcel.Deliver())
            {
                HandleSuccessfulDelivery(parcel);   
            };
        }

        foreach (var parcel in PackagesList)
        {
            for (int i = 0; i < ReattemptsPerAddress; i++)
            {
                if (!parcel.Delivered && ReattemptsLeft > 0) 
                {
                    if (Reattempt(parcel))
                    {
                        HandleSuccessfulDelivery(parcel);
                    }
                    
                }
            }
        }       
    }

    private bool Reattempt(Parcel parcel) 
    {
        //Implement 
        ReattemptsLeft--;
        return parcel.Deliver();
    }

    private void HandleSuccessfulDelivery(Parcel parcel)
    {
        //Implement
        parcel.Delivered = true;
        SuccessfullDeliveries++;
    }
    
    public Report GetReport()
    {
        //Implement
        return new Report(Name, SuccessfullDeliveries, NrOfPackages, ReattemptsLeft);
    }
}

public class Address
{
    public int ZipCode { get; }
    public string StreetAddress { get; }
    public string CustomerName { get; }

    public Address(int zipcode, string streetAddress, string customerName)
    {
        ZipCode = zipcode;
        StreetAddress = streetAddress;
        CustomerName = customerName;
    }
    public bool Equals(Address? obj)
    {
        if (obj == null || obj.GetType() != this.GetType()) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (ZipCode == obj.ZipCode && StreetAddress == obj.StreetAddress && CustomerName == obj.CustomerName) return true;
        return false;
    }
    public override int GetHashCode()
    {
        return ZipCode;
    }

    public override string ToString()
    {
        return $"ZipCode: {ZipCode}, StreetAddress: {StreetAddress}, CustomerName: {CustomerName}";
    } 
}

public class Parcel
{
    private static Random rand = new Random();
    public int Id { get; }
    public Address Address { get; set; }

    public bool Delivered { get; set; }

    public Parcel(int id, Address address)
    {
        Id = id;
        Address = address;
        Delivered = false;
    }

    public bool Deliver()
    {
        int[] ints= new int[100];

        for (int i = 0; i < 50; i++)
        {
            ints[i] = 1;
            ints[i + 50] = rand.Next(1, 8);
        }

        return ints[rand.Next(0, ints.Length)] == 1;
    }

    public override string ToString()
    {
        return $"Parcel_Id: {Id}, Parcel_Address: {Address}";
    }
}

public class Report
{
    public string Name { get; }
    public int SuccessfullDeliveries { get; }
    public int NrOfPackages { get; }
    public int ReattemptsLeft { get; }

    public Report(string name, int successfullDeliveries, int nrOfPackages, int reattemptsLeft)
    {
        Name = name;
        SuccessfullDeliveries = successfullDeliveries;
        NrOfPackages = nrOfPackages;
        ReattemptsLeft = reattemptsLeft;
    }
    public override string ToString()
    {
        return $"{Name} delivered {SuccessfullDeliveries} packages out of {NrOfPackages} ({ReattemptsLeft} reattempts left)";
    }
}