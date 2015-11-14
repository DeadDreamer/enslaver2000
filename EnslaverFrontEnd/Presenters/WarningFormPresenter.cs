using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnslaverFrontEnd.Contracts;
using EnslaverFrontEnd.Models;

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
            FormMessage formMessage = this.WarningView.GetFormMessage();
            if (formMessage.Body is string)
            {
                this.WarningView.ShowWarningMessage(formMessage.Body as string);
            }
            else
            {
                this.WarningView.ShowWarningMessage("Вернитесь на рабочее место!");
            }

        }

        public IWarningView WarningView { get; set; }
    }
}
