using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverFrontEnd.Logic
{
    public class FormHelper
    {
        [DllImport("user32")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint itemId, uint uEnable);

        public static void DisableCloseButton(BaseForm form)
        {            
            EnableMenuItem(GetSystemMenu(form.Handle, false), 0xF060, 1);
        }

        public static void EnableCloseButton(BaseForm form)
        {         
            EnableMenuItem(GetSystemMenu(form.Handle, false), 0xF060, 0);
        }
    }
}
