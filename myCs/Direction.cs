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

        public static int[] GetDirectionVector(int[] direction)
        {
            return new[]{Signum(direction[0]), Signum(direction[1])};
        }

        public static int GetDirectionByVector(int[] vector)
        {
            var y = Signum(vector[0]) + 1;
            var x = Signum(vector[1]) + 1;
            return 3 * y + x;
        }

        private static int Signum(int value)
        {
            return value switch{
                <0 => -1,
                0 => 0,
                >0 => 1
            };
        }
    }
}