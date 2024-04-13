namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
        {
            var lengthX = original.GetLength(0);
            var lengthY = original.GetLength(1);
            var grayScale = new double[lengthX, lengthY];
            for (int i = 0; i < lengthX; i++)
                for (int j = 0; j < lengthY; j++)
                {
                    var pixel = original[i, j];
                    grayScale[i, j] = (0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B) / 255;
                }
            return grayScale;
        }
    }
}