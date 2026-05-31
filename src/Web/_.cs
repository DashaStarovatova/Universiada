public interface IEventCalendarService
{
    EventTypes GetEventTypeByDate(DateTime currentDate);
}

public class StubEventCalendarService : IEventCalendarService
{
    public EventTypes GetEventTypeByDate(DateTime currentDate)
    {
        return EventTypes.KeyRateSend;
    }
}

public enum EventTypes
{
    KeyRateSend,
    KeyRateAndMetodologySend,
    MetodologySend
}