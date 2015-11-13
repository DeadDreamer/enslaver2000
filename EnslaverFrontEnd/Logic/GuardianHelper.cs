using EnslaverCore.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverFrontEnd.Logic
{
    public class GuardianHelper
    {
        public const string GuardianLockName = "GuardianLock";

        public const int MaxCountOfGuardians = 5;

        public const string GuardianArgument = "guardian";

        public static string ProcessName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;


        public static List<Process> GetCheckedProcesses()
        {
            int result = 0;
            Process currentProcess = Process.GetCurrentProcess();
            return Process.GetProcesses().Where(c => c.Id != currentProcess.Id).ToList();
        }

        public static int GetCountOfGuardians()
        {
            int result = 0;
            Process currentProcess = Process.GetCurrentProcess();
            return Process.GetProcesses().Count(c => c.ProcessName.Contains(ProcessName) && c.Id != currentProcess.Id);
        }

        public static bool RunAsGuardian()
        {
            int pid = 0;
            return RunAsGuardian(ref  pid);
        }

        public static bool RunAsGuardian(ref int findedPid)
        {
            findedPid = 0;
            IntPtr fileMapHandler = IntPtr.Zero, handlerOfMapView = IntPtr.Zero;
            if (SharedMemoryHelper.OpenSharedMemory(SharedMemoryHelper.NameOfSharedMemory, ref fileMapHandler, ref handlerOfMapView))
            {
                byte[] bytes = null;
                uint size = 0;
                if (SharedMemoryHelper.ReadContentBytesAtOnce(handlerOfMapView, ref bytes, ref size) && bytes != null)
                {
                    string pidOfMainProcess = SharedMemoryHelper.GetUnicodeStringFromBytes(bytes);
                    int pid;
                    if (int.TryParse(pidOfMainProcess, out pid))
                    {
                        findedPid = pid;
                        var checkedProcesses = GuardianHelper.GetCheckedProcesses();
                        if (checkedProcesses.FirstOrDefault(c => c.Id == pid) != null)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static ProcessStartInfo GetHiddenProcess(string path, string args = null)
        {
            ProcessStartInfo startInfo = (args != null) ? new ProcessStartInfo(path, args) : new ProcessStartInfo(path);
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            return startInfo;
        }
    }
}
