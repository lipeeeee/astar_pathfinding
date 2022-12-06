using System.Drawing.Drawing2D;
using System.Globalization;

namespace astar_pathfinding
{
    public static class utils
    {
        public static void fillBidimensionalMatrix(int[,] matrix, int value)
        {
            // Backwards for loop for XY coord migration
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = value;
                }
            }
        }

        public static void removeBidimensionalMatrixValue(int[,] matrix, int oldValue, int newValue)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == oldValue)
                    {
                        matrix[i, j] = newValue;
                    }
                }
            }
        }

        public static void randomMaze(int[,] matrix)
        {
            Random rnd = new();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rnd.Next(0, 2) == 0 ? 0 : 3;
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

        public static void switchIntegers(ref int a, ref int b)
        {
            (b, a) = (a, b);
        }

        // i,j\nmatrix(x)^
        public static string[] exportMatrix(int[,] matrix) 
        {
            // Clean matrix
            removeBidimensionalMatrixValue(matrix, globals.MATRIX_VALUES["path"], globals.MATRIX_VALUES["empty"]);
            removeBidimensionalMatrixValue(matrix, globals.MATRIX_VALUES["explored"], globals.MATRIX_VALUES["empty"]);

            List<string> result = new()
            {
                matrix.GetLength(0).ToString(),
                matrix.GetLength(1).ToString()
            };

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result.Add(matrix[i, j].ToString());
                }
            }

            return result.ToArray();
        }

        public static string[] importMatrix(string[] lines) 
        {
            int iLen = Int32.Parse(lines[0]);
            int jLen = Int32.Parse(lines[1]);

            List<string> result = new()
            {
                lines[0].ToString(),
                lines[1].ToString()
            };

            for (int i = 2; )

            return result.ToArray();
        }
    }
}
