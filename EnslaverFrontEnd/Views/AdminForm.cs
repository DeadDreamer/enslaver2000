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
    public partial class AdminForm : BaseForm, IAdminView
    {        
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
            Presenter = new EnslaverFrontEnd.Presenters.AdminFormPresenter(this);
            InitializeComponent();
            TryRaiseEvent(Init, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> Init;

        public event EventHandler<EventArgs> ExitClick;
    }
}
