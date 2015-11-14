using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnslaverFrontEnd.Models
{
    public class MessageBodyOfWarningForm
    {
        public MessageBodyOfWarningForm(string messageText, string pathToVideoFile)
        {
            this.MessageText = messageText;
            this.PathToVideoFile = pathToVideoFile;
        }

        public string PathToVideoFile { get; set; }

        public string MessageText { get; set; }
    }
}
