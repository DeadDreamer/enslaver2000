using EnslaverCore.Logic;
using EnslaverCore.Models;
using EnslaverFrontEnd.Contracts;
using EnslaverFrontEnd.Logic;
using EnslaverFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnslaverFrontEnd.Presenters
{
    public class LoginFormPresenter : BasePresenter
    {
        public LoginFormPresenter(IBaseView view)
            : base(view)
        {
            (this.View as ILoginView).OkButtonClick += LoginFormPresenter_OkButtonClick;
        }

        private ILoginView LoginView
        {
            get
            {
                var loginView = this.View as ILoginView;
                if (loginView == null) throw new Exception("Неправильный контракт для представления");
                return loginView;
            }
        }

        void LoginFormPresenter_OkButtonClick(object sender, EventArgs e)
        {

            string checkThis = LoginView.GetPasswordString();
            if (string.IsNullOrEmpty(checkThis))
            {
                LoginView.ShowMessage("enter any characters...");
            }
            else if (checkThis == Constants.VeryStrongPassword)
            {
                //  SharedMemoryHelper.WriteBytesAtOnce(AppGlobalContext.HandlerOfMapView, SharedMemoryHelper.GetBytesFromUnicodeString("-1"));
                //  SharedMemoryHelper.CloseHandlers(AppGlobalContext.FileMapHandler, AppGlobalContext.HandlerOfMapView);

                FormMessage formMessage = this.LoginView.GetFormMessage();
                //Проверяем ,что это наше сообщение и не пустое...
                if (formMessage != null &&
                    formMessage.Body is MessageBodyOfLoginForm)
                {
                    var forms = AppGlobalContext.GetInstance().FindFormsByType(2);
                    if (forms != null && forms.Count > 0)
                    {
                        forms[0].Show();
                    }
                    else
                    {
                        AppGlobalContext.GetInstance().ShowForm(null, (long)(formMessage.Body as MessageBodyOfLoginForm).RedirectToForm);

                    }
                    this.LoginView.CloseView();
                }
                else
                {
                    AppGlobalContext.GetInstance().Exit();
                }


            }
            else
            {
                LoginView.ShowMessage("try again...");
            }
        }
    }
}
