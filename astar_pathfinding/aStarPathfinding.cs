using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace astar_pathfinding
{
    public class aStarPathfinding
    {
        public aStarPathfinding(int[,] matrix, int[] start_ij, int[] end_ij)
        {
            this.matrix = matrix;
            this.start_ij = start_ij;
            this.end_ij = end_ij;
        }

        public struct Node
        {
            public int[] ij; // matrix coords
            public int g; // g = distance from starting node (g cost)
            public int h; // h = how far away node is from end node (heuristic)
            public int f; // f = g + h
        }

        public int[,] matrix;
        public Node[,] matrixNodes = new Node[globals.xSize, globals.ySize];

        public int[] start_ij = new int[2];
        public int[] end_ij = new int[2];

        public Node[] calculateCost(int[] ij)
        {
            Node[] aroundNodes = new Node[8];
            //for (int i = 0; i < )

            return aroundNodes;
        }

        // My implementation of the heuritics calculation
        public int fastHeuritics(int[] ij)
        {
            int h;
            int diff0, diff1; //difference in dimensions

            // Calculate differences
            if (ij[0] > end_ij[0])
                diff0 = ij[0] - end_ij[0];
            else
                diff0 = end_ij[0] - ij[0];

            if (ij[1] > end_ij[1])
                diff1 = ij[1] - end_ij[1];
            else
                diff1 = end_ij[1] - ij[1];

            // Check for exception
            if (diff0 < diff1)
                (diff0, diff1) = (diff1, diff0);

            diff0 -= diff1;
            h = (diff0 * 10) + (diff1 * 14);
            
            return h;
        }
    }
}
