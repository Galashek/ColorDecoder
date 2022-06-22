using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorDecoder
{
    public class ColorSet : Grid
    {
        public SolidColorBrush[] Colors => buttons.Select(b => b.Color).Cast<SolidColorBrush>().ToArray();
        public bool AllColorsAreSelected => !Colors.Any(c => c == GameColors.EmptyColor);
        private readonly ColorButton[] buttons;
        private ColorButton selectedButton;

        public event Action ButtonPressed;

        public ColorSet()
        {
            Width = 200;
            Height = 60;

            buttons = new ColorButton[4];
            for (int i = 0; i < 4; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
                var colorButton = new ColorButton();
                var j = i;
                colorButton.Click += (s, e) =>
                {
                    selectedButton = colorButton;
                    ButtonPressed?.Invoke();
                };
                buttons[i] = colorButton;
                SetColumn(colorButton, i);
                Children.Add(colorButton);
            }
        }
        public void SetColors(SolidColorBrush[] colors)
        {
            if (colors.Length < 4) return;
            for (int i = 0; i < 4; i++)
            {
                buttons[i].Color = colors[i];
            }
        }
        public void ChangeColor(Brush color)
        {
            selectedButton.Color = color;
        }
        public void Disable()
        {
            foreach (var button in buttons)
                button.IsEnabled = false;
        }
    }
}