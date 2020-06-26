using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisableDispatchService
{
    public class ConfigModel
    {
        public bool Dispatch { get; set; }
        public bool Traffic { get; set; }
        public bool DisablePedsDrop { get; set; }
        public float Destinity { get; set; }
    }
}
