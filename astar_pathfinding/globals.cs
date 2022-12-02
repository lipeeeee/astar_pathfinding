using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astar_pathfinding
{
    public static class globals
    {
        public static int cellSize = 30;
        
        public static int xSize = 0;
        public static int ySize = 0;

        public static Dictionary<string, int> matrixValues = new Dictionary<string, int>()
        {
            {"empty", 0},
            {"start", 1},
            {"end", 2},
            {"wall", 3}
        };
    }
}
