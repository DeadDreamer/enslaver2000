using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverCore.Models
{
    public enum LogType : int
    {
        Debug = 100,
        Info = 0,
        Warning = -100,
        Error = -200
    }
}
