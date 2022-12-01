using System.Runtime.CompilerServices;

namespace astart_pathfinding
{
    /*
        1. Draw matrix
        
    */

    /*
        - Start maximized
    */

    public partial class Form1 : Form
    {
        public int[,] matrix;
        
        public Form1()
        {
            InitializeComponent();

            // Non-Nullable warning
            matrix = new int[0,0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize Coordinates
            this.MaximumSize = new Size(this.Width, this.Height);
            matrix = new int[this.Width, this.Height];

            utils.fillBidemensionalMatrix(matrix, globals.matrixValues["empty"]);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawGrid();
            updateDimensions();
        }

        private void drawGrid()
        {
            SolidBrush myBrush = new(Color.Black);
            Graphics formGraphics = this.CreateGraphics();
            Pen p = new(myBrush);

            // Draw matrix grid 
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                // Vertical
                formGraphics.DrawLine(p, i * globals.cellSize, 0, i * globals.cellSize, matrix.GetLength(0) * globals.cellSize);

                // Horizontal
                formGraphics.DrawLine(p, 0, i * globals.cellSize, matrix.GetLength(1) * globals.cellSize, i * globals.cellSize);
            }

            myBrush.Dispose();
            formGraphics.Dispose();
        }

        // Color drawing
        private void drawMatrixValues()
        {
            SolidBrush blackBrush = new(Color.Black);
            SolidBrush redBrush = new(Color.Red);
            SolidBrush greenBrush = new(Color.Green);
            Graphics formGraphics = this.CreateGraphics();

            int cur_x = 0, cur_y = 0;

            // Color draw matrix values
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == globals.matrixValues["empty"]) 
                    { 

                    }
                    else if (matrix[i, j] == globals.matrixValues["wall"])
                    {

                    }
                    else if (matrix[i, j] == globals.matrixValues["start"])
                    {

                    }
                    else if (matrix[i, j] == globals.matrixValues["end"])
                    {

                    }
                    else
                        throw new Exception("matrix unbound value");

                    cur_x += globals.cellSize;
                }
                cur_x = 0;
                cur_y += globals.cellSize;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(matrix[0, 1] + "");
        }

        private void updateDimensions()
        {
            lblMatrixEndpoints.Text = this.Width + "," + this.Height;      
        }
    }
}