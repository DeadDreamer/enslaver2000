using EnslaverFrontEnd.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Windows.Forms;
using Emgu.CV.CvEnum;
using System.IO;
using EnslaverFrontEnd.Models;

namespace EnslaverFrontEnd.Presenters
{
    public class AdminFormPresenter : BasePresenter
    {
        //Declararation of all variables, vectors and haarcascades

        public AdminFormPresenter(IBaseView view)
            : base(view)
        {
            if (view is IAdminView)
            {
                this.AdminView = (view as IAdminView);
                this.AdminView.Init += new EventHandler<EventArgs>(AdminView_Init);             
            }
            else
            {
                new Exception("Это не наша форма!");
            }
        }

        void AdminView_Init(object sender, EventArgs e)
        {
            var formMessage = this.AdminView.GetFormMessage();
            if (formMessage != null && formMessage.Body is MessageBodyOfAdminForm) 
            {
                if (!(formMessage.Body as MessageBodyOfAdminForm).IsVisible) 
                {
                    this.AdminView.HideView();
                }
            }
        }

        

        public IAdminView AdminView { get; set; }
    }
}
