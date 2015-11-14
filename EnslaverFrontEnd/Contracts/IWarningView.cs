using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnslaverFrontEnd.Models;

namespace EnslaverFrontEnd.Contracts
{
    public interface IWarningView : IBaseView
    {
        void HidePlayer();
        FormMessage GetFormMessage();
        void ShowWarningMessage(string text);
        void ShowVideo(string uri);
    }
}
