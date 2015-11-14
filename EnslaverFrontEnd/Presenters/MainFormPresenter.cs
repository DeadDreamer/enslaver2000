using EnslaverFrontEnd.Contracts;
using EnslaverFrontEnd.Logic;
using EnslaverFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnslaverCore.Logic.Sound;
using EnslaverCore;
using System.IO;


namespace EnslaverFrontEnd.Presenters
{
    public class MainFormPresenter : BasePresenter
    {
        private System.Timers.Timer timer = new System.Timers.Timer(AppGlobalContext.GetInstance().TimerPeriodInMilSec);
        private object lockObject = new object();
        private UserStatus userStatusChecker = new UserStatus();

        public MainFormPresenter(IBaseView view)
            : base(view)
        {
            (this.View as IMainView).OnChangeCamDevice += MainFormPresenter_OnChangeCamDevice;
            (this.View as IMainView).OnStartOrStopClick += MainFormPresenter_OnStartOrStopClick;
            (this.View as IMainView).OnAdminClick += new EventHandler<EventArgs>(MainFormPresenter_OnAdminClick);

            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Start();
            System.Windows.Forms.Application.Idle += new EventHandler(Application_Idle);
            AppGlobalContext.GetInstance().CamHelper.OnNewFrame += CamHelper_OnNewFrame;
        }


        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool needToShowWarningForm = false;
            string messageInfo = "";
            string uriToVideoPath = "";
            lock (lockObject)
            {
                UserStates userStatus = userStatusChecker.GetUserStatus(AppGlobalContext.GetInstance().Owner);
                switch (userStatus)
                {
                    case UserStates.HeadNotFound:
                        needToShowWarningForm = true;
                        messageInfo = "{0}, вернитесь на рабочее место!".FormatWithOwner();
                        uriToVideoPath = Directory.GetCurrentDirectory() + "\\Resources\\eyeSauron.mp4";
                        break;
                    case UserStates.EyesNotFound:
                        needToShowWarningForm = true;
                        messageInfo = "{0}, смотрите в монитор!".FormatWithOwner();
                        uriToVideoPath = Directory.GetCurrentDirectory() + "\\Resources\\eyeSauron.mp4";
                        break;
                    case UserStates.Smiling:
                        needToShowWarningForm = true;
                        messageInfo = "{0}, перестаньте улыбаться!".FormatWithOwner();
                        uriToVideoPath = Directory.GetCurrentDirectory() + "\\Resources\\eyeSauron.mp4";
                        break;
                    case UserStates.Fine:
                    default:
                        needToShowWarningForm = false;
                        break;
                }
                userStatusChecker.ResetCounters();
                if (needToShowWarningForm)
                {
                    List<BaseForm> forms = AppGlobalContext.GetInstance().FindFormsByType((long)FormTypes.WarningForm);
                    if (forms != null && forms.Count > 0)
                    {
                        //Закрываем формы
                        forms.ForEach(c => c.ForceClose());
                    }

                    //Показываем новую форму...
                /*    object messageBody = (object)(new MessageBodyOfWarningForm(messageInfo, uriToVideoPath));
                    System.Threading.Thread thread = new System.Threading.Thread(() =>
                    {
                        AppGlobalContext.GetInstance().ShowForm(null, (long)FormTypes.WarningForm, new FormMessage() { Body = messageBody });
                    });
                    thread.SetApartmentState(System.Threading.ApartmentState.STA);
                    thread.Start();*/                   

                }
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            lock (lockObject)
            {
                List<HeadInformation> headInformation = EnslaverCore.Brain.GetInformation();
                userStatusChecker.CheckFrame(headInformation);
            }
        }

        void MainFormPresenter_OnAdminClick(object sender, EventArgs e)
        {
            AppGlobalContext.GetInstance().ShowSingletoneForm(null, (long)FormTypes.LoginForm, new FormMessage() { Body = new MessageBodyOfLoginForm(FormTypes.AdminForm) });
            //AppGlobalContext.GetInstance().ShowForm(null, (long)FormTypes.AdminForm, new FormMessage() { });
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
