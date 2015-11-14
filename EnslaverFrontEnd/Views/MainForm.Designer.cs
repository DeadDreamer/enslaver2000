using System.Drawing;
using System.Windows.Forms;
namespace EnslaverFrontEnd.Views
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.StartOrStopTrackingButton = new System.Windows.Forms.Button();
            this.DevicesComboBox = new System.Windows.Forms.ComboBox();
            this.DeviseHeaderLabel = new System.Windows.Forms.Label();
            this.TestButton = new System.Windows.Forms.Button();
            this.AlertTestTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "NotifyIcon";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(12, 12);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(541, 415);
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;
            // 
            // StartOrStopTrackingButton
            // 
            this.StartOrStopTrackingButton.Enabled = false;
            this.StartOrStopTrackingButton.Location = new System.Drawing.Point(645, 65);
            this.StartOrStopTrackingButton.Name = "StartOrStopTrackingButton";
            this.StartOrStopTrackingButton.Size = new System.Drawing.Size(121, 25);
            this.StartOrStopTrackingButton.TabIndex = 1;
            this.StartOrStopTrackingButton.Text = "Start tracking...";
            this.StartOrStopTrackingButton.UseVisualStyleBackColor = true;
            this.StartOrStopTrackingButton.Click += new System.EventHandler(this.StartOrStopTrackingButton_Click);
            // 
            // DevicesComboBox
            // 
            this.DevicesComboBox.FormattingEnabled = true;
            this.DevicesComboBox.Location = new System.Drawing.Point(563, 38);
            this.DevicesComboBox.Name = "DevicesComboBox";
            this.DevicesComboBox.Size = new System.Drawing.Size(261, 21);
            this.DevicesComboBox.TabIndex = 2;
            this.DevicesComboBox.SelectedIndexChanged += new System.EventHandler(this.DevicesComboBox_SelectedIndexChanged);
            // 
            // DeviseHeaderLabel
            // 
            this.DeviseHeaderLabel.AutoSize = true;
            this.DeviseHeaderLabel.Location = new System.Drawing.Point(560, 13);
            this.DeviseHeaderLabel.Name = "DeviseHeaderLabel";
            this.DeviseHeaderLabel.Size = new System.Drawing.Size(49, 13);
            this.DeviseHeaderLabel.TabIndex = 3;
            this.DeviseHeaderLabel.Text = "Devices:";
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(749, 287);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(75, 23);
            this.TestButton.TabIndex = 4;
            this.TestButton.Text = "Raise alert";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // AlertTestTextBox
            // 
            this.AlertTestTextBox.Location = new System.Drawing.Point(563, 290);
            this.AlertTestTextBox.Name = "AlertTestTextBox";
            this.AlertTestTextBox.Size = new System.Drawing.Size(180, 20);
            this.AlertTestTextBox.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 439);
            this.Controls.Add(this.AlertTestTextBox);
            this.Controls.Add(this.TestButton);
            this.Controls.Add(this.DeviseHeaderLabel);
            this.Controls.Add(this.DevicesComboBox);
            this.Controls.Add(this.StartOrStopTrackingButton);
            this.Controls.Add(this.PictureBox);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        public void CustomSettings()
        {
            CenterToScreen();     
            BackColor = Color.White;
        }


        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private PictureBox PictureBox;
        private Button StartOrStopTrackingButton;
        private ComboBox DevicesComboBox;
        private Label DeviseHeaderLabel;
        private Button TestButton;
        private TextBox AlertTestTextBox;
    }
}