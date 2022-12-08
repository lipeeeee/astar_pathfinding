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

        private class Node
        {
            public int[] ij; // matrix coords
            public int g; // g = distance from starting node (g cost)
            public int h; // h = how far away node is from end node (heuristic)
            public int f; // f = g + h
            public unsafe Node? parent; // parent node

            public Node()
            {

            }

            public Node(int[] ij)
            {
                this.ij = ij;
            }

            // override object.Equals
            public override bool Equals(object? obj)
            {
                return Equals(obj as Node);
            }

            public bool Equals(Node? obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                return obj.ij[0] == ij[0] && obj.ij[1] == ij[1];
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        public int[,] matrix;
        public int[] start_ij = new int[2];
        public int[] end_ij = new int[2];

        // https://mat.uab.cat/~alseda/MasterOpt/AStar-Algorithm.pdf
        // https://en.wikipedia.org/wiki/A*_search_algorithm
        public bool betterGetPath()
        {
            Node lowestCost;
            Node? current;
            List<Node> neighbours, open = new()
            {
                // Add start node to open list
                new()
                {
                    f = 0,
                    h = 0,
                    g = 0,
                    ij = start_ij,
                    parent = null
                }
            }, close = new();

            while (open.Count > 0)
            {
                // Get lowest f cost in open list
                lowestCost = getLowestFCost(open);

                _ = open.Remove(lowestCost);
                close.Add(lowestCost);

                // Found end
                if (lowestCost.ij[0] == end_ij[0] && lowestCost.ij[1] == end_ij[1])
                {
                    // color explored nodes
                    foreach (Node node in close)
                    {
                        matrix[node.ij[0], node.ij[1]] = globals.MATRIX_VALUES["explored"];
                    }
                    foreach (Node node in open)
                    {
                        matrix[node.ij[0], node.ij[1]] = globals.MATRIX_VALUES["explored"];
                    }

                    // color path
                    current = lowestCost;
                    while (current != null)
                    {
                        matrix[current.ij[0], current.ij[1]] = globals.MATRIX_VALUES["path"];
                        current = current.parent;
                    }

                    return true;
                }

                // Get neighbours
                neighbours = getNeighbours(lowestCost);
                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (open.Contains(neighbours[i]) || close.Contains(neighbours[i]))
                    {
                        continue;
                    }

                    open.Add(neighbours[i]);
                }
            }

            return false;
        }

        private Node calculateCost(int[] ij, Node? parent)
        {
            Node node = new()
            {
                ij = ij,
                h = getHCost(ij),
                g = getGCost(ij),
                parent = parent
            };

            node.f = node.g + node.h;
            return node;
        }

        private List<Node> getNeighbours(Node node)
        {
            List<Node> neighbours = new();
            int[] ij = node.ij;

            // get neighbours around node
            neighbours.Add(calculateCost(new int[] { ij[0] - 1, ij[1] }, node));
            neighbours.Add(calculateCost(new int[] { ij[0] + 1, ij[1] }, node));
            neighbours.Add(calculateCost(new int[] { ij[0], ij[1] - 1 }, node));
            neighbours.Add(calculateCost(new int[] { ij[0], ij[1] + 1 }, node));

            // normalize and remove duplicates
            neighbours = removeDuplicateNodes(neighbours);
            neighbours = normalizeNodes(neighbours);
            neighbours = removeWalls(neighbours);

            return neighbours;
        }

        private int getHCost(int[] ij)
        {
            int h;
            int diff0, diff1; //difference in dimensions

            // Calculate differences
            diff0 = ij[0] > end_ij[0] ? ij[0] - end_ij[0] : end_ij[0] - ij[0];

            diff1 = ij[1] > end_ij[1] ? ij[1] - end_ij[1] : end_ij[1] - ij[1];

            // Check for exception
            if (diff0 < diff1)
            {
                (diff0, diff1) = (diff1, diff0);
            }

            diff0 -= diff1;
            h = (diff0 * 10) + (diff1 * 14);

            return h;
        }

        private int getGCost(int[] ij)
        {
            int gCost;
            int diff0, diff1; //difference in dimensions

            // Calculate differences
            diff0 = ij[0] > start_ij[0] ? ij[0] - start_ij[0] : start_ij[0] - ij[0];

            diff1 = ij[1] > start_ij[1] ? ij[1] - start_ij[1] : start_ij[1] - ij[1];

            // Check for exception
            if (diff0 < diff1)
            {
                (diff0, diff1) = (diff1, diff0);
            }

            diff0 -= diff1;
            gCost = (diff0 * 10) + (diff1 * 14);

            return gCost;
        }

        private Node getLowestFCost(List<Node> nodes)
        {
            Node lowestFCost = nodes[0];

            for (int i = 1; i < nodes.Count; i++)
            {
                if (nodes[i].f <= lowestFCost.f)
                {
                    lowestFCost = nodes[i].f == lowestFCost.f
                        ? (nodes[i].h < lowestFCost.h) ? nodes[i] : lowestFCost
                        : nodes[i];
                }
            }

            return lowestFCost;
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
                    {
                        addReady = false;
                    }
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
                if (node.ij[0] >= 0 && node.ij[1] >= 0 &&
                    node.ij[0] <= globals.Y_SIZE && node.ij[1] <= globals.X_SIZE)
                {
                    normalizedNodes.Add(node);
                }
            }

            return normalizedNodes;
        }

        private List<Node> removeWalls(List<Node> nodes)
        {
            List<Node> removedWalls = new();

            foreach (Node node in nodes)
            {
                if (matrix[node.ij[0], node.ij[1]] != globals.MATRIX_VALUES["wall"])
                {
                    removedWalls.Add(node);
                }
            }

            return removedWalls;
        }
    }
}
