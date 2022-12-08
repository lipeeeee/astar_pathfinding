namespace astar_pathfinding
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCell = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMaze = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.matrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMatrixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.presetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxDiagonal = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCell
            // 
            this.lblCell.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCell.AutoSize = true;
            this.lblCell.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCell.Location = new System.Drawing.Point(15, 10);
            this.lblCell.Name = "lblCell";
            this.lblCell.Size = new System.Drawing.Size(64, 17);
            this.lblCell.TabIndex = 1;
            this.lblCell.Text = "MousePos";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.checkBoxDiagonal);
            this.panel1.Controls.Add(this.btnMaze);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.lblCell);
            this.panel1.Location = new System.Drawing.Point(12, 378);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 60);
            this.panel1.TabIndex = 2;
            // 
            // btnMaze
            // 
            this.btnMaze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaze.Location = new System.Drawing.Point(149, 10);
            this.btnMaze.Name = "btnMaze";
            this.btnMaze.Size = new System.Drawing.Size(132, 23);
            this.btnMaze.TabIndex = 6;
            this.btnMaze.TabStop = false;
            this.btnMaze.Text = "Random Maze(M)";
            this.btnMaze.UseVisualStyleBackColor = true;
            this.btnMaze.Click += new System.EventHandler(this.btnMaze_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(287, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(87, 51);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.TabStop = false;
            this.btnSearch.Text = "Search(Enter)";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearchClick);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(149, 34);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(132, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "Clear(C)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.matrixToolStripMenuItem,
            this.toolStripMenuItem1,
            this.presetsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // matrixToolStripMenuItem
            // 
            this.matrixToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importMatrixToolStripMenuItem,
            this.exportMatrixToolStripMenuItem});
            this.matrixToolStripMenuItem.Name = "matrixToolStripMenuItem";
            this.matrixToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.matrixToolStripMenuItem.Text = "Import/Export";
            // 
            // importMatrixToolStripMenuItem
            // 
            this.importMatrixToolStripMenuItem.Name = "importMatrixToolStripMenuItem";
            this.importMatrixToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.importMatrixToolStripMenuItem.Text = "Import Matrix";
            this.importMatrixToolStripMenuItem.Click += new System.EventHandler(this.importMatrixToolStripMenuItem_Click);
            // 
            // exportMatrixToolStripMenuItem
            // 
            this.exportMatrixToolStripMenuItem.Name = "exportMatrixToolStripMenuItem";
            this.exportMatrixToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.exportMatrixToolStripMenuItem.Text = "Export Matrix";
            this.exportMatrixToolStripMenuItem.Click += new System.EventHandler(this.exportMatrixToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // presetsToolStripMenuItem
            // 
            this.presetsToolStripMenuItem.Name = "presetsToolStripMenuItem";
            this.presetsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.presetsToolStripMenuItem.Text = "Presets";
            // 
            // checkBoxDiagonal
            // 
            this.checkBoxDiagonal.AutoSize = true;
            this.checkBoxDiagonal.Location = new System.Drawing.Point(15, 34);
            this.checkBoxDiagonal.Name = "checkBoxDiagonal";
            this.checkBoxDiagonal.Size = new System.Drawing.Size(73, 19);
            this.checkBoxDiagonal.TabIndex = 7;
            this.checkBoxDiagonal.Text = "Diagonal";
            this.checkBoxDiagonal.UseVisualStyleBackColor = true;
            this.checkBoxDiagonal.CheckedChanged += new System.EventHandler(this.checkBoxDiagonal_CheckedChanged);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "a*";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.onLoad);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.onPaint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.onKeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onMouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.onMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.onMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onMouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label lblCell;
        private Panel panel1;
        private Button btnClear;
        private Button btnSearch;
        private Button btnMaze;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem matrixToolStripMenuItem;
        private ToolStripMenuItem importMatrixToolStripMenuItem;
        private ToolStripMenuItem exportMatrixToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem presetsToolStripMenuItem;
        private CheckBox checkBoxDiagonal;
    }
}