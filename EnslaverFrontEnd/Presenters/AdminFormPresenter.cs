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

namespace EnslaverFrontEnd.Presenters
{
    public class AdminFormPresenter : BasePresenter
    {
        //Declararation of all variables, vectors and haarcascades

        public AdminFormPresenter(IBaseView view)
            : base(view)
        {

            //  (this.View as IAdminView).OnStartClick += new EventHandler<EventArgs>(AdminFormPresenter_OnStartClick);
            //  (this.View as IAdminView).OnStopClick += new EventHandler<EventArgs>(AdminFormPresenter_OnStopClick);
        }

        public override void OnView_ExitClick(object sender, EventArgs e)
        {
            (this.View as IAdminView).CloseView();
        }
    }
}
