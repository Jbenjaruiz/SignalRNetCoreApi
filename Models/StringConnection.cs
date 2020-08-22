using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApi.Models
{
    public partial class StringConnection
    {
        public string source  { get; set; }
        public string database { get; set; }
        public string user { get; set; }
        public string password { get; set; }
    }
}
