using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnslaverFrontEnd.Models;

namespace EnslaverFrontEnd.Contracts
{
    public interface ILoginView : IBaseView
    {
        string GetPasswordString();
        FormMessage GetFormMessage();
        void ShowMessage(string message);
        void CloseView();

        event EventHandler<EventArgs> OkButtonClick;
    }
}
