using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ColorDecoder
{
    public class ColorStack
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
        public Grid Stack { get; }
        public SolidColorBrush[] Colors => buttons.Select(b => b.Fill).Cast<SolidColorBrush>().ToArray();
        private readonly Shape[] buttons;

        public ColorStack()
        {
            Stack = new Grid
            {
                Width = 200,
                Height = 60
            };
            buttons = new Shape[4];
            for (int i = 0; i < 4; i++)
            {
                Stack.ColumnDefinitions.Add(new ColumnDefinition());
                var colorButton = new Ellipse()
                {
                    Width = 40,
                    Height = 40,
                    Fill = EmptyColor,
                    Stroke = Brushes.DarkSlateGray
                };
                var j = i;
                colorButton.MouseUp += (s, e) => ChangeColor(j);
                colorButton.Tag = -1;
                buttons[i] = colorButton;
                Grid.SetColumn(colorButton, i);
                Stack.Children.Add(colorButton);
            }
        }

        public void SetColors(SolidColorBrush[] colors)
        {
            if (colors.Length < 4) return;
            for (int i = 0; i < 4; i++)
            {
                buttons[i].Fill = colors[i];
            }
        }

        private void ChangeColor(int index)
        {
            var button = buttons[index];
            var curColorIndex = (int)button.Tag;
            int newColorIndex;
            if (curColorIndex == -1)
                newColorIndex = 0;
            else newColorIndex = (curColorIndex + 1) % 6;
            button.Fill = AllColors[newColorIndex];
            button.Tag = newColorIndex;
        }

        public void Disable()
        {
            foreach (var button in buttons)
            {
                button.IsEnabled = false;
            }
        }
    }
}