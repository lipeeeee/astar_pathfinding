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
    }
}
