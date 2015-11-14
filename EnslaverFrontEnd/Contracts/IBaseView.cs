using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverFrontEnd.Contracts
{
    public interface IBaseView
    {        
        event EventHandler<EventArgs> Init;
        event EventHandler<EventArgs> ExitClick;
    }
}
