namespace EnslaverFrontEnd.Views
{
    partial class AdminForm
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button2 = new System.Windows.Forms.Button();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TeachedImage = new Emgu.CV.UI.ImageBox();
            this.imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CountOfFacesLabel = new System.Windows.Forms.Label();
            this.ListOfUserLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TeachedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Location = new System.Drawing.Point(6, 278);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(224, 31);
            this.button2.TabIndex = 3;
            this.button2.Text = "Обучить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.TeachButton_Click);
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(67, 170);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(107, 20);
            this.UserNameTextBox.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.UserNameTextBox);
            this.groupBox1.Controls.Add(this.TeachedImage);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(830, 297);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 315);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Обучение: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Имя: ";
            // 
            // TeachedImage
            // 
            this.TeachedImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TeachedImage.Location = new System.Drawing.Point(42, 19);
            this.TeachedImage.Name = "TeachedImage";
            this.TeachedImage.Size = new System.Drawing.Size(163, 134);
            this.TeachedImage.TabIndex = 5;
            this.TeachedImage.TabStop = false;
            // 
            // imageBoxFrameGrabber
            // 
            this.imageBoxFrameGrabber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBoxFrameGrabber.Location = new System.Drawing.Point(12, 12);
            this.imageBoxFrameGrabber.Name = "imageBoxFrameGrabber";
            this.imageBoxFrameGrabber.Size = new System.Drawing.Size(800, 600);
            this.imageBoxFrameGrabber.TabIndex = 4;
            this.imageBoxFrameGrabber.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(239, 29);
            this.label2.TabIndex = 14;
            this.label2.Text = "Обнаружено лиц: ";
            // 
            // CountOfFacesLabel
            // 
            this.CountOfFacesLabel.AutoSize = true;
            this.CountOfFacesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CountOfFacesLabel.ForeColor = System.Drawing.Color.Red;
            this.CountOfFacesLabel.Location = new System.Drawing.Point(8, 82);
            this.CountOfFacesLabel.Name = "CountOfFacesLabel";
            this.CountOfFacesLabel.Size = new System.Drawing.Size(30, 31);
            this.CountOfFacesLabel.TabIndex = 15;
            this.CountOfFacesLabel.Text = "0";
            // 
            // ListOfUserLabel
            // 
            this.ListOfUserLabel.AutoSize = true;
            this.ListOfUserLabel.Font = new System.Drawing.Font("Times New Roman", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListOfUserLabel.ForeColor = System.Drawing.Color.Red;
            this.ListOfUserLabel.Location = new System.Drawing.Point(9, 212);
            this.ListOfUserLabel.Name = "ListOfUserLabel";
            this.ListOfUserLabel.Size = new System.Drawing.Size(95, 30);
            this.ListOfUserLabel.TabIndex = 16;
            this.ListOfUserLabel.Text = "никого";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(232, 29);
            this.label5.TabIndex = 17;
            this.label5.Text = "Нашлись в кадре:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.ListOfUserLabel);
            this.groupBox2.Controls.Add(this.CountOfFacesLabel);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(830, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(236, 281);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Результаты: ";
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 625);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imageBoxFrameGrabber);
            this.Name = "AdminForm";
            this.Text = "ПОРАБОТИТЕЛЬ 2000";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TeachedImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AdminForm_FormClosing);

        }

        void AdminForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {            
            this.Hide();
            e.Cancel = true;
        }

        #endregion

        private System.Windows.Forms.Button button2;
        private Emgu.CV.UI.ImageBox imageBoxFrameGrabber;
        private Emgu.CV.UI.ImageBox TeachedImage;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label CountOfFacesLabel;
        private System.Windows.Forms.Label ListOfUserLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;

    }
}