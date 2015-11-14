using EnslaverFrontEnd.Contracts;
using EnslaverFrontEnd.Logic;
using EnslaverFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnslaverCore.Logic.Sound;

namespace EnslaverFrontEnd.Presenters
{
    public class MainFormPresenter : BasePresenter
    {
        public MainFormPresenter(IBaseView view)
            : base(view)
        {
            (this.View as IMainView).OnChangeCamDevice += MainFormPresenter_OnChangeCamDevice;
            (this.View as IMainView).OnStartOrStopClick += MainFormPresenter_OnStartOrStopClick;
            (this.View as IMainView).OnAdminClick += new EventHandler<EventArgs>(MainFormPresenter_OnAdminClick);
            AppGlobalContext.GetInstance().CamHelper.OnNewFrame += CamHelper_OnNewFrame;
        }

        void MainFormPresenter_OnAdminClick(object sender, EventArgs e)
        {
            
            AppGlobalContext.GetInstance().ShowForm(null, (long)FormTypes.AdminForm, new FormMessage() { });
        }

        void CamHelper_OnNewFrame(object sender, EnslaverCore.Logic.CamEvent e)
        {
            (this.View as IMainView).SetCurrentBitmap(e.BitmapFromCam);
        }

        void MainFormPresenter_OnStartOrStopClick(object sender, EventArgs e)
        {
            if (AppGlobalContext.GetInstance().CamHelper.IsActive)
            {
                AppGlobalContext.GetInstance().CamHelper.Stop();
            }
            else
            {
                AppGlobalContext.GetInstance().CamHelper.Start();
            }
            (this.View as IMainView).SetCamIsActive(AppGlobalContext.GetInstance().CamHelper.IsActive);
        }

        void MainFormPresenter_OnChangeCamDevice(object sender, EventArgs e)
        {
            var selectedDevice = (this.View as IMainView).GetCurrentDevice();
            if (selectedDevice != null)
            {
                AppGlobalContext.GetInstance().CamHelper.SelectCamDeviceByMonikerString(selectedDevice.MonikerString);
                (this.View as IMainView).SetCamIsActive(AppGlobalContext.GetInstance().CamHelper.IsActive);
            }
        }

        public override void OnView_Init(object sender, EventArgs e)
        {
            (this.View as IMainView).SetCamDevices(AppGlobalContext.GetInstance().CamHelper.GetListOfDevices());

            Speaker.Say("Поработитель запущен");
        }
    }
}
