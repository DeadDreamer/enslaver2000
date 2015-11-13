using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnslaverFrontEnd.Logic;
using EnslaverFrontEnd.Models;
using EnslaverFrontEnd.Contracts;

namespace EnslaverFrontEnd.Views
{
    public partial class AlertForm : BaseForm, IAlertView
    {
        public AlertForm()
        {
            InitThisForm(null);
        }


        public AlertForm(FormFactory currentFormFactory)
            : base(currentFormFactory)
        {
            InitThisForm(null);
        }

        private FormMessage formMessage = null;
        public AlertForm(FormFactory currentFormFactory, FormMessage someMessage)
            : base(currentFormFactory)
        {
            InitThisForm(someMessage);
        }

        private void InitThisForm(FormMessage someMessage)
        {
            formMessage = someMessage;
            Presenter = new EnslaverFrontEnd.Presenters.AlertFormPresenter(this);
            InitializeComponent();
            TryRaiseEvent(Init, EventArgs.Empty);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public event EventHandler<EventArgs> Init;

        public event EventHandler<EventArgs> ExitClick;

        public FormMessage GetFormMessage()
        {
            return formMessage;
        }

        public void SetInfoMessage(string infoMessage)
        {
            AlertTextBox.Text = infoMessage;
        }

        private void AlertForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (animateGradient != null)
            {
                animateGradient.StopAnimation();
            }
        }
    }
}
