namespace SOFT_152_AIR_BnB
{
    partial class CustomListBox
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
            this.leftBox = new System.Windows.Forms.ListBox();
            this.rightBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // leftBox
            // 
            this.leftBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftBox.FormattingEnabled = true;
            this.leftBox.ItemHeight = 24;
            this.leftBox.Location = new System.Drawing.Point(0, 0);
            this.leftBox.Name = "leftBox";
            this.leftBox.Size = new System.Drawing.Size(175, 28);
            this.leftBox.TabIndex = 0;
            this.leftBox.SelectedIndexChanged += new System.EventHandler(this.LeftBox_SelectedIndexChanged);
            this.leftBox.DoubleClick += new System.EventHandler(this.LeftBox_DoubleClick);
            // 
            // rightBox
            // 
            this.rightBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightBox.FormattingEnabled = true;
            this.rightBox.ItemHeight = 24;
            this.rightBox.Location = new System.Drawing.Point(174, 0);
            this.rightBox.Name = "rightBox";
            this.rightBox.Size = new System.Drawing.Size(153, 28);
            this.rightBox.TabIndex = 1;
            this.rightBox.SelectedIndexChanged += new System.EventHandler(this.RightBox_SelectedIndexChanged);
            this.rightBox.DoubleClick += new System.EventHandler(this.RightBox_DoubleClick);
            // 
            // CustomListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.rightBox);
            this.Controls.Add(this.leftBox);
            this.Name = "CustomListBox";
            this.Size = new System.Drawing.Size(330, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox leftBox;
        private System.Windows.Forms.ListBox rightBox;
    }
}
