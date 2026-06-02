using Application.Services;

public interface IEventCalendarService
{
    EventTypes GetEventTypeByDate(DateTime currentDate);
}

public class EventCalendarService : IEventCalendarService
{
    private readonly List<DateTime> _keyRateDates = new()
    {
        new DateTime(2026, 6, 2),
        new DateTime(2026, 5, 10)
    };

    private readonly List<DateTime> _keyRateAndMetodologyDates = new()
    {
        new DateTime(2026, 5, 3),
        new DateTime(2026, 5, 4),
        new DateTime(2026, 5, 7)
    };

    private readonly List<DateTime> _metodologyDates = new()
    {
        new DateTime(2026, 5, 14),
        new DateTime(2026, 5, 15)
    };

    public EventTypes GetEventTypeByDate(DateTime date)
    {
        if (_keyRateDates.Contains(date.Date))
            return EventTypes.KeyRateSend;

        if (_keyRateAndMetodologyDates.Contains(date.Date))
            return EventTypes.KeyRateAndMetodologySend;

        if (_metodologyDates.Contains(date.Date))
            return EventTypes.MetodologySend;

        throw new Exception($"Дата {date:yyyy-MM-dd} не найдена ни в одном списке");
    }


    // public EventTypes GetEventTypeByDate(DateTime currentDate)
    // {
    //     return EventTypes.KeyRateSend;
    // }
}

public enum EventTypes
{
    KeyRateSend,
    KeyRateAndMetodologySend,
    MetodologySend
}

public class MatlabWorker : BackgroundService
{
    private readonly IAnswersResolver _resolver;

    public MatlabWorker(IAnswersResolver resolver)
    {
        _resolver = resolver;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _resolver.RunAsync(stoppingToken);
    }
}
