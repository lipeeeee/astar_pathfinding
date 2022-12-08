#pragma warning disable CA2211 // Non-constant fields should not be visible
namespace astar_pathfinding
{
    public static class globals
    {
        public static int[,] matrix;

        public static int CELL_SIZE = 30;

        public static int X_SIZE = 0;
        public static int Y_SIZE = 0;

        public static int STRAIGHT_COST = 10; // cost to move foward
        public static int DIAGONAL_COST = 14; // cost to move in diagonal

        public static int[] start_ij = new int[2] { -1 , -1};
        public static int[] end_ij = new int[2] { -1 , -1};

        public static bool diagonal = false;

        public static Dictionary<string, int> MATRIX_VALUES = new()
        {
            {"empty", 0},
            {"start", 1},
            {"end", 2},
            {"wall", 3},
            {"path", 4 },
            {"close", 5 },
            {"open", 6 }
        };
    }
}
#pragma warning restore CA2211 // Non-constant fields should not be visible
