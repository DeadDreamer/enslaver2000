using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnslaverFrontEnd.Models
{
    public class MessageBodyOfAdminForm
    {
        public MessageBodyOfAdminForm(bool isVisible)
        {
            IsVisible = isVisible;
        }

        public bool IsVisible { get; set; }
    }
}
