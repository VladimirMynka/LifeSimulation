using System.Drawing;
using System.IO;

namespace LifeSimulation.myCs.Drawer
{
    public static class Pictures
    {
        private static readonly string Path = Directory.GetCurrentDirectory() + @"\sources\pictures\";
        
        public static Image Herbivore = Image.FromFile(Path + @"animals\herbivore.png");
        public static Image Predator = Image.FromFile(Path + @"animals\predator.png");
        public static Image Omnivore = Image.FromFile(Path + @"animals\omnivore.png");
        public static Image Scavenger = Image.FromFile(Path + @"animals\scavenger.png");
        public static Image Meat = Image.FromFile(Path + @"animals\meat.png");
        public static Image Egg = Image.FromFile(Path + @"animals\egg.png");

        public static Image Plant = Image.FromFile(Path + @"plants\plant.png");
        public static Image UneatablePlant = Image.FromFile(Path + @"plants\uneatable-plant.png");
        public static Image PoisonousPlant = Image.FromFile(Path + @"plants\poisonous-plant.png");
        public static Image DeadPlant = Image.FromFile(Path + @"plants\dead-plant.png");

        public static Image Fruit = Image.FromFile(Path + @"plants\fruit.png");


        public static Color AnimalColor = GetAverage(Omnivore);
        public static Color PlantColor = GetAverage(Plant);
        public static Color FruitColor = GetAverage(Fruit);
        public static Color NoneColor = Color.Black;

        public static Image Get(string name)
        {
            switch (name)
            {
                case "Animal":
                    return Omnivore;
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