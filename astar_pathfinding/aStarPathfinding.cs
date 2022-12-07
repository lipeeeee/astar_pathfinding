using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

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
            public override bool Equals(object? obj) => this.Equals(obj as Node);

            public bool Equals(Node? obj)
            {
                if (obj == null || this.GetType() != obj.GetType())
                    return false;

                if (obj.ij[0] == this.ij[0] && obj.ij[1] == this.ij[1])
                    return true;

                return false;
            }

            public override int GetHashCode() => base.GetHashCode();
        }

        public int[,] matrix;
        public int[] start_ij = new int[2];
        public int[] end_ij = new int[2];

        // Returns path
        public unsafe bool getPath()
        {
            bool endFound = false;
            uint maxIterations = (uint)globals.X_SIZE * (uint)globals.Y_SIZE;
            List<Node> unExploredNodes = new(), tempUnExplored;
            List<Node> exploredNodes = new(){
                calculateCost(start_ij, null)
            };
            Node lowestFCostNode, current = exploredNodes[0];
            current.parent = null;

            for (uint i = 0; i < maxIterations && endFound == false; i++)
            {
                // Search 
                unExploredNodes.AddRange(getNeighbours(current));
                unExploredNodes = removeDuplicateNodes(unExploredNodes);
                unExploredNodes = subtractLists(unExploredNodes, exploredNodes);
                if (unExploredNodes.Count == 0)
                {
                    return false;
                }
                else if (1 == 1) // deeperSearch
                {
                    tempUnExplored = unExploredNodes;
                    foreach (Node node in tempUnExplored)
                    {
                        unExploredNodes.AddRange(getNeighbours(node));
                        unExploredNodes = removeDuplicateNodes(unExploredNodes);
                        unExploredNodes = subtractLists(unExploredNodes, exploredNodes);
                    }
                }

                // get lowest f cost
                lowestFCostNode = unExploredNodes[0];
                for (int j = 1; j < unExploredNodes.Count; j++)
                {
                    if (unExploredNodes[j].f <= lowestFCostNode.f)
                    {
                        lowestFCostNode = unExploredNodes[j].f == lowestFCostNode.f
                            ? (unExploredNodes[j].h < lowestFCostNode.h) ? unExploredNodes[j] : lowestFCostNode
                            : unExploredNodes[j];
                    }
                }

                // pursue it
                current = lowestFCostNode;

                exploredNodes.Add(lowestFCostNode);
                matrix[current.ij[0], current.ij[1]] = globals.MATRIX_VALUES["explored"];

                if ((current.ij[0] == end_ij[0]) && (current.ij[1] == end_ij[1]))
                {
                    endFound = true;
                }
            }

            // get path
            _ = reversePath(current);

            // remove explored and unexplored nodes
            if (endFound == false)
            {
                removeExploredUnexploredNodes(matrix);
            }

            return endFound;
        }

        //https://mat.uab.cat/~alseda/MasterOpt/AStar-Algorithm.pdf
        // https://en.wikipedia.org/wiki/A*_search_algorithm
        public bool betterGetPath()
        {
            Node lowestCost;
            Node? current;
            List<Node> open = new(); // nodes that have been visited but not expanded
            List<Node> close = new(); // nodes that have been visited and expanded
            List<Node> neighbours;
            int tentativeScore;

            // Add start node to open list
            open.Add(new()
            {
                f = 0,
                h = 0,
                g = 0,
                ij = start_ij,
                parent = null
            });
            
            while (open.Count > 0)
            {
                // Get lowest f cost in open list
                lowestCost = getLowestFCost(open);

                open.Remove(lowestCost);
                close.Add(lowestCost);

                // Found end
                if (lowestCost.ij[0] == end_ij[0] && lowestCost.ij[1] == end_ij[1])
                {
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
                    /*tentativeScore = lowestCost.g + d(neighbours[i].ij, lowestCost.ij);
                    if (tentativeScore < neighbours[i].g)
                    {
                        neighbours[i].parent = lowestCost;
                        neighbours[i].g = tentativeScore;
                        neighbours[i].f = tentativeScore + neighbours[i].g;

                        if (open.Contains(neighbours[i]) == false)
                            open.Add(neighbours[i]);
                    }*/
                    tentativeScore = lowestCost.g + d(neighbours[i].ij, lowestCost.ij);
                    neighbours[i].g = tentativeScore;

                    if (open.Contains(neighbours[i]))
                    {
                        if (neighbours[i].g <= tentativeScore)
                            continue;
                    }
                    else if (close.Contains(neighbours[i]))
                    {
                        if (neighbours[i].g <= tentativeScore)
                            continue;
                        close.Remove(neighbours[i]);
                        open.Add(neighbours[i]);
                    }
                    else
                        open.Add(neighbours[i]);

                }
            }

            return false;
        }

        private unsafe List<int[]> reversePath(Node current)
        {
            List<int[]> reversedPath = new();

            while (current.parent != null)
            {
                reversedPath.Add(current.ij);
                matrix[current.ij[0], current.ij[1]] = globals.MATRIX_VALUES["path"];
                current = current.parent;
            }

            return reversedPath;
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
            Node neighbourTemp;
            int[] ij = node.ij;
            int offset;

            // left column
            offset = ij[1] - 1;
            for (int i = 0; i < 3; i++)
            {
                neighbourTemp = calculateCost(new int[] { ij[0] - 1, offset }, node);
                neighbours.Add(neighbourTemp);

                offset += 1;
            }

            // top row
            offset = ij[0] - 1;
            for (int i = 0; i < 3; i++)
            {
                neighbourTemp = calculateCost(new int[] { offset, ij[1] - 1 }, node);
                neighbours.Add(neighbourTemp);

                offset += 1;
            }

            // right column
            offset = ij[1] - 1;
            for (int i = 0; i < 3; i++)
            {
                neighbourTemp = calculateCost(new int[] { ij[0] + 1, offset }, node);
                neighbours.Add(neighbourTemp);

                offset += 1;
            }

            // down row
            offset = ij[0] - 1;
            for (int i = 0; i < 3; i++)
            {
                neighbourTemp = calculateCost(new int[] { offset, ij[1] + 1 }, node);
                neighbours.Add(neighbourTemp);

                offset += 1;
            }

            // normalize and remove duplicates
            neighbours = removeDuplicateNodes(neighbours);
            neighbours = normalizeNodes(neighbours);
            neighbours = removeWalls(neighbours);

            return neighbours;
        }

        private int newGetH(int[] ij)
        {
            int HxDiff = Math.Abs(end_ij[0] - ij[0]);
            int HyDiff = Math.Abs(end_ij[1] - ij[1]);
            return HxDiff + HyDiff;
        }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        private int newGetG(Node node)
        {
            if (node == null)
                return -1;

            int GxMoveCost = node.ij[0] - node.parent.ij[0];
            int GyMoveCost = node.ij[1] - node.parent.ij[1];
            int gCost = node.parent.g;

            if (GxMoveCost != 0 && GyMoveCost != 0)
                gCost += 14;
            else
                gCost += 10;

            return gCost;
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        // My implementation of the heuritics calculation
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

        private int d(int[] start, int[] finish)
        {
            int dCost;
            int diff0, diff1; //difference in dimensions

            // Calculate differences
            diff0 = start[0] > finish[0] ? start[0] - finish[0] : finish[0] - start[0];
            diff1 = start[1] > finish[1] ? start[1] - finish[1] : finish[1] - start[1];

            // Check for exception
            if (diff0 < diff1)
            {
                (diff0, diff1) = (diff1, diff0);
            }

            diff0 -= diff1;
            dCost = (diff0 * 10) + (diff1 * 14);

            return dCost;
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

        private int searchOpen(List<Node> open, int[] ij)
        {
            int index = -1;
            for (int i = 0; i < open.Count; i++)
            {
                if (open[i].ij[0] == ij[0] && open[i].ij[1] == ij[1])
                {
                    index = i;
                    break;
                }
            }
            return index;
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

        // list1 - list2
        // O(mn)
        private static List<Node> subtractLists(List<Node> list1, List<Node> list2)
        {
            List<Node> newList = new();
            bool coordsExist;
            for (int i = 0; i < list1.Count; i++)
            {
                coordsExist = false;
                for (int j = 0; j < list2.Count && coordsExist == false; j++)
                {
                    if ((list1[i].ij[0] == list2[j].ij[0]) &&
                        (list1[i].ij[1] == list2[j].ij[1]))
                    {
                        coordsExist = true;
                    }
                }

                if (coordsExist == false)
                {
                    newList.Add(list1[i]);
                }
            }

            return newList;
        }

        private static void removeExploredUnexploredNodes(int[,] matrix)
        {
            utils.removeBidimensionalMatrixValue(matrix, globals.MATRIX_VALUES["explored"], globals.MATRIX_VALUES["empty"]);
            utils.removeBidimensionalMatrixValue(matrix, globals.MATRIX_VALUES["path"], globals.MATRIX_VALUES["empty"]);
        }
    }
}
