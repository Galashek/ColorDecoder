using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ColorDecoder
{
    public static class GameColors
    {
        public static readonly IEnumerable<SolidColorBrush> AllColors;
        private static readonly string[] colorStrings;

        static GameColors()
        {
            colorStrings = new[]
            {
                "#61d800", // green            
                "#1976D2", // blue
                "#F44336", // red
                "#AB47BC", // violet
                "#EEFF41", // yellow
                "#ff8d00" // orange
            };
            AllColors = colorStrings.Select(s => BrushFromColorString(s));
        }

        public static readonly SolidColorBrush 
            EmptyColor = Brushes.DarkGray,
            StrokeColor = Brushes.DarkSlateGray,
            BlackMatch = BrushFromColorString("#424242"),
            WhiteMatch = BrushFromColorString("#FAFAFA"),
            PanelWin = BrushFromColorString("#69F0AE"),
            PanelLose = BrushFromColorString("#EF5350"),
            PanelDefault = BrushFromColorString("#DDDDDD");

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

        private static SolidColorBrush BrushFromColorString(string s)        
            => new SolidColorBrush((Color)ColorConverter.ConvertFromString(s));
    }
}