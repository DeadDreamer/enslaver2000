using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnslaverFrontEnd.Contracts;

namespace EnslaverFrontEnd.Presenters
{
    public class WarningFormPresenter : BasePresenter
    {
        public WarningFormPresenter(IBaseView view)
            : base(view)
        {
            if (view is IWarningView)
            {
                this.WarningView = (view as IWarningView);
                this.WarningView.Init += new EventHandler<EventArgs>(View_Init);
                this.WarningView.ExitClick += OnView_ExitClick;
            }
            else
            {
                new Exception("Это не наша форма!");
            }
        }

        void View_Init(object sender, EventArgs e)
        {
            this.WarningView.ShowWarningMessage("Привет!");
        }

        public IWarningView WarningView { get; set; }
    }
}
