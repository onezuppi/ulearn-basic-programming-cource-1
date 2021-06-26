using System;
using System.Linq;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] people)
        {
            var days = GetRange(2, 30);
            var months = GetRange(1, 12);
            var heatmapData = new double[30, 12];
            foreach (var person in people)
            {
                if (person.BirthDate.Day != 1)
                    heatmapData[person.BirthDate.Day - 2, person.BirthDate.Month - 1]++;
            }
            return new HeatmapData(
                "Пример карты интенсивностей",
                heatmapData,
                days,
                months);
        }

        private static string[] GetRange(int start, int count)
        {
            return Enumerable.Range(start, count).Select(x => x.ToString()).ToArray();
        } 
    }
}