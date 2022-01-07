using System;
using System.Diagnostics;
public class ElapsedTime
{
    Stopwatch stopwatch;
    long elapsedTime;

    public ElapsedTime()
    {
        elapsedTime = 0;
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    // getting elapsed time in watch format like 01 : 57
    public string TimeInHourFormat()
    {          
        elapsedTime = stopwatch.ElapsedMilliseconds / 1000;
        int second = (int) elapsedTime % 60;
        int minute = (int) elapsedTime / 60;
        string result = string.Empty;

        if (second < 10)
        {
            result = string.Format(": 0{0}", second);
        }
        else  
        {
            result = string.Format(": {0}", second);
        }

        if (minute < 10)
        {                
            result = string.Format("0{1} {0}", result, minute);
        }
        else  
        {
            result = string.Format("{1} {0}", result, minute);
        }

        if (elapsedTime > 3600)
        {
            int hour = (int)elapsedTime / 3600;
            result = string.Format("{1} {0}", result, hour);
        }

        return result;
    }

    public void StopTimer()
    {
        stopwatch.Stop();
    }

}