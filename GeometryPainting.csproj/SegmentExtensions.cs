using System;
using System.Collections.Generic;
using GeometryTasks;
using System.Drawing;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        public static Dictionary<Segment, Color> SegmentsColors = new Dictionary<Segment, Color>();

        public static void SetColor(this Segment segment, Color color)
        {
            SegmentsColors.Add(segment, color);
        }

        public static Color GetColor(this Segment segment)
        {
            return (!SegmentsColors.ContainsKey(segment))? SegmentsColors[segment]: Color.Black;
        }
    }   
}
