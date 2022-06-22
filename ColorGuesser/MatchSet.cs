using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ColorDecoder
{
    public class MatchSet : Grid
    {        
        public MatchSet(int onlyColorMatch, int colorAndPositionMatch)
        {
            Width = 200;
            Height = 60;

            for (int i = 0; i < 4; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < onlyColorMatch + colorAndPositionMatch; i++)
            {
                var match = new Ellipse()
                {
                    Width = 40,
                    Height = 40,
                    Fill = i < onlyColorMatch ? 
                        GameColors.BlackMatch : GameColors.WhiteMatch,
                    Stroke = GameColors.StrokeColor
                };
                SetColumn(match, i);
                Children.Add(match);
            }
        }
    }
}