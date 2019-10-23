using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataloggerMerkez.Models
{
    public class Gateway
    {
        public string GatewayName { get; set; }
        public bool GatewayIsActive { get; set; }
        public int GatewayActiveCount { get; set; }
        public int GatewayPassiveCount { get; set; }

        
    }
   
       
}
