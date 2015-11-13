using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnslaverFrontEnd.Models
{
    /// <summary>
    /// Класс предназначен для общения между формами
    /// </summary>
    public class FormMessage
    {
        /// <summary>
        /// Guid того, кто послал сообщение
        /// </summary>
        public Guid SenderGuid;

        /// <summary>
        /// Некоторое сообщение
        /// </summary>
        public Object Body;

        public FormMessage()
        {
        }
        public FormMessage(Guid senderGuid, object body)
        {
            SenderGuid = senderGuid;
            Body = body;
        }
    }
}
