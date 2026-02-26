using LapLog.Models;

namespace LapLog.Services;

public class TelemetryProvider : ITelemetryProvider
{
    // async / await => many threads working in the same time,
    // App asks the database for data, but app is not frozen when waiting
    // User can move the window, spinner will work etc.
    
    public async Task<IEnumerable<LapTime>> GetAllLapTimes()
    {
        return new List<LapTime>()
        {
            new LapTime("Akina", "Toyota AE86", new TimeSpan(4, 30, 738)),
            new LapTime("Akagi", "Mazda RX7", new TimeSpan(4, 37, 529)),
            new LapTime("Nordschleife", "Nissan Silvia S15", new TimeSpan(9, 50, 680)),
            new LapTime("Nordschleife", "Nissan Silvia S15", new TimeSpan(13, 10, 393))
        };
    }
}