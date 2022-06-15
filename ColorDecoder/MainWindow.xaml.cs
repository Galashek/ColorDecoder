using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorDecoder
{
    public partial class MainWindow
    {
        private ColorStack answerStack;
        private ColorStack tryStack;
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
            GameField.Children.Clear();
            if (GamePanel.Children.Count > 2)
                GamePanel.Children.RemoveAt(2);
            CheckButton.IsEnabled = true;

            colorsToGuess = GetRandomColors();
            answerStack = new ColorStack();
            answerStack.Disable();
            GamePanel.Children.Add(answerStack.Stack);
            // answerStack.SetColors(colorsToGuess);
            
            NewTry();
        }
        private void NewTry()
        {
            tryStack?.Disable();
            var stack = new ColorStack();
            Grid.SetRow(stack.Stack, tryCount);
            Grid.SetColumn(stack.Stack, 1);
            GameField.Children.Add(stack.Stack);
            tryStack = stack;
        }
        private void EndGame()
        {
            CheckButton.IsEnabled = false;
            answerStack.SetColors(colorsToGuess);
            // MessageBox.Show("End");
        }
        private void Check(object sender, RoutedEventArgs e)
        {
            var curStackColors = tryStack.Colors;
            if (curStackColors.Any(c => c == ColorStack.EmptyColor)) return;
            var colorAndPositionMatch = 0;
            var onlyColorMatch = curStackColors.Distinct().Count(color => colorsToGuess.Contains(color));
            for (int i = 0; i < 4; i++)
            {
                if (curStackColors[i] == colorsToGuess[i])
                {
                    colorAndPositionMatch++;
                    onlyColorMatch--;
                }
            }
            var matchStack = new MatchStack(onlyColorMatch, colorAndPositionMatch);
            Grid.SetRow(matchStack.Stack, tryCount);
            Grid.SetColumn(matchStack.Stack, 0);
            GameField.Children.Add(matchStack.Stack);
            tryCount++;
            if (colorAndPositionMatch == 4 || tryCount == 6) EndGame();
            else NewTry();
        }
        private SolidColorBrush[] GetRandomColors()
        {
            var colors = new SolidColorBrush[4];
            var rnd = new Random();
            var colorList = new List<SolidColorBrush>(ColorStack.AllColors);
            for (int i = 0; i < 4; i++)
            {
                var index = rnd.Next(0, colorList.Count);
                colors[i] = colorList[index];
                colorList.RemoveAt(index);
            }
            return colors;
        }
    }
}