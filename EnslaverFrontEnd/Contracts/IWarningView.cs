using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnslaverFrontEnd.Contracts
{
    public interface IWarningView : IBaseView
    {
        void ShowWarningMessage(string text);
        void ShowVideo(string uri);
    }
}
