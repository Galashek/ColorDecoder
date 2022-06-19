using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace ColorDecoder
{
    public static class GameColors
    {
        public static readonly SolidColorBrush[] AllColors = 
        { 
            Brushes.Crimson, 
            Brushes.Green, 
            Brushes.RoyalBlue, 
            Brushes.DarkViolet, 
            Brushes.Yellow, 
            Brushes.Orange 
        };
        public static readonly SolidColorBrush EmptyColor = Brushes.DarkGray;
        public static readonly SolidColorBrush StrokeColor = Brushes.DarkSlateGray;
        public static SolidColorBrush[] GetRandomColors(int count = 4)
        {
            var colors = new SolidColorBrush[count];
            var rnd = new Random();
            var colorList = new List<SolidColorBrush>(AllColors);
            for (int i = 0; i < count; i++)
            {
                var index = rnd.Next(0, colorList.Count);
                colors[i] = colorList[index];
                colorList.RemoveAt(index);
            }
            return colors;
        }
    }
}