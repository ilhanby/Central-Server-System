using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataloggerMerkez2.Models
{
    public class Systems
    {
        public string SystemName { get; set; }
        public bool IsServerActive { get; set; }
        public bool IsSmsActive { get; set; }
        public bool IsCallActive { get; set; }
        public int OfflineGateway { get; set; }
        public int OnlineGateway { get; set; }
        public List<Gateway> GatewayList { get; set; }
    }

}
