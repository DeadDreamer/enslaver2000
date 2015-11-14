using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnslaverFrontEnd.Models
{
  public   class MessageBodyOfLoginForm
    {
        public MessageBodyOfLoginForm(FormTypes type)
        {
            this.RedirectToForm= type;
            
        }

        public FormTypes RedirectToForm { get; set; }
    }
}
