﻿namespace EnslaverFrontEnd.Views
{
    partial class WarningForm
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
            this.WarningLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // WarningLabel
            // 
            this.WarningLabel.AutoSize = true;
            this.WarningLabel.Location = new System.Drawing.Point(508, 200);
            this.WarningLabel.Name = "WarningLabel";
            this.WarningLabel.Size = new System.Drawing.Size(154, 13);
            this.WarningLabel.TabIndex = 0;
            this.WarningLabel.Text = "Вернитесь на рабочее место";
            // 
            // WarningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 556);
            this.Controls.Add(this.WarningLabel);
            this.Name = "WarningForm";
            this.Text = "WarningForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WarningLabel;
    }
}