namespace SOFT_152_AIR_BnB
{
    partial class EnterData
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.submitBtn = new System.Windows.Forms.Button();
            this.textLabel = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(12, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(47, 24);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Edit ";
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(12, 36);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(211, 20);
            this.inputBox.TabIndex = 1;
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(229, 34);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(75, 23);
            this.submitBtn.TabIndex = 2;
            this.submitBtn.Text = "SUBMIT";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // textLabel
            // 
            this.textLabel.AutoSize = true;
            this.textLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLabel.Location = new System.Drawing.Point(52, 9);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(15, 24);
            this.textLabel.TabIndex = 3;
            this.textLabel.Text = " ";
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(229, 66);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 4;
            this.cancelBtn.Text = "CANCEL";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // EnterData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 96);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.textLabel);
            this.Controls.Add(this.submitBtn);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.nameLabel);
            this.Name = "EnterData";
            this.Text = "EnterData";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.Button cancelBtn;
    }
}