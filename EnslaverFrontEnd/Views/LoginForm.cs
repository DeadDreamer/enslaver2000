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
    public partial class LoginForm : BaseForm, ILoginView
    {
        private FormMessage formMessage = null;

        public LoginForm()
        {
            InitThisForm(null);
        }

        public LoginForm(FormFactory currentFormFactory)
            : base(currentFormFactory)
        {
            InitThisForm(null);
        }

        public LoginForm(FormFactory currentFormFactory, FormMessage formMessage)
            : base(currentFormFactory)
        {
            InitThisForm(formMessage);
        }

        private void InitThisForm(FormMessage someMessage)
        {
            formMessage = someMessage;
            Presenter = new EnslaverFrontEnd.Presenters.LoginFormPresenter(this);
            InitializeComponent();
            TryRaiseEvent(Init, EventArgs.Empty);
        }


        public event EventHandler<EventArgs> Init;

        public event EventHandler<EventArgs> ExitClick;

        private void OkButton_Click(object sender, EventArgs e)
        {
            TryRaiseEvent(OkButtonClick, EventArgs.Empty);
        }

        public string GetPasswordString()
        {
            return PasswordTextBox.Text;
        }

        public event EventHandler<EventArgs> OkButtonClick;


        public void ShowMessage(string message)
        {
            MessageLabel.Text = message;
        }


        public FormMessage GetFormMessage()
        {           
            return formMessage;
        }


        public void CloseView()
        {
            ForceClose();
        }
    }
}
