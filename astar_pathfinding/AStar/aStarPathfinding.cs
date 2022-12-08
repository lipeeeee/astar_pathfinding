namespace astar_pathfinding.AStar
{
    public class aStarPathfinding
    {
        public aStarPathfinding()
        {
        }

        // https://mat.uab.cat/~alseda/MasterOpt/AStar-Algorithm.pdf
        // https://en.wikipedia.org/wiki/A*_search_algorithm
        public bool getPath()
        {
            Node lowestCost;
            List<Node> neighbours, open = new()
            {
                new Node(globals.start_ij, null)
            }, close = new();

            while (open.Count > 0)
            {
                // Get lowest f cost in open list
                lowestCost = getLowestFCost(open);

                open.Remove(lowestCost);
                close.Add(lowestCost);

                // Found end
                if (lowestCost.ij[0] == globals.end_ij[0] && lowestCost.ij[1] == globals.end_ij[1])
                {
                    retracePath(lowestCost, open, close);
                    return true;
                }

                // Get neighbours
                neighbours = getNormalizedNeighbours(lowestCost);
                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (!open.Contains(neighbours[i]) && !close.Contains(neighbours[i]))
                    {
                        open.Add(neighbours[i]);
                    }
                }
            }

            return false;
        }

        public bool getDiagonalPath()
        {
            int newG, dx, dy;
            bool newPath;
            Node lowestCost;
            List<Node> neighbours, open = new()
            {
                new Node(globals.start_ij, null)
            }, close = new();

            while (open.Count > 0)
            {
                // Get lowest f cost in open list
                lowestCost = getLowestFCost(open);

                _ = open.Remove(lowestCost);
                close.Add(lowestCost);

                // Found end
                if (lowestCost.ij[0] == globals.end_ij[0] && lowestCost.ij[1] == globals.end_ij[1])
                {
                    retracePath(lowestCost, open, close);
                    return true;
                }

                // Get neighbours
                neighbours = getNormalizedNeighbours(lowestCost);
                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (close.Contains(neighbours[i]))
                    {
                        continue;
                    }

                    newPath = false;
                    dx = lowestCost.ij[0] - neighbours[i].ij[0];
                    dy = lowestCost.ij[1] - neighbours[i].ij[1];
                    newG = dx != 0 && dy != 0 ? lowestCost.g + 14 : lowestCost.g + 10;
                    if (open.Contains(neighbours[i]))
                    {
                        if (newG < neighbours[i].g)
                        {
                            neighbours[i].g = newG;
                            newPath = true;
                        }
                    }
                    else
                    {
                        neighbours[i].g = newG;
                        newPath = true;
                        open.Add(neighbours[i]);
                    }

                    if (newPath)
                    {
                        neighbours[i].updateHeuristic();
                        neighbours[i].parent = lowestCost;
                    }
                }
            }

            return false;
        }

        private static void retracePath(Node? cur, List<Node> open, List<Node> close)
        {
            // color explored nodes
            foreach (Node node in close)
            {
                globals.matrix[node.ij[0], node.ij[1]] = globals.MATRIX_VALUES["close"];
            }
            foreach (Node node in open)
            {
                globals.matrix[node.ij[0], node.ij[1]] = globals.MATRIX_VALUES["open"];
            }

            // color path
            while (cur != null)
            {
                globals.matrix[cur.ij[0], cur.ij[1]] = globals.MATRIX_VALUES["path"];
                cur = cur.parent;
            }
        }

        private static List<Node> getNormalizedNeighbours(Node node)
        {
            List<Node> neighbours = node.getNeighbours();

            // normalize and remove duplicates
            neighbours = normalizeNodes(neighbours);
            neighbours = removeWalls(neighbours);

            return neighbours;
        }

        private static Node getLowestFCost(List<Node> nodes)
        {
            Node lowestFCost = nodes[0];

            for (int i = 1; i < nodes.Count; i++)
            {
                if (nodes[i].f <= lowestFCost.f)
                {
                    lowestFCost = nodes[i].f == lowestFCost.f
                        ? nodes[i].h < lowestFCost.h ? nodes[i] : lowestFCost
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

        private static List<Node> removeWalls(List<Node> nodes)
        {
            List<Node> removedWalls = new();

            foreach (Node node in nodes)
            {
                if (globals.matrix[node.ij[0], node.ij[1]] != globals.MATRIX_VALUES["wall"])
                {
                    removedWalls.Add(node);
                }
            }

            return removedWalls;
        }
    }
}
