using System.Drawing;
using System.Windows.Forms;
using EnslaverFrontEnd.Logic;
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
            this.VideoFileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.blinkTimer = new System.Windows.Forms.Timer(this.components);
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
            this.TestButton.Location = new System.Drawing.Point(563, 357);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(75, 23);
            this.TestButton.TabIndex = 4;
            this.TestButton.Text = "Raise alert";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // AlertTestTextBox
            // 
            this.AlertTestTextBox.Location = new System.Drawing.Point(563, 277);
            this.AlertTestTextBox.Name = "AlertTestTextBox";
            this.AlertTestTextBox.Size = new System.Drawing.Size(180, 20);
            this.AlertTestTextBox.TabIndex = 5;
            // 
            // VideoFileTextBox
            // 
            this.VideoFileTextBox.Location = new System.Drawing.Point(563, 331);
            this.VideoFileTextBox.Name = "VideoFileTextBox";
            this.VideoFileTextBox.Size = new System.Drawing.Size(261, 20);
            this.VideoFileTextBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(563, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Warning Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(563, 315);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Path to video file";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // blinkTimer
            // 
            this.blinkTimer.Tick += new System.EventHandler(this.blnkTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 439);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VideoFileTextBox);
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
            this.VisibleChanged += new System.EventHandler(this.MainForm_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        public void CustomSettings()
        {
            //For Debug only

            /*CenterToScreen();     
            BackColor = Color.White;
            this.ShowInTaskbar = false;
             * */

            this.Visible = AppGlobalContext.GetInstance().IsDebug;

        }


        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private PictureBox PictureBox;
        private Button StartOrStopTrackingButton;
        private ComboBox DevicesComboBox;
        private Label DeviseHeaderLabel;
        private Button TestButton;
        private TextBox AlertTestTextBox;
        private TextBox VideoFileTextBox;
        private Label label1;
        private Label label2;
        private Timer timer1;
        private Timer blinkTimer;
    }
}