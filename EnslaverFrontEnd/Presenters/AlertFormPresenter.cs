using EnslaverFrontEnd.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverFrontEnd.Presenters
{
    public class AlertFormPresenter : BasePresenter
    {
        public AlertFormPresenter(IBaseView view)
            : base(view)
        {

        }

        public override void OnView_Init(object sender, EventArgs e)
        {
            var formMessage=(this.View as IAlertView).GetFormMessage();
            if (formMessage != null && formMessage.Body != null && formMessage.Body is string && !string.IsNullOrEmpty(formMessage.Body as string)) 
            {
                (this.View as IAlertView).SetInfoMessage(formMessage.Body as string);
            }
        }
    }
}
