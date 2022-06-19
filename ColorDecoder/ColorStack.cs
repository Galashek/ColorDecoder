using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ColorDecoder
{
    public class ColorStack
    {
        public Grid Stack { get; }
        public SolidColorBrush[] Colors => buttons.Select(b => b.Fill).Cast<SolidColorBrush>().ToArray();
        private readonly Shape[] buttons;
        private Shape selectedButton;

        public event Action ButtonPressed;

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
                    Fill = GameColors.EmptyColor,
                    Stroke = GameColors.StrokeColor
                };
                var j = i;
                colorButton.MouseUp += (s, e) =>
                {
                    selectedButton = colorButton;
                    ButtonPressed?.Invoke();
                };
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
        public void ChangeColor(Brush color)
        {
            selectedButton.Fill = color;
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