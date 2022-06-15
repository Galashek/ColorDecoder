using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ColorDecoder
{
    public class MatchStack
    {
        public Grid Stack { get; }

        public MatchStack(int black, int white)
        {
            Stack = new Grid
            {
                Width = 200,
                Height = 60
            };
            for (int i = 0; i < black + white; i++)
            {
                Stack.ColumnDefinitions.Add(new ColumnDefinition());
                var match = new Ellipse()
                {
                    Width = 40,
                    Height = 40,
                    Fill = i < black ? Brushes.Black : Brushes.White,
                    Stroke = Brushes.DarkSlateGray
                };
                Grid.SetColumn(match, i);
                Stack.Children.Add(match);
            }
        }
    }
}