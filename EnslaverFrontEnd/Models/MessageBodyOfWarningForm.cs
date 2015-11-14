using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnslaverCore.Logic.Sound;

namespace EnslaverFrontEnd.Models
{
    public class MessageBodyOfWarningForm
    {
        public MessageBodyOfWarningForm(Phrase[] messageText, string pathToVideoFile)
        {
            this.MessageText = messageText;
            this.PathToVideoFile = pathToVideoFile;
        }

        public string PathToVideoFile { get; set; }

        public Phrase[] MessageText { get; set; }
    }
}
