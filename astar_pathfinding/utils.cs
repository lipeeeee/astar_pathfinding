namespace astar_pathfinding
{
    public static class utils
    {
        public static void fillBidimensionalMatrix(int value)
        {
            // Backwards for loop for XY coord migration
            for (int i = 0; i < globals.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < globals.matrix.GetLength(1); j++)
                {
                    globals.matrix[i, j] = value;
                }
            }
        }

        public static void removeBidimensionalMatrixValue(int oldValue, int newValue)
        {
            for (int i = 0; i < globals.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < globals.matrix.GetLength(1); j++)
                {
                    if (globals.matrix[i, j] == oldValue)
                    {
                        globals.matrix[i, j] = newValue;
                    }
                }
            }
        }

        public static void randomMaze()
        {
            Random rnd = new();

            for (int i = 0; i < globals.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < globals.matrix.GetLength(1); j++)
                {
                    globals.matrix[i, j] = rnd.Next(0, 2) == 0 ? 0 : 3;
                }
            }
        }

        public static void debugMatrixValues()
        {
            Random rnd = new();

            for (int i = 0; i < globals.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < globals.matrix.GetLength(1); j++)
                {
                    globals.matrix[i, j] = rnd.Next(0, 4);
                }
            }
        }

        public static int[] getCell(int x, int y)
        {
            int[] cellXY = new int[2] { -1, -1 };
            int cur_x = 0, cur_y = 0;

            // Brute force(figure out better way later)
            for (int i = 0; i < globals.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < globals.matrix.GetLength(1); j++)
                {
                    // AABB collision
                    if (cur_x < x + globals.CELL_SIZE && cur_x + globals.CELL_SIZE > x &&
                        cur_y < y + globals.CELL_SIZE && cur_y + globals.CELL_SIZE > y)
                    {
                        return new int[2] { i, j };
                    }

                    cur_x += globals.CELL_SIZE;
                }
                cur_x = 0;
                cur_y += globals.CELL_SIZE;
            }

            return cellXY;
        }

        // i,j\nmatrix(x)^
        public static string[] exportMatrix()
        {
            // Clean matrix
            removeBidimensionalMatrixValue(globals.MATRIX_VALUES["path"], globals.MATRIX_VALUES["empty"]);
            removeBidimensionalMatrixValue(globals.MATRIX_VALUES["close"], globals.MATRIX_VALUES["empty"]);
            removeBidimensionalMatrixValue(globals.MATRIX_VALUES["open"], globals.MATRIX_VALUES["empty"]);

            List<string> result = new()
            {
                globals.matrix.GetLength(0).ToString(),
                globals.matrix.GetLength(1).ToString()
            };

            for (int i = 0; i < globals.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < globals.matrix.GetLength(1); j++)
                {
                    result.Add(globals.matrix[i, j].ToString());
                }
            }

            return result.ToArray();
        }

        public static void importMatrix(string[] lines)
        {
            int iLen = int.Parse(lines[0]);
            int jLen = int.Parse(lines[1]);
            lines = lines.Skip(2).ToArray();

            globals.matrix = get2DMatrix(lines, iLen, jLen);
        }

        private static int[,] get2DMatrix(string[] input, int iLen, int jLen)
        {
            int[,] output = new int[iLen, jLen];
            for (int i = 0; i < iLen; i++)
            {
                for (int j = 0; j < jLen; j++)
                {
                    output[i, j] = int.Parse(input[(i * jLen) + j]);
                }
            }
            return output;
        }
    }
}
