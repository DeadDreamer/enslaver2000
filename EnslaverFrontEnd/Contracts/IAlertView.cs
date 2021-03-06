﻿using EnslaverFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverFrontEnd.Contracts
{
    public interface IAlertView : IBaseView
    {
        FormMessage GetFormMessage();
        void SetInfoMessage(string infoMessage);
    }
}
