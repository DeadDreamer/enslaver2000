using EnslaverCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverCore.Logic
{
    public class SimpleFileLogger
    {
        public static string RecordTemplate = "{0}|{1}|=>{2}";

        public static string LogFilePath = Constants.DefaultLogFilePath;

        public static void Write(string fileName, string message, LogType logtype)
        {
            try
            {
                StreamWriter strwr = new StreamWriter(fileName, true);
                strwr.Write(string.Format(RecordTemplate, System.DateTime.UtcNow, logtype.ToString(), message));
                strwr.Flush();
                strwr.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in file LogWriter..." + ex.Message);
            }
        }

        public static void Write(string fileName, string message)
        {
            Write(fileName, message, Constants.DefaultLogType);
        }

        public static void Write(string message)
        {
            Write(LogFilePath, message);
        }

        public static void Write(string message, LogType logType)
        {
            Write(LogFilePath, message, logType);
        }

        public static void Write(Exception ex)
        {
            Write(string.Format("Message-> {0} | StackTrace-> {1} | Source-> {2} | InnerException-> {3}", ex.Message, ex.StackTrace, ex.Source, ex.InnerException, ex.InnerException));
        }
    }
}
