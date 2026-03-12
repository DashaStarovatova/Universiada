namespace Domain;

public class Answer
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; } // FOREIGN KEY → Team.Id
    public DateTime CreatedAt  { get; set; }
    public DateTime CreatedDate { get; set; }
    public double AnswerValue { get; set; }


    private Answer() {}
    
    public Answer(Guid teamId, double answerValue) : this()
    {
        TeamId = teamId;
        AnswerValue = answerValue;
        CreatedAt = DateTime.UtcNow;
        CreatedDate = DateTime.UtcNow.Date; 
    }

}
