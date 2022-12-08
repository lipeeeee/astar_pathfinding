using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace astar_pathfinding.AStar
{
    public class Node
    {
        // Propreties
        public int[] ij // matrix coords
        {
            get { return ij; }
            set { ij = value; }
        } 
        
        public int g // g = distance from starting node (g cost)
        {
            get { return g; }
            set { g = value; }
        }

        public int h // h = how far away node is from end node (heuristic)
        {
            get { return h; }
            set { h = value; }
        }

        public int f // f = g + h
        {
            get { return g + this.h; }
            set { f = value; }
        } 

        public Node? parent // parent node
        {
            get { return parent; }
            set { parent = value; }
        }

        public Node(int[] ij, Node? parent)
        {
            this.ij = ij;
            this.parent = parent;
            if (globals.diagonal)
            {
                this.h = (int)Math.Round(euclideanH());
            }
            else
            {
                this.h = heuristic();
            }

            if (parent != null)
            {
                this.g = d(ij, parent.ij);
            }
            else
            {
                this.g = 0;
            }

            // this.f = this.h + this.g;
        }

        // Methods
        private int heuristic()
        {
            return d(this.ij, globals.end_ij);
        }

        private double euclideanH()
        {
            int HxDiff = Math.Abs(globals.end_ij[0] - ij[0]);
            int HyDiff = Math.Abs(globals.end_ij[1] - ij[1]);
            int hCost = HxDiff + HyDiff;
            return hCost;
            /*int dx = Math.Abs(ij[0] - end_ij[0]);
            int dy = Math.Abs(ij[1] - end_ij[1]);
            return 10 * Math.Sqrt(dx * dx + dy * dy);*/
        }

        public List<Node> getNeighbours()
        {
            List<Node> neighbours = new();

            if (!globals.diagonal)
            {
                // get neighbours around node
                neighbours.Add(new Node(new int[] { this.ij[0] - 1, this.ij[1] }, this));
                neighbours.Add(new Node(new int[] { this.ij[0] + 1, this.ij[1] }, this));
                neighbours.Add(new Node(new int[] { this.ij[0], this.ij[1] - 1 }, this));
                neighbours.Add(new Node(new int[] { this.ij[0], this.ij[1] + 1 }, this));
            }
            else
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }
                        neighbours.Add(new Node(new int[] { ij[0] + i, ij[1] + j }, this));
                    }
                }
            }

            return neighbours;
        }

        public void updateHeuristic()
        {
            if (globals.diagonal)
            {
                this.g = (int)euclideanH();
            }
            else
            {
                this.g = heuristic();
            }
        }

        private static int d(int[] ij, int[] xy)
        {
            int dx = Math.Abs(ij[0] - xy[0]);
            int dy = Math.Abs(ij[1] - xy[1]);
            return globals.STRAIGHT_COST * (dx + dy) + (globals.DIAGONAL_COST - 2 * globals.STRAIGHT_COST) * Math.Min(dx, dy);
            /*int h;
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
            h = diff0 * 10 + diff1 * 14;
          
            return h;*/
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
}
