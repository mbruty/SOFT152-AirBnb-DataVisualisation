namespace SOFT_152_AIR_BnB
{
    partial class Map
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
            this.titleLbl = new System.Windows.Forms.Label();
            this.midLabel = new System.Windows.Forms.Label();
            this.minPriceLabel = new System.Windows.Forms.Label();
            this.maxPriceLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.BackColor = System.Drawing.Color.White;
            this.titleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLbl.Location = new System.Drawing.Point(-5, 0);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(258, 25);
            this.titleLbl.TabIndex = 36;
            this.titleLbl.Text = "Average Price Per District";
            // 
            // midLabel
            // 
            this.midLabel.AutoSize = true;
            this.midLabel.BackColor = System.Drawing.Color.White;
            this.midLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.midLabel.Location = new System.Drawing.Point(410, 184);
            this.midLabel.Name = "midLabel";
            this.midLabel.Size = new System.Drawing.Size(36, 25);
            this.midLabel.TabIndex = 35;
            this.midLabel.Text = "$0";
            // 
            // minPriceLabel
            // 
            this.minPriceLabel.AutoSize = true;
            this.minPriceLabel.BackColor = System.Drawing.Color.White;
            this.minPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minPriceLabel.Location = new System.Drawing.Point(410, 329);
            this.minPriceLabel.Name = "minPriceLabel";
            this.minPriceLabel.Size = new System.Drawing.Size(36, 25);
            this.minPriceLabel.TabIndex = 34;
            this.minPriceLabel.Text = "$0";
            // 
            // maxPriceLabel
            // 
            this.maxPriceLabel.AutoSize = true;
            this.maxPriceLabel.BackColor = System.Drawing.Color.White;
            this.maxPriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxPriceLabel.Location = new System.Drawing.Point(410, 43);
            this.maxPriceLabel.Name = "maxPriceLabel";
            this.maxPriceLabel.Size = new System.Drawing.Size(36, 25);
            this.maxPriceLabel.TabIndex = 33;
            this.maxPriceLabel.Text = "$0";
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.titleLbl);
            this.Controls.Add(this.midLabel);
            this.Controls.Add(this.minPriceLabel);
            this.Controls.Add(this.maxPriceLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Map";
            this.Size = new System.Drawing.Size(484, 386);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Map_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Label midLabel;
        private System.Windows.Forms.Label minPriceLabel;
        private System.Windows.Forms.Label maxPriceLabel;
    }
}
