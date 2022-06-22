using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorDecoder
{
    public partial class MainWindow
    {
        private ColorSet answerColors;
        private ColorSet tryColors;
        private SolidColorBrush[] colorsToGuess;
        private int tryCount;

        public MainWindow()
        {
            InitializeComponent();
            StartGame(null, null);
        }        

        private void StartGame(object sender, RoutedEventArgs e)
        {
            tryCount = 0;
            tryResults.Children.Clear();
            tries.Children.Clear();
            answer.Children.Clear();
            gameStateText.Text = "Guess 4 colors";
            panel.Background = GameColors.PanelDefault;
            colorsToGuess = GameColors.GetRandomColors();
            answerColors = new ColorSet();
            answerColors.Disable();
            answer.Children.Add(answerColors);
            answerColors.SetColors(colorsToGuess);
            FillPalette();
            NewTry();
        }

        private void NewTry()
        {
            tryColors = new ColorSet();
            tryColors.ButtonPressed += () => palettePopup.IsOpen = true;
            Grid.SetRow(tryColors, tryCount);
            tries.Children.Add(tryColors);
            Grid.SetRow(checkButton, tryCount);
            tryCountText.Text = $"Try {tryCount + 1}/6";
            checkButton.Style = Resources["checkBtnStyle_select"] as Style;
        }

        private void EndGame(bool won)
        {
            gameStateText.Text = won ? "Well done!" : "Try again";
            panel.Background = won ? GameColors.PanelWin : GameColors.PanelLose;
            checkButton.Style = Resources["hidden"] as Style;
            answerColors.SetColors(colorsToGuess);
        }

        private void Check(object sender, RoutedEventArgs e)
        {
            var curStackColors = tryColors.Colors;
            var colorAndPositionMatch = 0;
            var onlyColorMatch = curStackColors
                .Select(b => b.Color)
                .Distinct()
                .Count(c => colorsToGuess
                            .Select(b => b.Color)
                            .Contains(c));
            for (int i = 0; i < 4; i++)
            {
                if (curStackColors[i].Color == colorsToGuess[i].Color)
                {
                    colorAndPositionMatch++;
                    onlyColorMatch--;
                }
            }
            var matchSet = new MatchSet(onlyColorMatch, colorAndPositionMatch);
            Grid.SetRow(matchSet, tryCount);
            tryResults.Children.Add(matchSet);
            tryCount++;
            tryColors?.Disable();
            if (colorAndPositionMatch == 4) EndGame(true);
            else if (tryCount == 6) EndGame(false);
            else NewTry();
        }

        private void SelectColor(object sender, RoutedEventArgs e)
        {
            tryColors.ChangeColor((sender as ColorButton)?.Color);
            palettePopup.IsOpen = false;
            if (tryColors.AllColorsAreSelected)
                checkButton.Style = Resources["checkBtnStyle"] as Style;
        }

        private void FillPalette()
        {
            palette.Children.Clear();
            foreach (var color in GameColors.AllColors)
            {
                var colorBtn = new ColorButton();
                colorBtn.Color = color;
                colorBtn.Template = Resources["buttonTemplate"] as ControlTemplate;
                colorBtn.Click += SelectColor;
                palette.Children.Add(colorBtn);
            }
        }
    }
}