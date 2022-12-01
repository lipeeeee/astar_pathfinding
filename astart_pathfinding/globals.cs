using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astart_pathfinding
{
    public static class globals
    {
        public static Dictionary<string, int> matrixValues = new Dictionary<string, int>()
        {
            {"empty", 0},
            {"start", 1 },
            {"end", 2},
            {"wall", 3}
        };
    }
}
