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
        int matrix_x, matrix_y;
        int[,] matrix;
        
        public Form1()
        {
            InitializeComponent();

            // Initialize Coordinates
            this.WindowState = FormWindowState.Maximized;
            this.MaximumSize = new Size(this.Width, this.Height);
            matrix = new int[this.Width, this.Height];
        }

        private void drawMatrix()
        {
            // Draw matrix 
            SolidBrush myBrush = new SolidBrush(Color.Black);
            Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.FillRectangle(myBrush, Rectangle.Empty);
            myBrush.Dispose();
            formGraphics.Dispose();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawMatrix();
            updateDimensions();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            utils.fillMatrix(matrix, globals.matrixValues["empty"]);
        }

        private void updateDimensions()
        {
            matrix_x = this.Width;
            matrix_y = this.Height;
            lblMatrixEndpoints.Text = matrix_x + "," + matrix_y;      
        }

        private void overrideMatrix()
        {
            int newX = this.Width;
            int newY = this.Height;
            int[,] newMatrix = new int[this.Width, this.Height];

            // transfer values to new matrix
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }

        }
    }
}