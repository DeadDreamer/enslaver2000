using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverFrontEnd.Contracts
{
    public interface ILoginView : IBaseView
    {
        string GetPasswordString();
        void ShowMessage(string message);

        event EventHandler<EventArgs> OkButtonClick;
    }
}
