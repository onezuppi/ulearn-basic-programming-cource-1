using System.Collections.Generic;
using System.Drawing;
using GeometryTasks;


namespace GeometryPainting
{
    public static class SegmentExtention
    {
        private static Dictionary<Segment, Color> colors = new Dictionary<Segment, Color>();

        public static Color GetColor(this Segment segment)
        {
            return colors.ContainsKey(segment) ? colors[segment] : Color.Black;
        }

        public static void SetColor(this Segment segment, Color color)
        {
            colors[segment] = color;
        }
    }
}