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
        string sourcePath = @"C:\Users\ozzy\source\repos\SystemIOTest\personalbest.ini";
        List<string> carNames = new List<string>();
        List<string> trackNames = new List<string>();
        List<string> dates = new List<string>();
        List<string> times = new List<string>();
        
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
                    dates.Add(date);
                }
                else if (line.Contains("TIME"))
                {
                    string time = line.Substring(line.LastIndexOf("=") + 1);
                    times.Add(time);
                }
            }
        }

        List<LapTime> lapTimes = new List<LapTime>();
        for (int i = 0; i < carNames.Count; i++)
        {
            // TODO: timespan is fake, maybe add another model for storing time ?
            // TODO: display date for track record, right now its not used - maybe another model?
            lapTimes.Add(new LapTime(trackNames[i],carNames[i],new TimeSpan(1)));
        }

        return lapTimes;
    }
}