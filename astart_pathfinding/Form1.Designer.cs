namespace astart_pathfinding
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
            this.SuspendLayout();
            // 
            // lblMatrixEndpoints
            // 
            this.lblMatrixEndpoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMatrixEndpoints.AutoSize = true;
            this.lblMatrixEndpoints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMatrixEndpoints.Location = new System.Drawing.Point(724, 9);
            this.lblMatrixEndpoints.Name = "lblMatrixEndpoints";
            this.lblMatrixEndpoints.Size = new System.Drawing.Size(40, 17);
            this.lblMatrixEndpoints.TabIndex = 0;
            this.lblMatrixEndpoints.Text = "label1";
            // 
            // lblMousePos
            // 
            this.lblMousePos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMousePos.AutoSize = true;
            this.lblMousePos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMousePos.Location = new System.Drawing.Point(724, 40);
            this.lblMousePos.Name = "lblMousePos";
            this.lblMousePos.Size = new System.Drawing.Size(39, 17);
            this.lblMousePos.TabIndex = 1;
            this.lblMousePos.Text = "10, 10";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblMousePos);
            this.Controls.Add(this.lblMatrixEndpoints);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblMatrixEndpoints;
        private Label lblMousePos;
    }
}