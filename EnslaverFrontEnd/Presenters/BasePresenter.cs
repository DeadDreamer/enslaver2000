using EnslaverFrontEnd.Contracts;
using EnslaverFrontEnd.Logic;
using EnslaverFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverFrontEnd.Presenters
{
    public class BasePresenter
    {
        public BasePresenter(IBaseView view)
        {
            this.View = view;
            this.View.Init += OnView_Init;
            this.View.ExitClick += OnView_ExitClick;
        }

        public virtual void OnView_ExitClick(object sender, EventArgs e)
        {
            AppGlobalContext.GetInstance().ShowSingletoneForm(null, (long)FormTypes.LoginForm, null);
        }

        public virtual void OnView_Init(object sender, EventArgs e)
        {

        }

        public virtual IBaseView View { get; set; }
    }
}
