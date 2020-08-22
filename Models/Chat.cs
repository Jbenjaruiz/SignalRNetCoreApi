using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApi.Models
{
    public partial class Chat
    {
        public int ct_usr_sender { get; set; }
        public int ct_usr_receptor { get; set; }
        public string ct_mensaje { get; set; }
    }
}
