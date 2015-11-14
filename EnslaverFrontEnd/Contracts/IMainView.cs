using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverFrontEnd.Contracts
{
    public interface IMainView : IBaseView
    {
        void SetCamDevices(List<FilterInfo> devices);
        void SetCamIsActive(bool isActive);

        event EventHandler<EventArgs> OnChangeCamDevice;
        event EventHandler<EventArgs> OnAdminClick;

        event EventHandler<EventArgs> OnStartOrStopClick;

        FilterInfo GetCurrentDevice();
        void SetCurrentBitmap(Bitmap bitmap);

    }
}
