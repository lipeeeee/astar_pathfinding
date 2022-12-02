using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astart_pathfinding
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
                    matrix[i, j] = rnd.Next(0,4);
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
                    if (cur_x < x + globals.cellSize && cur_x + globals.cellSize > x &&
                        cur_y < y + globals.cellSize && cur_y + globals.cellSize > y) 
                    {
                        return new int[2] { i, j };
                    }

                    cur_x += globals.cellSize;
                }
                cur_x = 0;
                cur_y += globals.cellSize;
            }

            return cellXY;
        }

        public static void switchIntegers(ref int a, ref int b)
        {
            (b, a) = (a, b);
        }
    }
}
