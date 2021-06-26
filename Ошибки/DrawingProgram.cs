using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    public static class Painter
    {
        static float startX, startY;
        static Graphics graphics;

        public static void Initialize(Graphics newGraphics)
        {
            graphics = newGraphics;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.Clear(Color.Black);
        }

        public static void SetStartPosition(float x, float y)
        {
            startX = x; 
            startY = y; 
        }

        public static void DrawLine(Pen pen, double length, double angle)
        {
            var x = (float)(startX + length * Math.Cos(angle));
            var y = (float)(startY + length * Math.Sin(angle));
            
            graphics.DrawLine(pen, startX, startY, x, y);
            SetStartPosition(x, y);
        }

        public static void ChangeStartPosition(double length, double angle)
        {
            startX = (float)(startX + length * Math.Cos(angle));
            startY = (float)(startY + length * Math.Sin(angle));
        }
    }

    public static class ImpossibleSquare
    {
        public static void DrawSide(Pen color, int length, double angle)
        {
            Painter.DrawLine(color, length * 0.375f, angle);
            Painter.DrawLine(color, length * Math.Sqrt(2) * 0.04f, angle + Math.PI / 4);
            Painter.DrawLine(color, length * 0.375f, angle +  Math.PI);
            Painter.DrawLine(color, length * 0.335f, angle + Math.PI / 2);

            Painter.ChangeStartPosition(length * 0.04f, angle - Math.PI);
            Painter.ChangeStartPosition(length * 0.04f * Math.Sqrt(2), angle +  Math.PI * 0.75f);
        }
        
        public static void Draw(int width, int height, double angleRotate, Graphics graphics)
        {
            Painter.Initialize(graphics);

            var length = Math.Min(width, height);
            var diagonalLength = ((Math.Sqrt(2) * length) * 0.415f) / 2;

            var x = (float) (diagonalLength * Math.Cos(Math.PI * 1.25f)) + width / 2f;
            var y = (float) (diagonalLength * Math.Sin(Math.PI * 1.25f)) + height / 2f;

            Painter.SetStartPosition(x, y);

            DrawSide(Pens.Yellow, length, 0);
            DrawSide(Pens.Yellow, length, -Math.PI / 2);
            DrawSide(Pens.Yellow, length, Math.PI);
            DrawSide(Pens.Yellow, length, Math.PI / 2);
        }
    }
}