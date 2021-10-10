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

        public static Brush White = Brushes.Khaki;
        public static Brush Black = Brushes.Black;//Brushes.Bisque;
        public static Brush Plant1 = Brushes.Green;//Brushes.Chartreuse;
        public static Brush Plant2 = Brushes.Green;
        public static Brush Plant3 = Brushes.Green;//Brushes.SpringGreen;
        public static Brush Animal1 = Brushes.Gold;//Brushes.Brown;
        public static Brush Animal2 = Brushes.Gold;//Brushes.Goldenrod;
        public static Brush Animal3 = Brushes.Gold;//Brushes.OrangeRed;

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
                default: return White;
            }
        }
    }
}