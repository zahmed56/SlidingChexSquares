using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Layout;
using Avalonia;
using System;

internal class ChexWindow
{
    const float boxWidth = 160;
    const float boxHeight = 90;
    const int screenWidth = 1280;
    const int screenHeight = 720;
    public Window win;

    static int arrayLength = 5;

    static int arrayHeight = 4;

    Box[,] array = new Box[arrayHeight, arrayLength];
    int gridSize;

    Color color = RealColor.Color(1f, 0, 0);
    int degrees = 2;
    private Canvas canvas;

    public ChexWindow()
    {
        MakeDisplay();
        MakeBox();
    }

    private void OnPointerPressedBox()
    {
        Animate.Run(DoFrame, 33, 450);
    }

    private void DoFrame()
    {
        for (int rowCounter = 0; rowCounter < arrayLength; rowCounter++)
        {
            for (int colCounter = 0; colCounter < arrayHeight; colCounter++)
            {
                Box current = array[colCounter, rowCounter];
                int turnSeverity = rowCounter + colCounter;
                float angle = (float)((degrees * turnSeverity) * (Math.PI / 180));
                current.Rotate(angle);
                current.Move(new Vector(2 - rowCounter, 1.5 - colCounter));
            }
        }
    }

    private void MakeDisplay()
    {
        win = new Window
        {
            Title = "ChexWindow v0.1",
            Height = screenHeight,
            Width = screenWidth,
            Background = Brushes.Magenta,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            CanResize = false,
        };

        canvas = new Canvas
        {
            Background = Brushes.Black,
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
        };
        win.Content = canvas;

        win.PointerPressed += (s, a) => OnPointerPressedBox();

        win.Show();
    }

    private void MakeBox()
    {
        int xGap = screenWidth / arrayLength;

        int yGap = screenHeight / arrayHeight;

        gridSize = arrayHeight * arrayLength;

        for (int rowCounter = 0; rowCounter < arrayLength; rowCounter++)
        {
            for (int colCounter = 0; colCounter < arrayHeight; colCounter++)
            {
                int x = (int)(boxWidth * .8f + (xGap * rowCounter));
                int y = (int)(boxHeight + (yGap * colCounter));

                array[colCounter, rowCounter] = new Box
                (boxWidth, boxHeight, new Vector(x, y), color);

                canvas.Children.Add(array[colCounter, rowCounter].Polygon);
            }

        }

    }
}