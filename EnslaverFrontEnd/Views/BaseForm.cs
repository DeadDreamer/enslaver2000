using EnslaverFrontEnd.Contracts;
using EnslaverFrontEnd.Logic;
using EnslaverFrontEnd.Models;
using EnslaverFrontEnd.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EnslaverFrontEnd
{
    public partial class BaseForm : Form
    {

      

        public BaseForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }


        public long TypeID;
        public Guid Guid = Guid.NewGuid();
        public Guid SenderGuid;
        protected FormFactory CurrentFormFactory;


        public BaseForm(FormFactory currentFormFactory)
        {
            CheckForIllegalCrossThreadCalls = false;
            CurrentFormFactory = currentFormFactory;
            CurrentFormFactory.RegisterForm(this);
        }

        public virtual int ReceiveMessage(FormMessage constructorParameter)
        {

            return -1;
        }

        public bool TryRaiseEvent(EventHandler<EventArgs> someHandler, EventArgs eventArgs)
        {
            if (someHandler != null)
            {
                someHandler(this, eventArgs);
                return true;
            }
            return false;
        }

        public BaseForm(FormMessage constructorParameter)
        {
            if (constructorParameter != null)
            {
                this.SenderGuid = constructorParameter.SenderGuid;
            }
        }

        public virtual BasePresenter Presenter { get; set; }

        protected override void OnClosed(EventArgs e)
        {
            CurrentFormFactory.DeleteForm(this);
        }
    }
}
