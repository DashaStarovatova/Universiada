namespace Domain;

public class Result

{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; } // FOREIGN KEY → Team.Id
    public string Period { get; set; } = "";
    public double NominalExchangeRate { get; set; }
    public double CPI { get; set; }
    public double RealExchangeRate { get; set; }
    public double RealGDP { get; set; }
    public double KeyRate { get; set; }
    public DateTime CreatedAt  { get; set; }
    public Guid? AnswerId {get;set;}



    private Result() {}
    
    public Result(Guid teamId, string period, double nominalExchangeRate,
    double cpi, double realExchangeRate, double realGDP, double keyRate, Guid? answerId) : this()
    {
        Id = Guid.NewGuid();
        TeamId = teamId;
        Period = period;
        NominalExchangeRate = nominalExchangeRate;
        CPI = cpi;
        RealExchangeRate = realExchangeRate;
        RealGDP = realGDP;
        KeyRate = keyRate;
        CreatedAt = DateTime.UtcNow;
        AnswerId=answerId;
    }

}
