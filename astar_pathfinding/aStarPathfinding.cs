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
        public List<int[]> pathToEnd = new();
        public Node[,] matrixNodes = new Node[globals.X_SIZE, globals.Y_SIZE];

        public int[] start_ij = new int[2];
        public int[] end_ij = new int[2];

        public bool getPath()
        {
            bool endFound = false;
            List<Node> searchedNodes = new();
            int[] cur_ij = start_ij;
            Node lowestFCostNode;
            uint maxIterations = (uint)globals.X_SIZE * (uint)globals.Y_SIZE;

            for (uint i = 0; i < maxIterations && endFound == false; i++)
            {
                // Search around
                searchedNodes.AddRange(getNeighbours(cur_ij));
                searchedNodes = removeDuplicateNodes(searchedNodes);

                // get lowest f cost
                lowestFCostNode = searchedNodes[0];
                for (int j = 1; j < searchedNodes.Count; j++)
                {
                    if (searchedNodes[j].f <= lowestFCostNode.f)
                        if (searchedNodes[j].f == lowestFCostNode.f)
                            lowestFCostNode = (searchedNodes[j].h < lowestFCostNode.h) ? searchedNodes[j] : lowestFCostNode;
                        else
                            lowestFCostNode = searchedNodes[j];
                }

                // pursue it
                cur_ij = lowestFCostNode.ij;
                matrix[cur_ij[0], cur_ij[1]] = globals.MATRIX_VALUES["path"];

                if ((cur_ij[0] == end_ij[0]) && (cur_ij[1] == end_ij[1]))
                    endFound = true;
            }

            return endFound;
        }

        private Node calculateCost(int[] ij)
        {
            Node node = new()
            {
                ij = ij,
                g = getGCost(ij),
                h = getHCost(ij)
            };
            node.f = node.g + node.h;
            return node;
        }

        private List<Node> getNeighbours(int[] ij)
        {
            List<Node> neighbours = new();
            Node neighbourTemp;
            int offset;

            // left column
            offset = ij[1] - 1;
            for (int i = 0; i < 3; i++)
            {
                neighbourTemp = calculateCost(new int[] { ij[0] - 1, offset });
                neighbours.Add(neighbourTemp);

                offset += 1;
            }

            // top row
            offset = ij[0] - 1;
            for (int i = 0; i < 3; i++)
            {
                neighbourTemp = calculateCost(new int[] { offset, ij[1] - 1 });
                neighbours.Add(neighbourTemp);

                offset += 1;
            }

            // right column
            offset = ij[1] - 1;
            for (int i = 0; i < 3; i++)
            {
                neighbourTemp = calculateCost(new int[] { ij[0] + 1, offset });
                neighbours.Add(neighbourTemp);

                offset += 1;
            }

            // down row
            offset = ij[0] - 1;
            for (int i = 0; i < 3; i++)
            {
                neighbourTemp = calculateCost(new int[] { offset, ij[1] + 1 });
                neighbours.Add(neighbourTemp);

                offset += 1;
            }

            // normalize and remove duplicates
            neighbours = removeDuplicateNodes(neighbours);
            neighbours = normalizeNodes(neighbours);

            return neighbours;
        }

        // My implementation of the heuritics calculation
        private int getHCost(int[] ij)
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

        private int getGCost(int[] ij)
        {
            int gCost;
            int diff0, diff1; //difference in dimensions

            // Calculate differences
            if (ij[0] > start_ij[0])
                diff0 = ij[0] - start_ij[0];
            else
                diff0 = start_ij[0] - ij[0];

            if (ij[1] > start_ij[1])
                diff1 = ij[1] - start_ij[1];
            else
                diff1 = start_ij[1] - ij[1];

            // Check for exception
            if (diff0 < diff1)
                (diff0, diff1) = (diff1, diff0);

            diff0 -= diff1;
            gCost = (diff0 * 10) + (diff1 * 14);

            return gCost;
        }

        private static List<Node> removeDuplicateNodes(List<Node> nodes)
        {
            List<Node> distinctNodes = new();
            List<int[]> distinctIJ = new();
            bool addReady;

            for (int i = 0; i < nodes.Count; i++)
            {
                addReady = true;
                foreach (int[] iIJ in distinctIJ)
                {
                    if (iIJ[0] == nodes[i].ij[0] && iIJ[1] == nodes[i].ij[1])
                        addReady = false;
                }

                if (addReady)
                {
                    distinctNodes.Add(nodes[i]);
                    distinctIJ.Add(new int[] { nodes[i].ij[0], nodes[i].ij[1] });
                }
            }

            return distinctNodes;
        }

        private static List<Node> normalizeNodes(List<Node> nodes)
        {
            List<Node> normalizedNodes = new();

            foreach (Node node in nodes)
            {
                if ((node.ij[0] >= 0 && node.ij[1] >= 0) &&
                    (node.ij[0] <= globals.Y_SIZE && node.ij[1] <= globals.X_SIZE))
                    normalizedNodes.Add(node);
            }

            return normalizedNodes;
        }
    }
}
