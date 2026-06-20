namespace Domain;

public class Answer
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime CreatedDate { get; set; }
    public double AnswerValue { get; set; }
    public int NullCount {get;set;} = 0;


    private Answer() { }

    public Answer(Guid teamId, double answerValue, int nullCount) : this()
    {
        TeamId = teamId;
        AnswerValue = answerValue;
        CreatedAt = DateTime.UtcNow;
        CreatedDate = DateTime.UtcNow.Date;
        NullCount = nullCount;
    }

    public bool IsTodayAnswer()
    {
        return CreatedDate == DateTime.UtcNow.Date;
        //return false;
    }
}
