using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnslaverCore.Models;
using EnslaverFrontEnd.Logic;
using EnslaverFrontEnd.Views;
using EnslaverFrontEnd.Models;
using EnslaverCore.Logic;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace EnslaverFrontEnd
{
    static class Program
    {
        public static ParameterizedLock Lock = new ParameterizedLock();

        public static Timer guardianTimer = new Timer() { Interval = Constants.StandartTimeIntervalInMilSec },
                            standartTimer = new Timer() { Interval = Constants.StandartTimeIntervalInMilSec };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            if ((args != null && args.Any() && args[0] == GuardianHelper.GuardianArgument) || GuardianHelper.RunAsGuardian())
            {
                RunAsGuardian();
            }
            else
            {
                DisplayForms();
            }
        }

        private static void RunAsGuardian()
        {
            bool exit = false;
            while (!exit)
            {
                try
                {
                    Lock.RunWithLock(GuardianHelper.GuardianLockName, () =>
                    {
                        int pid = 0;
                        if (!GuardianHelper.RunAsGuardian(ref pid))
                        {
                            exit = true;
                            if (pid != -1)
                            {
                                DisplayForms();
                            }
                        }
                    });
                }
                catch (Exception ex)
                {
                    SimpleFileLogger.Write(ex);
                }
                finally
                {
                    SharedMemoryHelper.CloseHandlers(AppGlobalContext.FileMapHandler, AppGlobalContext.HandlerOfMapView);
                }
                if (!exit)
                    Thread.Sleep(5000);
            }
        }

        private static void DisplayForms()
        {
            guardianTimer.Stop();
            standartTimer.Tick += FormsTimer_Tick;
            bool result = SharedMemoryHelper.CreateSharedMemory(SharedMemoryHelper.NameOfSharedMemory, ref AppGlobalContext.FileMapHandler, ref AppGlobalContext.HandlerOfMapView);
            Process currentProcess = Process.GetCurrentProcess();
            var pidAsBytes = SharedMemoryHelper.GetBytesFromUnicodeString(currentProcess.Id.ToString());
            SharedMemoryHelper.WriteBytesAtOnce(AppGlobalContext.HandlerOfMapView, pidAsBytes);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormFactory formFactory = AppGlobalContext.GetInstance();
            standartTimer.Start();
            Application.Run(formFactory.MainApplicationContext);
        }
        
        private static void FormsTimer_Tick(object sender, EventArgs e)
        {
            while (GuardianHelper.GetCountOfGuardians() < GuardianHelper.MaxCountOfGuardians)
            {
                string currentProcessPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                ProcessStartInfo processStartInfo = GuardianHelper.GetHiddenProcess(currentProcessPath, GuardianHelper.GuardianArgument);
                Process.Start(processStartInfo);
            }
        }
    }
}
