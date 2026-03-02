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
        
        List<Car> cars = new List<Car>();
        List<Track> tracks = new List<Track>();
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

                    cars.Add(new Car(carName));
                    tracks.Add(new Track(trackName));
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

        // We have a few lists, they are not connected (they just have matching objects in the right positions).
        // It seems unprofessional, but it works for now.
        List<LapTime> lapTimes = new List<LapTime>();
        for (int i = 0; i < cars.Count; i++)
        {
            lapTimes.Add(new LapTime(tracks[i],cars[i],times[i],dates[i]));
        }

        return lapTimes;
    }
}