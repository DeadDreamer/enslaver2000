using AForge.Video.DirectShow;
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

namespace EnslaverFrontEnd.Views
{
    public partial class MainForm : BaseForm, IMainView
    {
        public MainForm()
        {
            InitThisForm();
        }

        public MainForm(FormFactory currentFormFactory)
            : base(currentFormFactory)
        {
            InitThisForm();
        }

        public MainForm(FormFactory currentFormFactory, FormMessage formMessage)
        {
            InitThisForm();
        }

        private void InitThisForm()
        {
            Presenter = new EnslaverFrontEnd.Presenters.MainFormPresenter(this);
            InitializeComponent();
            TryRaiseEvent(Init, EventArgs.Empty);
            this.Visible = false;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = AppGlobalContext.GetInstance().TimerPeriodInMilSec;
            timer1.Start();
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            TryRaiseEvent(OnTimerTick, e);         
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                if (components != null)
                    components.Dispose();

            base.Dispose(disposing);
        }

        private void NotifyIcon_ExitClick(object sender, EventArgs e)
        {
            if (ExitClick != null)
                ExitClick(this, new EventArgs());
        }

        private void AdminIcon_ExitClick(object sender, EventArgs e)
        {
            TryRaiseEvent(OnAdminClick, EventArgs.Empty);
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private object lockObject = new object();

        public event EventHandler<EventArgs> Init;

        public event EventHandler<EventArgs> ExitClick;

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.NotifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { new MenuItem("Аdministration", this.AdminIcon_ExitClick), new MenuItem("Close!", this.NotifyIcon_ExitClick) });
        }


        public void SetCamDevices(List<FilterInfo> camDevices)
        {
            devices = camDevices;
            if (devices != null)
            {
                DevicesComboBox.Items.Clear();
                DevicesComboBox.Items.Add("Сhoose device...");
                for (int i = 0; i < devices.Count; i++)
                {
                    DevicesComboBox.Items.Add(devices[i].Name);
                }
            }
            else
            {
                DevicesComboBox.Items.Add("No devices...");
            }
            DevicesComboBox.SelectedIndex = 0;
        }

        private List<FilterInfo> devices = null;

        public event EventHandler<EventArgs> OnChangeCamDevice;

        public event EventHandler<EventArgs> OnStartOrStopClick;

        public FilterInfo GetCurrentDevice()
        {
            if (DevicesComboBox.SelectedIndex == 0 || DevicesComboBox.SelectedIndex == -1)
                return null;
            //Первый пустой элемент
            if ((DevicesComboBox.SelectedIndex - 1) < (devices.Count))
                return devices[DevicesComboBox.SelectedIndex - 1];
            return null;
        }

        public void SetCurrentBitmap(Bitmap bitmap)
        {
            lock (lockObject)
            {
                if (PictureBox.Image != null) PictureBox.Image.Dispose();
                PictureBox.Image = bitmap;
            }
        }

        private void StartOrStopTrackingButton_Click(object sender, EventArgs e)
        {
            TryRaiseEvent(OnStartOrStopClick, EventArgs.Empty);
        }

        private void DevicesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TryRaiseEvent(OnChangeCamDevice, EventArgs.Empty);
            StartOrStopTrackingButton.Enabled = (DevicesComboBox.SelectedIndex == 0) ? false : true;
        }

        public virtual void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!AppGlobalContext.GetInstance().AllowExit)
            {
                e.Cancel = true;
                TryRaiseEvent(ExitClick, EventArgs.Empty);
            }
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            object messageBody = (object)(new MessageBodyOfWarningForm(AlertTestTextBox.Text, VideoFileTextBox.Text));
            AppGlobalContext.GetInstance().ShowForm(this, (long)FormTypes.WarningForm, new FormMessage() { Body = messageBody });
        }


        public void SetCamIsActive(bool isActive)
        {
            StartOrStopTrackingButton.Text = (isActive) ? "Stop tracking..." : "Start tracking...";
        }

        public event EventHandler<EventArgs> OnTimerTick;

        /// <summary>
        /// Сами себя скроем ...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_VisibleChanged(object sender, EventArgs e)
        {
            this.Visible = AppGlobalContext.GetInstance().IsDebug;
        }
        
        public event EventHandler<EventArgs> OnAdminClick;
    }
}
