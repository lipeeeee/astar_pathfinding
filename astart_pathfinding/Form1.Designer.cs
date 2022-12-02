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
            this.lblMatrixEndpoints = new System.Windows.Forms.Label();
            this.lblMousePos = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMatrixEndpoints
            // 
            this.lblMatrixEndpoints.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMatrixEndpoints.AutoSize = true;
            this.lblMatrixEndpoints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMatrixEndpoints.Location = new System.Drawing.Point(9, 10);
            this.lblMatrixEndpoints.Name = "lblMatrixEndpoints";
            this.lblMatrixEndpoints.Size = new System.Drawing.Size(62, 17);
            this.lblMatrixEndpoints.TabIndex = 0;
            this.lblMatrixEndpoints.Text = "EndPoints";
            // 
            // lblMousePos
            // 
            this.lblMousePos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMousePos.AutoSize = true;
            this.lblMousePos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMousePos.Location = new System.Drawing.Point(9, 34);
            this.lblMousePos.Name = "lblMousePos";
            this.lblMousePos.Size = new System.Drawing.Size(64, 17);
            this.lblMousePos.TabIndex = 1;
            this.lblMousePos.Text = "MousePos";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.btnDebug);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.lblMatrixEndpoints);
            this.panel1.Controls.Add(this.lblMousePos);
            this.panel1.Location = new System.Drawing.Point(12, 378);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 60);
            this.panel1.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(122, 34);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear(C)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDebug.Location = new System.Drawing.Point(122, 6);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 3;
            this.btnDebug.Text = "Debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "a*";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblMatrixEndpoints;
        private Label lblMousePos;
        private Panel panel1;
        private Button btnClear;
        private Button btnDebug;
    }
}