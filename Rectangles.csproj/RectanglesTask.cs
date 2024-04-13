using System;

namespace Rectangles
{
    public static class RectanglesTask
    {
        public static bool SideIntersected(int coord1, int coord2, int coord3, int coord4)
        {
            return (coord1 <= coord2) && (coord2 <= coord3) ||
                (coord2 <= coord1) && (coord1 <= coord4);
        }

        public static bool SideInRectangle(int coord1, int coord2, int coord3, int coord4)
        {
            return coord1 >= coord2 && coord3 >= coord4;
        }

        public static int SquareInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (IndexOfInnerRectangle(r1, r2) == 0)
                return r1.Height * r1.Width;
            return r2.Height * r2.Width;
        }

        public static int SideOfRectangle(int coord1, int coord2, int coord3, int coord4)
        {
            return Math.Min(coord1, coord2) - Math.Max(coord3, coord4);
        }

        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            if (SideIntersected(r1.Left, r2.Left, r1.Right, r2.Right))
                if (SideIntersected(r1.Top, r2.Top, r1.Bottom, r2.Bottom))
                    return true;
            return false;
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            if (!AreIntersected(r1, r2))
                return 0;
            if (IndexOfInnerRectangle(r1, r2) != -1)
                return SquareInnerRectangle(r1, r2);
            return (SideOfRectangle(r1.Bottom, r2.Bottom, r1.Top, r2.Top))
                * SideOfRectangle(r1.Right, r2.Right, r1.Left, r2.Left);
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {

            if (SideInRectangle(r2.Top, r1.Top, r1.Bottom, r2.Bottom) &&
                SideInRectangle(r1.Right, r2.Right, r2.Left, r1.Left))
                return 1;
            if (SideInRectangle(r1.Top, r2.Top, r2.Bottom, r1.Bottom) &&
                SideInRectangle(r2.Right, r1.Right, r1.Left, r2.Left))
                return 0;
            return -1;
        }
    }
}