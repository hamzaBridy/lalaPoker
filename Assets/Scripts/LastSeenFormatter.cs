using System;

public class LastSeenFormatter
{
    public static string FormatLastSeen(string lastSeenTimestamp)
    {
        // Parse the timestamp
        DateTime lastSeenUtc = DateTime.ParseExact(lastSeenTimestamp, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

        // Convert to local time
        DateTime lastSeenLocal = lastSeenUtc.ToLocalTime();

        // Calculate the difference
        TimeSpan timeDifference = DateTime.Now - lastSeenLocal;

        // Format the difference
        if (timeDifference.TotalDays > 365)
        {
            int years = (int)(timeDifference.TotalDays / 365);
            return $" {years} year{(years > 1 ? "s" : "")} ago";
        }
        else if (timeDifference.TotalDays > 30)
        {
            int months = (int)(timeDifference.TotalDays / 30);
            return $" {months} month{(months > 1 ? "s" : "")} ago";
        }
        else if (timeDifference.TotalDays >= 1)
        {
            return $" {timeDifference.Days} day{(timeDifference.Days > 1 ? "s" : "")} ago";
        }
        else if (timeDifference.TotalHours >= 1)
        {
            return $" {timeDifference.Hours} hour{(timeDifference.Hours > 1 ? "s" : "")} ago";
        }
        else
        {
            return " less than an hour ago";
        }
    }
}
