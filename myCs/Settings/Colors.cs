using System.Drawing;

namespace LifeSimulation.myCs.Settings
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
        public const int UneatablePlant1Const = 9;
        public const int PoisonousPlant1Const = 10;
        public const int NormalFruit1Const = 11;
        public const int PoisonousFruit1Const = 12;
        public const int UneatableFruit1Const = 13;
        public const int RotFruit1Const = 14;
        public const int Animal4Const = 15;
        public const int Animal5Const = 16;
        public const int Animal6Const = 17;
        public const int Animal7Const = 18;
        public const int Animal8Const = 19;
        public const int Animal9Const = 20;

        public static Brush White = Brushes.Khaki;
        public static Brush Black = Brushes.Black;
        public static Brush Plant1 = Brushes.Lime;
        public static Brush Plant2 = Brushes.Lime;
        public static Brush Plant3 = Brushes.Lime;
        public static Brush Animal1 = Brushes.Blue;
        public static Brush Animal2 = Brushes.Orchid;
        public static Brush Animal3 = Brushes.Indigo;
        public static Brush Animal4 = Brushes.MediumVioletRed;
        public static Brush Animal5 = Brushes.Aqua;
        public static Brush Animal6 = Brushes.PaleVioletRed;
        public static Brush DiedPlant1 = Brushes.Green;
        public static Brush UneatablePlant1 = Brushes.Brown;
        public static Brush PoisonousPlant1 = Brushes.Olive;
        public static Brush NormalFruit1 = Brushes.Gold;
        public static Brush PoisonousFruit1 = Brushes.Red;
        public static Brush UneatableFruit1 = Brushes.Chocolate;
        public static Brush RotFruit1 = Brushes.Tomato;


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
                case Animal4Const: return Animal4;
                case Animal5Const: return Animal5;
                case Animal6Const: return Animal6;
                case DiedPlant1Const: return DiedPlant1;
                case UneatablePlant1Const: return UneatablePlant1;
                case PoisonousPlant1Const: return PoisonousPlant1;
                case NormalFruit1Const: return NormalFruit1;
                case PoisonousFruit1Const: return PoisonousFruit1;
                case UneatableFruit1Const: return UneatableFruit1;
                case RotFruit1Const: return RotFruit1;
                default: return White;
            }
        }
    }
}