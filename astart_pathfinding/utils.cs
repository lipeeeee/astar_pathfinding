using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astart_pathfinding
{
    public static class utils
    {
        public static void fillMatrix(int[,] matrix, int value)
        {
            MessageBox.Show(matrix.GetLength(0).ToString());
            MessageBox.Show(matrix.GetLength(1).ToString());
        }
    }
}
