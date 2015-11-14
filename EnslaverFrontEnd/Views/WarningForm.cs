using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EnslaverFrontEnd.Contracts;
using EnslaverFrontEnd.Logic;
using EnslaverFrontEnd.Models;
using EnslaverFrontEnd.Presenters;

namespace EnslaverFrontEnd.Views
{
    public partial class WarningForm : BaseForm, IWarningView
    {
        public WarningForm()
        {
            InitializeComponent();
        }

        public WarningForm(FormFactory currentFormFactory)
            : base(currentFormFactory)
        {
            InitThisForm(null);
        }

        public WarningForm(FormFactory currentFormFactory, FormMessage someMessage)
            : base(currentFormFactory)
        {
            InitThisForm(someMessage);
        }

        private FormMessage formMessage = null;

        private void InitThisForm(FormMessage someMessage)
        {            
            formMessage = someMessage;
            Presenter = new EnslaverFrontEnd.Presenters.WarningFormPresenter(this);
            InitializeComponent();
            TryRaiseEvent(Init, EventArgs.Empty);
        }

        public void ShowWarningMessage(string text)
        {
            WarningLabel.Text = text;
        }

        public event EventHandler<EventArgs> Init;

        public event EventHandler<EventArgs> ExitClick;
    }
}
