using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace LifeSimulation.myCs.Drawing
{
    public static class Pictures
    {
        private static readonly string Path = Directory.GetCurrentDirectory() + @"\sources\pictures\";
        
        public static Image Herbivore = Image.FromFile(Path + @"animals\herbivore.png");
        public static Image Herbivore2 = Image.FromFile(Path + @"animals\herbivore2.png");
        public static Image Herbivore3 = Image.FromFile(Path + @"animals\herbivore3.png");

        public static Image Predator = Image.FromFile(Path + @"animals\predator.png");
        public static Image Predator2 = Image.FromFile(Path + @"animals\predator2.png");
        public static Image Predator3 = Image.FromFile(Path + @"animals\predator3.png");
        
        public static Image Omnivore = Image.FromFile(Path + @"animals\omnivore.png");
        public static Image Omnivore2 = Image.FromFile(Path + @"animals\omnivore2.png");
        public static Image Omnivore3 = Image.FromFile(Path + @"animals\omnivore3.png");
        
        public static Image Scavenger = Image.FromFile(Path + @"animals\scavenger.png");
        public static Image Scavenger2 = Image.FromFile(Path + @"animals\scavenger2.png");
        public static Image Scavenger3 = Image.FromFile(Path + @"animals\scavenger3.png");

        public static Image Meat = Image.FromFile(Path + @"animals\meat.png");
        public static Image Meat2 = Image.FromFile(Path + @"animals\meat2.png");
        public static Image Meat3 = Image.FromFile(Path + @"animals\meat3.png");
        public static Image Meat4 = Image.FromFile(Path + @"animals\meat4.png");

        public static Image Egg = Image.FromFile(Path + @"animals\egg.png");
        
        public static Image Human = Image.FromFile(Path + @"animals\human.png");
        public static Image Human2 = Image.FromFile(Path + @"animals\human2.png");
        
        public static Image Plant = Image.FromFile(Path + @"plants\plant.png");
        public static Image UneatablePlant = Image.FromFile(Path + @"plants\uneatable-plant.png");
        public static Image PoisonousPlant = Image.FromFile(Path + @"plants\poisonous-plant.png");
        public static Image DeadPlant = Image.FromFile(Path + @"plants\dead-plant.png");

        public static Image Fruit = Image.FromFile(Path + @"plants\fruit.png");
        public static Image UneatableFruit = Image.FromFile(Path + @"plants\uneatable-fruit.png");
        public static Image PoisonousFruit = Image.FromFile(Path + @"plants\poisonous-fruit.png");
        public static Image DeadFruit = Image.FromFile(Path + @"plants\dead-fruit.png");

        public static Image Sleeper = Image.FromFile(Path + @"animals\sleeper.png");

        public static Image Gold = Image.FromFile(Path + @"resources\gold.png");
        public static Image Iron = Image.FromFile(Path + @"resources\iron.png");
        public static Image IronEasy = Image.FromFile(Path + @"resources\iron-easy.png");
        public static Image Compost = Image.FromFile(Path + @"resources\compost.png");
        public static Image CompostEasy = Image.FromFile(Path + @"resources\compost-easy.png");

        public static Image FirstBuildingStage1 = Image.FromFile(Path + @"buildings\warehouse-1-1.png");
        public static Image FirstBuildingStage2 = Image.FromFile(Path + @"buildings\warehouse-1-2.png");
        
        public static Image SecondBuildingStage1 = Image.FromFile(Path + @"buildings\warehouse-2-1.png");
        public static Image SecondBuildingStage2 = Image.FromFile(Path + @"buildings\warehouse-2-2.png");
        public static Image SecondBuildingStage3 = Image.FromFile(Path + @"buildings\warehouse-2-3.png");
        
        public static Image ThirdBuildingStage1 = Image.FromFile(Path + @"buildings\warehouse-3-1.png");
        public static Image ThirdBuildingStage2 = Image.FromFile(Path + @"buildings\warehouse-3-2.png");
        public static Image ThirdBuildingStage3 = Image.FromFile(Path + @"buildings\warehouse-3-3.png");


        public static readonly Image[] FirstBuilding = new Image[]{
            FirstBuildingStage1, FirstBuildingStage2
        };
        public static readonly Image[] SecondBuilding = new Image[]{
            SecondBuildingStage1, SecondBuildingStage2, SecondBuildingStage3
        };
        public static readonly Image[] ThirdBuilding = new Image[]{
            ThirdBuildingStage1, ThirdBuildingStage2, ThirdBuildingStage3
        };

        private static readonly List<Image> AllImages = new List<Image>(){
            Herbivore, Herbivore2, Herbivore3,
            Omnivore, Omnivore2, Omnivore3,
            Predator, Predator2, Predator3,
            Scavenger, Scavenger2, Scavenger3,
            Meat, Meat2, Meat3, Meat4,
            Egg, Human, Human2,
            Plant, UneatablePlant, PoisonousPlant, DeadPlant,
            Fruit, UneatableFruit, PoisonousFruit, DeadFruit,
            Sleeper,
            Gold, Iron, IronEasy, Compost, CompostEasy,
            FirstBuildingStage1, FirstBuildingStage2,
            SecondBuildingStage1, SecondBuildingStage2, SecondBuildingStage3,
            ThirdBuildingStage1, ThirdBuildingStage2, ThirdBuildingStage3
        };

        private static readonly List<Brush> AllBrushes = GetBrushesList(AllImages);

        private static List<Brush> GetBrushesList(IEnumerable<Image> images)
        {
            return images.Select(image => new SolidBrush(GetAverage(image))).Cast<Brush>().ToList();
        }
        public static Brush GetBrushFor(Image image)
        {
            return AllBrushes[AllImages.IndexOf(image)];
        }
        
        private static Color GetAverage(Image image)
        {
            //int alpha = 0;
            int red = 0;
            int green = 0;
            int blue = 0;
            int divider = 1;
            Bitmap bitmap = (Bitmap) image;

            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color color = bitmap.GetPixel(i, j);
                    if (color.A < 120) 
                        continue;
                    //alpha += color.A;
                    red += color.R;
                    green += color.G;
                    blue += color.B;
                    divider++;
                }
            }

            return Color.FromArgb(
                255, 
                red / divider, 
                green / divider, 
                blue / divider
                );
        }
    }
}