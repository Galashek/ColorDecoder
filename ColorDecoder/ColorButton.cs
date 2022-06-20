using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ColorDecoder
{
    class ColorButton : Button
    {
        private readonly Shape colorShape;
        public TextBlock text { get; }

        public Brush Color 
        { 
            get => colorShape.Fill; 
            set 
            { 
                colorShape.Fill = value;
                text.Visibility = Visibility.Hidden;
            }
        }

        public ColorButton()
        {            
            Width = 50;
            Height = 50;

            colorShape = new Ellipse()
            {
                Width = 40,
                Height = 40,                
                Fill = GameColors.EmptyColor,
                Stroke = GameColors.StrokeColor,
                IsHitTestVisible = false
            };

            text = new TextBlock();
            text.Text = "?";
            text.FontSize = 14;
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;

            var content = new Grid();
            content.Children.Add(colorShape);
            content.Children.Add(text);
            Content = content;
        }
    }
}
