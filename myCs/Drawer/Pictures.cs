using System.Drawing;
using System.IO;

namespace LifeSimulation.myCs.Drawer
{
    public static class Pictures
    {
        private static readonly string Path = Directory.GetCurrentDirectory() + @"\..\..\sources\pictures\";
        
        public static Image Animal = Image.FromFile(Path + "animal.png");
        public static Image Plant = Image.FromFile(Path + "plant.png");
        public static Image Fruit = Image.FromFile(Path + "fruit.png");

        public static Color AnimalColor = GetAverage(Animal);
        public static Color PlantColor = GetAverage(Plant);
        public static Color FruitColor = GetAverage(Fruit);
        public static Color NoneColor = Color.Black;

        public static Image Get(string name)
        {
            switch (name)
            {
                case "Animal":
                    return Animal;
                case "Plant":
                    return Plant;
                case "Fruit":
                    return Fruit;
                default:
                    return null;
            }
        }

        public static Color GetColor(string name)
        {
            switch (name)
            {
                case "Animal":
                    return AnimalColor;
                case "Plant":
                    return PlantColor;
                case "Fruit":
                    return FruitColor;
                default:
                    return NoneColor;
            }
        }

        private static Color GetAverage(Image image)
        {
            int alpha = 0;
            int red = 0;
            int green = 0;
            int blue = 0;
            Bitmap bitmap = (Bitmap) image;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    alpha += color.A;
                    red += color.R;
                    green += color.G;
                    blue += color.B;
                }
            }

            int divider = bitmap.Width * bitmap.Height;
            return Color.FromArgb(
                alpha / divider, 
                red / divider, 
                green / divider, 
                blue / divider
                );
        }
    }
}