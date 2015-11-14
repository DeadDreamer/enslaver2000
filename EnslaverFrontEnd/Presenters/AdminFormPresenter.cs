using EnslaverFrontEnd.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverFrontEnd.Presenters
{
    public class AdminFormPresenter : BasePresenter
    {
        public AdminFormPresenter(IBaseView view)
            : base(view)
        {
            (this.View as IAdminView).OnStartClick += new EventHandler<EventArgs>(AdminFormPresenter_OnStartClick);
            (this.View as IAdminView).OnStopClick += new EventHandler<EventArgs>(AdminFormPresenter_OnStopClick);
            (this.View as IAdminView).OnTeachClick +=new EventHandler<EventArgs>(AdminFormPresenter_OnTeachClick); 
        }

        void AdminFormPresenter_OnStopClick(object sender, EventArgs e)
        {
                
        }

        void AdminFormPresenter_OnStartClick(object sender, EventArgs e)
        {
         
        }

        void AdminFormPresenter_OnTeachClick(object sender, EventArgs e)
        {

        }
    }
}
