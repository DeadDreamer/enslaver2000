using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;

namespace EnslaverFrontEnd.Contracts
{
    public interface IAdminView : IBaseView
    {
        event EventHandler<EventArgs> OnStartClick;
        event EventHandler<EventArgs> OnStopClick;
        event EventHandler<EventArgs> OnTeachClick;
        string GetUserName();
        Capture GetCapture();
        void SetImage(IImage someImage);
    }
}
