using EnslaverFrontEnd.Contracts;
using EnslaverFrontEnd.Logic;
using EnslaverFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;

namespace EnslaverFrontEnd.Views
{
    public partial class AdminForm : BaseForm, IAdminView
    {
        private Capture _capture = null;

        public AdminForm()
        {
            InitThisForm();
        }

        public AdminForm(FormFactory currentFormFactory)
            : base(currentFormFactory)
        {
            InitThisForm();
        }

        public AdminForm(FormFactory currentFormFactory, FormMessage formMessage)
            : base(currentFormFactory)
        {
            InitThisForm();
        }
        private void InitThisForm()
        {
            InitEmgu();
            Presenter = new EnslaverFrontEnd.Presenters.AdminFormPresenter(this);
            InitializeComponent();
            TryRaiseEvent(Init, EventArgs.Empty);
            
        }

        private void InitEmgu()
        {            
            try
            {
                CvInvoke.UseOpenCL = false;
                _capture = new Capture();
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }

        public event EventHandler<EventArgs> Init;

        public event EventHandler<EventArgs> ExitClick;

        public event EventHandler<EventArgs> OnStartClick;

        public event EventHandler<EventArgs> OnStopClick;

        public event EventHandler<EventArgs> OnTeachClick;

        public string GetUserName()
        {
            return UserTextBox.Text;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            TryRaiseEvent(OnStartClick, e);

        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            TryRaiseEvent(OnStopClick, e);
        }

        private void TeachButton_Click(object sender, EventArgs e)
        {
            TryRaiseEvent(OnTeachClick, e);
        }


        public Capture GetCapture()
        {
            return _capture;
        }


        public void SetImage(IImage someImage)
        {
            lock (typeof(AdminForm))
            {
                ImageBox.Image = someImage;
            }
        }
    }
}
