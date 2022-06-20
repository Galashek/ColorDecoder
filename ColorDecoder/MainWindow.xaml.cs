﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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
            foreach (var color in GameColors.AllColors)
            {
                var colorButton = new Ellipse
                {
                    Fill = color,
                    Margin = new Thickness(2),
                    Stroke = GameColors.StrokeColor
                };
                colorButton.MouseDown += SelectColor;
                palette.Children.Add(colorButton);
            }
            StartGame(null, null);
        }
        private void StartGame(object sender, RoutedEventArgs e)
        {
            tryCount = 0;
            TryResults.Children.Clear();
            Tries.Children.Clear();
            Answer.Children.Clear();

            CheckButton.IsEnabled = true;
            CheckButton.Visibility = Visibility.Visible;

            colorsToGuess = GameColors.GetRandomColors();
            answerStack = new ColorStack();
            answerStack.Disable();
            Answer.Children.Add(answerStack.Stack);
            //answerStack.SetColors(colorsToGuess);
            
            NewTry();
        }
        private void NewTry()
        {
            var stack = new ColorStack();
            stack.ButtonPressed += () => palettePopup.IsOpen = true;;
            Grid.SetRow(stack.Stack, tryCount);
            Tries.Children.Add(stack.Stack);
            tryStack = stack;
            Grid.SetRow(CheckButton, tryCount);
        }
        private void EndGame()
        {
            CheckButton.IsEnabled = false;
            CheckButton.Visibility = Visibility.Hidden;
            answerStack.SetColors(colorsToGuess);
        }
        private void Check(object sender, RoutedEventArgs e)
        {
            var curStackColors = tryStack.Colors;
            if (curStackColors.Any(c => c == GameColors.EmptyColor)) return;
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
            TryResults.Children.Add(matchStack.Stack);
            tryCount++;
            tryStack?.Disable();
            if (colorAndPositionMatch == 4 || tryCount == 6) EndGame();
            else NewTry();
        }

        private void SelectColor(object sender, MouseButtonEventArgs e)
        {
            tryStack.ChangeColor((sender as Ellipse)?.Fill);
            palettePopup.IsOpen = false;
        }
    }
}