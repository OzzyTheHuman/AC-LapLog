using LapLog.Models;
using System.IO;

namespace LapLog.Services;

public class TelemetryProvider : ITelemetryProvider
{
    // async / await => many threads working in the same time,
    // App asks the database for data, but app is not frozen when waiting
    // User can move the window, spinner will work etc.
    // TODO: Its not real async method, fix it
    public async Task<IEnumerable<LapTime>> GetAllLapTimes()
    {
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string sourcePath = Path.Combine(documentsPath, "Assetto Corsa\\personalbest.ini");
        
        List<string> carNames = new List<string>();
        List<string> trackNames = new List<string>();
        List<DateTime> dates = new List<DateTime>();
        List<TimeSpan> times = new List<TimeSpan>();
        
        using (StreamReader sr = new StreamReader(sourcePath))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line.Contains('@'))
                {
                    string carName = line.Substring(1, line.IndexOf('@') - 1);
                    string trackName = line.Substring(line.IndexOf('@') + 1).TrimEnd(']');

                    carNames.Add(carName);
                    trackNames.Add(trackName);
                }
                else if (line.Contains("DATE"))
                {
                    string date = line.Substring(line.LastIndexOf("=") + 1);
                    DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(date)).DateTime;
                    
                    dates.Add(dateTime);
                }
                else if (line.Contains("TIME"))
                {
                    string time = line.Substring(line.LastIndexOf("=") + 1);
                    TimeSpan timeSpan = TimeSpan.FromMilliseconds(long.Parse(time));
                    
                    times.Add(timeSpan);
                }
            }
        }

        List<LapTime> lapTimes = new List<LapTime>();
        for (int i = 0; i < carNames.Count; i++)
        {
            lapTimes.Add(new LapTime(trackNames[i],carNames[i],times[i],dates[i]));
        }

        return lapTimes;
    }
}