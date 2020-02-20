namespace SOFT_152_AIR_BnB
{
    partial class BarGraph
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.imageBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Location = new System.Drawing.Point(1, 1);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(149, 149);
            this.imageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imageBox.TabIndex = 0;
            this.imageBox.TabStop = false;
            // 
            // BarGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.imageBox);
            this.Name = "BarGraph";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imageBox;
    }
}
