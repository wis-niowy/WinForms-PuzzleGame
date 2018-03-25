namespace IV_sem___PwSG___lab3___WinFORMS
{
    partial class SettingsWindow
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
            this.labelLife = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.numericUpDownLifes = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTime = new System.Windows.Forms.NumericUpDown();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLifes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTime)).BeginInit();
            this.SuspendLayout();
            // 
            // labelLife
            // 
            this.labelLife.AutoSize = true;
            this.labelLife.Location = new System.Drawing.Point(35, 21);
            this.labelLife.Name = "labelLife";
            this.labelLife.Size = new System.Drawing.Size(25, 13);
            this.labelLife.TabIndex = 0;
            this.labelLife.Text = "lifes";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(35, 61);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(40, 13);
            this.labelTime.TabIndex = 1;
            this.labelTime.Text = "time [s]";
            // 
            // numericUpDownLifes
            // 
            this.numericUpDownLifes.Location = new System.Drawing.Point(124, 19);
            this.numericUpDownLifes.Name = "numericUpDownLifes";
            this.numericUpDownLifes.Size = new System.Drawing.Size(147, 20);
            this.numericUpDownLifes.TabIndex = 2;
            this.numericUpDownLifes.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // numericUpDownTime
            // 
            this.numericUpDownTime.Location = new System.Drawing.Point(124, 59);
            this.numericUpDownTime.Name = "numericUpDownTime";
            this.numericUpDownTime.Size = new System.Drawing.Size(147, 20);
            this.numericUpDownTime.TabIndex = 3;
            this.numericUpDownTime.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(112, 106);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(87, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(215, 106);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(87, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 141);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.numericUpDownTime);
            this.Controls.Add(this.numericUpDownLifes);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelLife);
            this.Name = "SettingsWindow";
            this.Text = "SettingsWindow";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLifes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLife;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.NumericUpDown numericUpDownLifes;
        private System.Windows.Forms.NumericUpDown numericUpDownTime;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}