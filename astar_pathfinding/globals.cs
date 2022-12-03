namespace astar_pathfinding
{
    public static class globals
    {
        public static int CELL_SIZE = 30;

        public static int X_SIZE = 0;
        public static int Y_SIZE = 0;

        public static Dictionary<string, int> MATRIX_VALUES = new Dictionary<string, int>()
        {
            {"empty", 0},
            {"start", 1},
            {"end", 2},
            {"wall", 3}
        };
    }
}
