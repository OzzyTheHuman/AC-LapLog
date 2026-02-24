using LapLog.Models;

namespace LapLog.Services;

public interface ITelemetryProvider
{
    Task<IEnumerable<LapTime>> GetAllLapTimes();
}