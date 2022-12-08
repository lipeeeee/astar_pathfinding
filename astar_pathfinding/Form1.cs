using astar_pathfinding.AStar;

namespace astar_pathfinding
{
    public partial class Form1 : Form
    {
        public List<int[]> subMatrixAdd; // cells to add when mousedown
        private bool mouseDown = false;
        private bool leftClick = false;

        public Form1()
        {
            InitializeComponent();

            // Non-Nullable warning
            globals.matrix = new int[0, 0];
            subMatrixAdd = new List<int[]>();
        }

        private void onLoad(object sender, EventArgs e)
        {
            // Initialize Coordinates
            MaximumSize = new Size(Width, Height);

            globals.X_SIZE = Width / 30;
            globals.Y_SIZE = Height / 30;
            globals.matrix = new int[Width, Height];

            utils.fillBidimensionalMatrix(globals.MATRIX_VALUES["empty"]);
        }

        private void drawGrid()
        {
            SolidBrush myBrush = new(Color.Black);
            Graphics formGraphics = CreateGraphics();
            int len0 = globals.matrix.GetLength(0);
            int len1 = globals.matrix.GetLength(1);
            Pen p = new(myBrush);

            // Draw matrix grid 
            for (int i = 0; i < globals.matrix.GetLength(0); i++)
            {
                // Vertical
                formGraphics.DrawLine(p, i * globals.CELL_SIZE, 0, i * globals.CELL_SIZE, len0 * globals.CELL_SIZE);

                // Horizontal
                formGraphics.DrawLine(p, 0, i * globals.CELL_SIZE, len1 * globals.CELL_SIZE, i * globals.CELL_SIZE);
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
            SolidBrush darkGreenBrush = new(Color.DarkGreen);
            SolidBrush emptyBrush = new(Color.WhiteSmoke);
            SolidBrush cyanBrush = new(Color.DarkCyan);
            SolidBrush orangeBrush = new(Color.DarkOrange);
            Graphics formGraphics = CreateGraphics();

            int cur_x = 0, cur_y = 0;

            // Color draw matrix values
            for (int i = 0; i < globals.matrix.GetLength(0); i++)
            {
                for (int j = 0; j < globals.matrix.GetLength(1); j++)
                {
                    if (globals.matrix[i, j] == globals.MATRIX_VALUES["empty"])
                    {
                        formGraphics.FillRectangle(emptyBrush, new Rectangle(cur_x, cur_y, globals.CELL_SIZE, globals.CELL_SIZE));
                    }
                    else if (globals.matrix[i, j] == globals.MATRIX_VALUES["wall"])
                    {
                        formGraphics.FillRectangle(blackBrush, new Rectangle(cur_x, cur_y, globals.CELL_SIZE, globals.CELL_SIZE));
                    }
                    else if (globals.matrix[i, j] == globals.MATRIX_VALUES["start"])
                    {
                        formGraphics.FillRectangle(greenBrush, new Rectangle(cur_x, cur_y, globals.CELL_SIZE, globals.CELL_SIZE));
                    }
                    else if (globals.matrix[i, j] == globals.MATRIX_VALUES["end"])
                    {
                        formGraphics.FillRectangle(redBrush, new Rectangle(cur_x, cur_y, globals.CELL_SIZE, globals.CELL_SIZE));
                    }
                    else if (globals.matrix[i, j] == globals.MATRIX_VALUES["path"])
                    {
                        formGraphics.FillRectangle(cyanBrush, new Rectangle(cur_x, cur_y, globals.CELL_SIZE, globals.CELL_SIZE));
                    }
                    else if (globals.matrix[i, j] == globals.MATRIX_VALUES["close"])
                    {
                        formGraphics.FillRectangle(orangeBrush, new Rectangle(cur_x, cur_y, globals.CELL_SIZE, globals.CELL_SIZE));
                    }
                    else if (globals.matrix[i,j] == globals.MATRIX_VALUES["open"])
                    {
                        formGraphics.FillRectangle(darkGreenBrush, new Rectangle(cur_x, cur_y, globals.CELL_SIZE, globals.CELL_SIZE));
                    }
                    else
                    {
                        throw new Exception("matrix unbound value");
                    }

                    cur_x += globals.CELL_SIZE;
                }
                cur_x = 0;
                cur_y += globals.CELL_SIZE;
            }

            blackBrush.Dispose();
            redBrush.Dispose();
            emptyBrush.Dispose();
            darkGreenBrush.Dispose();
            cyanBrush.Dispose();
            greenBrush.Dispose();
            orangeBrush.Dispose();
            formGraphics.Dispose();
        }

        private void renderMatrix()
        {
            drawMatrixValues();
            drawGrid();
        }

        private void onMouseClick(object sender, MouseEventArgs e)
        {
            // Brute force to know which cell clicked on
            int[] ij = utils.getCell(e.X, e.Y);
            globals.matrix[ij[0], ij[1]] = e.Button == MouseButtons.Left ? globals.MATRIX_VALUES["wall"] : globals.MATRIX_VALUES["empty"];

            renderMatrix();
        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            leftClick = e.Button == MouseButtons.Left;
        }

        private void onMouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void onMouseMove(object sender, MouseEventArgs e)
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
                        int[] ij = utils.getCell(cell[0], cell[1]);
                        globals.matrix[ij[0], ij[1]] = leftClick ? globals.MATRIX_VALUES["wall"] : globals.MATRIX_VALUES["empty"];
                    }

                    renderMatrix();
                    subMatrixAdd = new List<int[]>();
                }
                int[] cell2 = utils.getCell(e.X, e.Y);
                if (cell2 == globals.start_ij)
                {
                    globals.start_ij = new int[] { -1, -1 };
                }
                else if (cell2 == globals.end_ij)
                {
                    globals.end_ij = new int[] { -1, -1 };
                }

                lblCell.Text = cell2[0] + ", " + cell2[1];
            }
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E)
            {
                // check if end exists already
                bool foundEnd = false;
                for (int i = 0; i < globals.matrix.GetLength(0) && foundEnd == false; i++)
                {
                    for (int j = 0; j < globals.matrix.GetLength(1) && foundEnd == false; j++)
                    {
                        if (globals.matrix[i, j] == globals.MATRIX_VALUES["end"])
                        {
                            globals.matrix[i, j] = globals.MATRIX_VALUES["empty"];
                            foundEnd = true;
                        }
                    }
                }

                // get cell
                Point relativePoint = PointToClient(Cursor.Position);
                int[] cell = utils.getCell(relativePoint.X, relativePoint.Y);
                globals.end_ij = cell;
                globals.matrix[cell[0], cell[1]] = globals.MATRIX_VALUES["end"];
                renderMatrix();
            }
            else if (e.KeyCode == Keys.S)
            {
                // check if start exists already
                bool foundStart = false;
                for (int i = 0; i < globals.matrix.GetLength(0) && foundStart == false; i++)
                {
                    for (int j = 0; j < globals.matrix.GetLength(1) && foundStart == false; j++)
                    {
                        if (globals.matrix[i, j] == globals.MATRIX_VALUES["start"])
                        {
                            globals.matrix[i, j] = globals.MATRIX_VALUES["empty"];
                            foundStart = true;
                        }
                    }
                }

                // get cell
                Point relativePoint = PointToClient(Cursor.Position);
                int[] cell = utils.getCell(relativePoint.X, relativePoint.Y);
                globals.start_ij = cell;
                globals.matrix[cell[0], cell[1]] = globals.MATRIX_VALUES["start"];
                renderMatrix();
            }
            else if (e.KeyCode == Keys.C)
            {
                // send click
                btnClear_Click(new object(), new EventArgs());
            }
            else if (e.KeyCode == Keys.Enter)
            {
                // send click
                btnSearchClick(new object(), new EventArgs());
            }
            else if (e.KeyCode == Keys.M)
            {
                // send click
                btnMaze_Click(new object(), new EventArgs());
            }
        }

        private void onPaint(object sender, PaintEventArgs e)
        {
            renderMatrix();
        }

        private static int getMatrixEndpoints()
        {
            int c = 0; // stop flag
            for (int i = 0; i < globals.matrix.GetLength(0) && (c < 2); i++)
            {
                for (int j = 0; j < globals.matrix.GetLength(1) && (c < 2); j++)
                {
                    if (globals.matrix[i, j] == globals.MATRIX_VALUES["end"])
                    {
                        globals.end_ij[0] = i;
                        globals.end_ij[1] = j;
                        c++;
                    }
                    else if (globals.matrix[i, j] == globals.MATRIX_VALUES["start"])
                    {
                        globals.start_ij[0] = i;
                        globals.start_ij[1] = j;
                        c++;
                    }
                }
            }

            return c;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            utils.fillBidimensionalMatrix(globals.MATRIX_VALUES["empty"]);
            globals.start_ij = new int[2] { -1, -1 };
            globals.end_ij = new int[2] { -1, -1 };
            renderMatrix();

            // better ui handling
            _ = btnSearch.Focus();
        }

        private void btnSearchClick(object sender, EventArgs e)
        {
            // double check start and end node
            int c = getMatrixEndpoints();
            if (c != 2)
            {
                _ = MessageBox.Show("Missing start and/or end node", "Missing endpoint", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool found;
            aStarPathfinding aStar = new();
            if (globals.diagonal)
                found = aStar.getDiagonalPath();
            else
                found = aStar.getPath();

            if (found)
            {
                globals.matrix[globals.end_ij[0], globals.end_ij[1]] = globals.MATRIX_VALUES["end"];
                globals.matrix[globals.start_ij[0], globals.start_ij[1]] = globals.MATRIX_VALUES["start"];
            }
            else
            {
                _ = MessageBox.Show("Did not find path to end node", "No Path", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            renderMatrix();
        }

        private void btnMaze_Click(object sender, EventArgs e)
        {
            utils.randomMaze();
            renderMatrix();
            _ = btnSearch.Focus();
        }

        private void importMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new()
            {
                Filter = "Text Files|*.txt",
                Title = "Save Matrix to file"
            };
            _ = openFile.ShowDialog();

            // If the file name is not an empty string open it for saving.   
            if (openFile.FileName != "")
            {
                // Reads matrix via a FileSteam
                string[] lines = File.ReadAllLines(openFile.FileName);
                utils.importMatrix(lines);
                renderMatrix();
            }
        }

        private void exportMatrixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new()
            {
                Filter = "Text Files|*.txt",
                Title = "Save Matrix to file"
            };
            _ = saveFile.ShowDialog();

            // If the file name is not an empty string open it for saving.   
            if (saveFile.FileName != "")
            {
                // Saves the matrix via a FileStream
                File.WriteAllLines(saveFile.FileName, utils.exportMatrix());
            }
        }

        private void checkBoxDiagonal_CheckedChanged(object sender, EventArgs e)
        {
            globals.diagonal = checkBoxDiagonal.Checked;
        }
    }
}