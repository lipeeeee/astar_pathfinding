namespace astar_pathfinding
{
    public static class utils
    {
        public static void fillBidemensionalMatrix(int[,] matrix, int value)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = value;
                }
            }
        }

        public static void debugMatrixValues(int[,] matrix)
        {
            Random rnd = new();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rnd.Next(0, 4);
                }
            }
        }

        public static int[] getCell(int[,] matrix, int x, int y)
        {
            int[] cellXY = new int[2] { -1, -1 };
            int cur_x = 0, cur_y = 0;

            // Brute force(figure out better way later)
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    // AABB collision
                    if (cur_x < x + globals.CELL_SIZE && cur_x + globals.CELL_SIZE > x &&
                        cur_y < y + globals.CELL_SIZE && cur_y + globals.CELL_SIZE > y)
                        return new int[2] { i, j };

                    cur_x += globals.CELL_SIZE;
                }
                cur_x = 0;
                cur_y += globals.CELL_SIZE;
            }

            return cellXY;
        }

        public static void switchIntegers(ref int a, ref int b)
        {
            (b, a) = (a, b);
        }
    }
}
