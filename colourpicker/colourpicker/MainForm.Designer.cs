namespace ColourPicker
{
    partial class MainForm
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
            this.colourPanel = new System.Windows.Forms.Panel();
            this.colorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // colourPanel
            // 
            this.colourPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colourPanel.Location = new System.Drawing.Point(12, 12);
            this.colourPanel.Name = "colourPanel";
            this.colourPanel.Size = new System.Drawing.Size(212, 212);
            this.colourPanel.TabIndex = 0;
            // 
            // colorLabel
            // 
            this.colorLabel.Location = new System.Drawing.Point(243, 65);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(321, 103);
            this.colorLabel.TabIndex = 1;
            this.colorLabel.Text = "...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 236);
            this.Controls.Add(this.colorLabel);
            this.Controls.Add(this.colourPanel);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 300);
            this.Name = "MainForm";
            this.Text = "Colour Picker";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel colourPanel;
        private System.Windows.Forms.Label colorLabel;
    }
}

