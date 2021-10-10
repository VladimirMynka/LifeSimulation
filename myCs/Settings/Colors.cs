using System.Drawing;

namespace LifeSimulation.myCs
{
    public static class Colors
    {
        public const int WhiteConst = 0;
        public const int BlackConst = 1;
        public const int Plant1Const = 2;
        public const int Plant2Const = 3;
        public const int Plant3Const = 4;
        public const int Animal1Const = 5;
        public const int Animal2Const = 6;
        public const int Animal3Const = 7;
        public const int DiedPlant1Const = 8;
        public const int Tree1Const = 9;
        public const int Poisonous1Const = 10;

        public static Brush White = Brushes.Khaki;
        public static Brush Black = Brushes.Black;//Brushes.Bisque;
        public static Brush Plant1 = Brushes.Lime;//Brushes.Chartreuse;
        public static Brush Plant2 = Brushes.Lime;
        public static Brush Plant3 = Brushes.Lime;//Brushes.SpringGreen;
        public static Brush Animal1 = Brushes.Blue;//Brushes.Brown;
        public static Brush Animal2 = Brushes.Blue;//Brushes.Goldenrod;
        public static Brush Animal3 = Brushes.Blue;//Brushes.OrangeRed;
        public static Brush DiedPlant1 = Brushes.Green;
        public static Brush Tree1 = Brushes.Brown;
        public static Brush Poisonous1 = Brushes.Olive;


        public static Brush GetBrush(int colorIndex)
        {
            switch (colorIndex)
            {
                case WhiteConst: return White;
                case BlackConst: return White;
                case Plant1Const: return Plant1;
                case Plant2Const: return Plant2;
                case Plant3Const: return Plant3;
                case Animal1Const: return Animal1;
                case Animal2Const: return Animal2;
                case Animal3Const: return Animal3;
                case DiedPlant1Const: return DiedPlant1;
                case Tree1Const: return Tree1;
                case Poisonous1Const: return Poisonous1;
                default: return White;
            }
        }
    }
}