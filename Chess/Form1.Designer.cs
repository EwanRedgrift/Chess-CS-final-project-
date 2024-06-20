namespace Chess
{
    partial class Chess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chess));
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitle = new System.Windows.Forms.Label();
            this.turnLabel = new System.Windows.Forms.Label();
            this.blackCollectedPeices = new System.Windows.Forms.Label();
            this.whiteCollectedPeices = new System.Windows.Forms.Label();
            this.previousMovesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(125, 66);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(380, 108);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "CHESS";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // subtitle
            // 
            this.subtitle.AutoSize = true;
            this.subtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subtitle.Location = new System.Drawing.Point(270, 174);
            this.subtitle.Name = "subtitle";
            this.subtitle.Size = new System.Drawing.Size(119, 15);
            this.subtitle.TabIndex = 1;
            this.subtitle.Text = "Press SPACE to start";
            this.subtitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // turnLabel
            // 
            this.turnLabel.AutoSize = true;
            this.turnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.turnLabel.Location = new System.Drawing.Point(749, 564);
            this.turnLabel.Name = "turnLabel";
            this.turnLabel.Size = new System.Drawing.Size(55, 18);
            this.turnLabel.TabIndex = 3;
            this.turnLabel.Text = "Turn = ";
            // 
            // blackCollectedPeices
            // 
            this.blackCollectedPeices.Location = new System.Drawing.Point(605, 9);
            this.blackCollectedPeices.Name = "blackCollectedPeices";
            this.blackCollectedPeices.Size = new System.Drawing.Size(103, 165);
            this.blackCollectedPeices.TabIndex = 4;
            // 
            // whiteCollectedPeices
            // 
            this.whiteCollectedPeices.Location = new System.Drawing.Point(605, 417);
            this.whiteCollectedPeices.Name = "whiteCollectedPeices";
            this.whiteCollectedPeices.Size = new System.Drawing.Size(103, 165);
            this.whiteCollectedPeices.TabIndex = 5;
            // 
            // previousMovesLabel
            // 
            this.previousMovesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previousMovesLabel.Location = new System.Drawing.Point(734, 221);
            this.previousMovesLabel.Name = "previousMovesLabel";
            this.previousMovesLabel.Size = new System.Drawing.Size(103, 73);
            this.previousMovesLabel.TabIndex = 6;
            // 
            // Chess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 591);
            this.Controls.Add(this.previousMovesLabel);
            this.Controls.Add(this.whiteCollectedPeices);
            this.Controls.Add(this.blackCollectedPeices);
            this.Controls.Add(this.turnLabel);
            this.Controls.Add(this.subtitle);
            this.Controls.Add(this.titleLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Chess";
            this.Text = "Chess";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subtitle;
        private System.Windows.Forms.Label turnLabel;
        private System.Windows.Forms.Label blackCollectedPeices;
        private System.Windows.Forms.Label whiteCollectedPeices;
        private System.Windows.Forms.Label previousMovesLabel;
    }
}

