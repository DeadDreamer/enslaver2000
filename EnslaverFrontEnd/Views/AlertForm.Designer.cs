using EnslaverFrontEnd.Models;
using System.Drawing;

namespace EnslaverFrontEnd.Views
{
    partial class AlertForm
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
            this.AlertTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.HeaderLabel = new EnslaverFrontEnd.Models.TransparentLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // AlertTextBox
            // 
            this.AlertTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlertTextBox.BackColor = System.Drawing.Color.White;
            this.AlertTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AlertTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AlertTextBox.Location = new System.Drawing.Point(226, 76);
            this.AlertTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.AlertTextBox.Multiline = true;
            this.AlertTextBox.Name = "AlertTextBox";
            this.AlertTextBox.Size = new System.Drawing.Size(630, 221);
            this.AlertTextBox.TabIndex = 0;
            this.AlertTextBox.Text = "\r\n\r\n\r\nGo back to your workplace!!!\r\n";
            this.AlertTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::EnslaverFrontEnd.Properties.Resources.AlertRobot;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(0, 76);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(228, 221);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.OkButton.FlatAppearance.BorderSize = 3;
            this.OkButton.Location = new System.Drawing.Point(698, 325);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(133, 37);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "Ok!";
            this.OkButton.UseVisualStyleBackColor = false;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HeaderLabel.ForeColor = System.Drawing.Color.White;
            this.HeaderLabel.Location = new System.Drawing.Point(21, 21);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(152, 36);
            this.HeaderLabel.TabIndex = 3;
            this.HeaderLabel.Text = "Attention!";
            // 
            // AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(856, 387);
            this.Controls.Add(this.HeaderLabel);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.AlertTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AlertForm";
            this.Text = "AlertForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlertForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            CustomSettings();
        }


        #endregion


        public void CustomSettings()
        {
            #region Show on center and make gradient
            animateGradient = new AnimateGradient(this, Color.Red, Color.Black, 0f, HeaderLabel);
            OkButton.Select();
            HeaderLabel.Parent = this;
            animateGradient.StartAnimation();
            CenterToScreen();
            #endregion
        }
        private AnimateGradient animateGradient { get; set; }

        private System.Windows.Forms.TextBox AlertTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button OkButton;
        private TransparentLabel HeaderLabel;
    }
}