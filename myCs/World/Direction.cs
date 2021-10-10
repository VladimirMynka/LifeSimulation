using System;

namespace LifeSimulation.myCs
{
    public static class Direction
    {
        public const int TopLeft = 0;
        public const int Top = 1;
        public const int TopRight = 2;
        public const int Left = 3;
        public const int None = 4;
        public const int Right = 5;
        public const int BottomLeft = 6;
        public const int Bottom = 7;
        public const int BottomRight = 8;

        public static int GetRandomDirection()
        {
            return World.Random.Next(9);
        }

        public static int[] GetRandomDirectionVector()
        {
            return GetDirectionVector(GetRandomDirection());
        }

        public static int[] GetDirectionVector(int direction)
        {
            return new[]{direction / 3 - 1, direction % 3 - 1};
        }

        public static int[] GetDirectionVector(int[] vector)
        {
            return new[]{Sign(vector[0]), Sign(vector[1])};
        }

        public static int GetDirectionByVector(int[] vector)
        {
            var y = vector[0];
            var x = vector[1];
            var absX = Math.Abs(x);
            var absY = Math.Abs(y);
            if (y >= 2 * absX) return Bottom;
            if (y <= -2 * absX) return Top;
            if (x >= 2 * absY) return Right;
            if (x <= -2 * absY) return Left;
            if (y > 0) return x > 0 ? BottomRight : BottomLeft; 
            if (y < 0) return x < 0 ? TopRight : TopLeft;
            return None;
        }

        public static int[] GetNormalDirection(int[] vector)
        {
            return new[]{-Sign(vector[1]), Sign(vector[0])};
        }

        private static int Sign(int value)
        {
            if (value < 0) return -1;
            if (value > 0) return 1;
            return 0;
        }
    }
}