namespace EnslaverFrontEnd.Views
{
    partial class AdminForm
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
            this.ImageBox = new Emgu.CV.UI.ImageBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.TeachButton = new System.Windows.Forms.Button();
            this.UserTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox )).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.ImageBox.Location = new System.Drawing.Point(13, 13);
            this.ImageBox.Name = "pictureBox1";
            this.ImageBox.Size = new System.Drawing.Size(591, 489);
            this.ImageBox.TabIndex = 0;
            this.ImageBox.TabStop = false;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(626, 13);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(95, 49);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "СТАРТ";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(736, 13);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(95, 49);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "СТОП";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // TeachButton
            // 
            this.TeachButton.Location = new System.Drawing.Point(756, 121);
            this.TeachButton.Name = "TeachButton";
            this.TeachButton.Size = new System.Drawing.Size(75, 23);
            this.TeachButton.TabIndex = 3;
            this.TeachButton.Text = "Обучить";
            this.TeachButton.UseVisualStyleBackColor = true;
            this.TeachButton.Click += new System.EventHandler(this.TeachButton_Click);
            // 
            // UserTextBox
            // 
            this.UserTextBox.Location = new System.Drawing.Point(626, 95);
            this.UserTextBox.Name = "UserTextBox";
            this.UserTextBox.Size = new System.Drawing.Size(205, 20);
            this.UserTextBox.TabIndex = 4;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 514);
            this.Controls.Add(this.UserTextBox);
            this.Controls.Add(this.TeachButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ImageBox);
            this.Name = "AdminForm";
            this.Text = "AdminForm";
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox ImageBox;        
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button TeachButton;
        private System.Windows.Forms.TextBox UserTextBox;

    }
}