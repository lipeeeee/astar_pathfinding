using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace astart_pathfinding
{
    public partial class Form1 : Form
    {
        public int[,] matrix;
        public List<int[]> subMatrixAdd; // cells to add when mousedown
        private bool mouseDown = false;
        private bool leftClick = false;

        public Form1()
        {
            InitializeComponent();

            // Non-Nullable warning
            matrix = new int[0,0];
            subMatrixAdd = new List<int[]>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize Coordinates
            this.MaximumSize = new Size(this.Width, this.Height);
            matrix = new int[this.Width, this.Height];
            globals.xSize = this.Width; 
            globals.ySize = this.Height;
            
            utils.fillBidemensionalMatrix(matrix, globals.matrixValues["empty"]);
            // utils.debugMatrixValues(matrix);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            updateDimensions();
            renderMatrix();
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

        // Color mapping
        private void drawMatrixValues()
        {
            SolidBrush blackBrush = new(Color.Black);
            SolidBrush redBrush = new(Color.Red);
            SolidBrush greenBrush = new(Color.Green);
            SolidBrush emptyBrush = new(Color.WhiteSmoke);
            Graphics formGraphics = this.CreateGraphics();

            int cur_x = 0, cur_y = 0;

            // Color draw matrix values
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == globals.matrixValues["empty"]) 
                    {
                        formGraphics.FillRectangle(emptyBrush, new Rectangle(cur_x, cur_y, globals.cellSize, globals.cellSize));
                    }
                    else if (matrix[i, j] == globals.matrixValues["wall"])
                    {
                        formGraphics.FillRectangle(blackBrush, new Rectangle(cur_x, cur_y, globals.cellSize, globals.cellSize));
                    }
                    else if (matrix[i, j] == globals.matrixValues["start"])
                    {
                        formGraphics.FillRectangle(greenBrush, new Rectangle(cur_x, cur_y, globals.cellSize, globals.cellSize));
                    }
                    else if (matrix[i, j] == globals.matrixValues["end"])
                    {
                        formGraphics.FillRectangle(redBrush, new Rectangle(cur_x, cur_y, globals.cellSize, globals.cellSize));
                    }
                    else
                        throw new Exception("matrix unbound value");

                    cur_x += globals.cellSize;
                }
                cur_x = 0;
                cur_y += globals.cellSize;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // Brute force to know which cell clicked on
            int[] ij = utils.getCell(matrix, e.X, e.Y);
            if (e.Button == MouseButtons.Left)
                matrix[ij[0], ij[1]] = globals.matrixValues["wall"];
            else
                matrix[ij[0], ij[1]] = globals.matrixValues["empty"];

            renderMatrix();
        }

        private void renderMatrix()
        {
            drawMatrixValues();
            drawGrid();
        }

        private void updateDimensions()
        {
            lblMatrixEndpoints.Text = this.Width + "," + this.Height;      
        }
  
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            leftClick = (e.Button == MouseButtons.Left);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                subMatrixAdd.Add(new int[] { e.X, e.Y });
            }
            else
            {
                if (subMatrixAdd.Count > 0)
                {
                    foreach (int[] cell in subMatrixAdd)
                    {
                        int[] ij = utils.getCell(matrix, cell[0], cell[1]);
                        if (leftClick)
                            matrix[ij[0], ij[1]] = globals.matrixValues["wall"];
                        else
                            matrix[ij[0], ij[1]] = globals.matrixValues["empty"];
                    }

                    renderMatrix();
                    subMatrixAdd = new List<int[]>();
                } 
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            utils.fillBidemensionalMatrix(matrix, globals.matrixValues["empty"]);
            renderMatrix();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E) 
            {
                // check if end exists already
                bool foundEnd = false;
                for (int i = 0; i < matrix.GetLength(0) && foundEnd == false; i++)
                {
                    for (int j = 0; j < matrix.GetLength(1) && foundEnd == false; j++)
                    {
                        if (matrix[i, j] == globals.matrixValues["end"])
                        {
                            matrix[i, j] = globals.matrixValues["empty"];
                            foundEnd = true;
                        }
                    }
                }

                // get cell
                Point relativePoint = this.PointToClient(Cursor.Position);
                int[] cell = utils.getCell(matrix, relativePoint.X, relativePoint.Y);
                
                matrix[cell[0], cell[1]] = globals.matrixValues["end"];
                renderMatrix();
            }
            else if (e.KeyCode == Keys.S)
            {
                // check if start exists already
                bool foundStart = false;
                for (int i = 0; i < matrix.GetLength(0) && foundStart == false; i++)
                {
                    for (int j = 0; j < matrix.GetLength(1) && foundStart == false; j++)
                    {
                        if (matrix[i, j] == globals.matrixValues["start"])
                        {
                            matrix[i, j] = globals.matrixValues["empty"];
                            foundStart = true;
                        }
                    }
                }

                // get cell
                Point relativePoint = this.PointToClient(Cursor.Position);
                int[] cell = utils.getCell(matrix, relativePoint.X, relativePoint.Y);

                matrix[cell[0], cell[1]] = globals.matrixValues["start"];
                renderMatrix();
            }
            else if (e.KeyCode == Keys.C)
            {
                // send click
                btnClear_Click(new object(), new EventArgs());
            }
        }

        private void getMatrixEndpoints(ref int[] start_ij, ref int[] end_ij)
        {
            int c = 0; // stop flag
            for (int i = 0; i < matrix.GetLength(0) && (c < 2); i++)
            {
                for (int j = 0; j < matrix.GetLength(1) && (c < 2); j++)
                {
                    if (matrix[i,j] == globals.matrixValues["end"])
                    {
                        end_ij[0] = i;
                        end_ij[1] = j;
                        c++;
                    }
                    else if (matrix[i, j] == globals.matrixValues["start"])
                    {
                        start_ij[0] = i;
                        start_ij[1] = j;
                        c++;                    
                    }
                }
            }
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            int[] start_ij = new int[2], end_ij = new int[2];
            getMatrixEndpoints(ref start_ij, ref end_ij);

            aStarPathfinding aStar = new aStarPathfinding(matrix, start_ij, end_ij);
            MessageBox.Show(aStar.fastHeuritics(new int[] { 1, 1 }).ToString()); 
            ///aStar.fastHeuritics(new int[] {0, 1});
        }
    }
}