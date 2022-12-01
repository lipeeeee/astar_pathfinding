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
            drawMatrix();
            updateDimensions();
        }

        private void drawMatrix()
        {
            SolidBrush myBrush = new(Color.Black);
            Graphics formGraphics = this.CreateGraphics();
            Pen p = new Pen(myBrush);

            // Draw matrix 
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