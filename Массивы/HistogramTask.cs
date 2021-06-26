using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] people, string name)
        {
            var days = GetDayNumbers();
            var birthdayСounts = new double[31]; 
            foreach(var person in people )
            {
                if (person.Name == name && person.BirthDate.Day != 1)
                    birthdayСounts[person.BirthDate.Day - 1]++;
            }
            return new HistogramData(
                $"Рождаемость людей с именем '{name}'",
                days,
                birthdayСounts);
        }
		
        private static string[] GetDayNumbers()
        {
            var days = new string[31];
            for (int dayNumber = 1; dayNumber < 32; dayNumber++)
                days[dayNumber - 1] = dayNumber.ToString();
            
            return days;
        }
    }
}