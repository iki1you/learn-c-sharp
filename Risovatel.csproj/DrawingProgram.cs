using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Drawer
    {
        static float x, y;
        static Graphics grafics;

        public static void Initialize(Graphics newGrafics)
        {
            grafics = newGrafics;
            grafics.SmoothingMode = SmoothingMode.None;
            grafics.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void DrawSide(Pen color, int size, double angle)
        {
            Drawer.MakeIt(color, size * 0.375f, angle);
            Drawer.MakeIt(color, size * 0.04f * Math.Sqrt(2), angle + Math.PI / 4);
            Drawer.MakeIt(color, size * 0.375f, angle + Math.PI);
            Drawer.MakeIt(color, size * 0.375f - size * 0.04f, angle + Math.PI / 2);

            Drawer.Change(size * 0.04f, angle - Math.PI);
            Drawer.Change(size * 0.04f * Math.Sqrt(2), angle + 3 * Math.PI / 4);
        }

        public static void MakeIt(Pen pen, double lenth, double angle)
        {
            var x1 = (float)(x + lenth * Math.Cos(angle));
            var y1 = (float)(y + lenth * Math.Sin(angle));
            grafics.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double lenth, double angle)
        {
            x = (float)(x + lenth * Math.Cos(angle));
            y = (float)(y + lenth * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int width, int height, Graphics grafics)
        {
            Drawer.Initialize(grafics);

            var size = Math.Min(width, height);

            var lengthDiagonal = Math.Sqrt(2) * (size * 0.375f + size * 0.04f) / 2;
            var x0 = (float)(lengthDiagonal * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(lengthDiagonal * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;

            Drawer.SetPosition(x0, y0);

            Drawer.DrawSide(Pens.Yellow, size, 0);
            Drawer.DrawSide(Pens.Yellow, size, -Math.PI / 2);
            Drawer.DrawSide(Pens.Yellow, size, Math.PI);
            Drawer.DrawSide(Pens.Yellow, size, Math.PI / 2);
        }
    }
}