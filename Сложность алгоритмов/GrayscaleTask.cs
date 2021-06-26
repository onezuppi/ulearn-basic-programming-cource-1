namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
        {
            var (width, height) = (original.GetLength(0), original.GetLength(1));
            var grayscale = new double[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    grayscale[x, y] = ConvertColorToShadow(original[x, y].R, original[x, y].G, original[x, y].B);
                }
            }
            
            return grayscale;
        }

        private static double ConvertColorToShadow(int red, int green, int blue)
		{
            return (0.299 * red + 0.587 * green + 0.114 * blue) / 255;
        }
    }
}