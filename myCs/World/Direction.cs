using System;

namespace LifeSimulation.myCs.World
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

        private static readonly int[][] Vectors = new int[9][];
        

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
            return Vectors[direction] ?? 
                   (Vectors[direction] = new[]{direction % 3 - 1, direction / 3 - 1});
        }

        public static int[] GetDirectionVector(int[] vector)
        {
            return GetDirectionVector(GetDirectionByVector(vector));
        }

        public static int[] GetDirectionVector(int[] coords1, int[] coords2)
        {
            return GetDirectionVector(new[]{
                coords2[0] - coords1[0],
                coords2[1] - coords1[1]
            });
        }
        public static int[] GetOrthogonalDirection(int[] vector)
        {
            var newVector = GetDirectionVector(vector);
            if ((newVector[0] == newVector[1] || newVector[0] == -newVector[1]) && 
                newVector[0] != 0) 
                newVector[1] = 0;
            return newVector;
        }
        
        public static int[] GetOrthogonalDirection(int[] coords1, int[] coords2)
        {
            return GetOrthogonalDirection(new[]{
                coords2[0] - coords1[0],
                coords2[1] - coords1[1]
            });
        }

        public static int GetDirectionByVector(int[] vector)
        {
            var x = vector[0];
            var y = vector[1];
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
            return GetDirectionVector((1 - Sign(vector[0])) * 3 + (Sign(vector[1]) + 1));
        }

        private static int Sign(int value)
        {
            if (value < 0) return -1;
            if (value > 0) return 1;
            return 0;
        }

        public static bool CheckEqual(int[] vector1, int[] vector2)
        {
            return (vector1[0] == vector2[0]) && (vector1[1] == vector2[1]);
        }

        public static int SqrLength(int[] point1, int[] point2)
        {
            int x = point1[0] - point2[0];
            int y = point1[1] - point2[1];
            return x * x + y * y;
        }
    }
}