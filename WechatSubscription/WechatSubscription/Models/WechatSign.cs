using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WechatSubscription.Models
{
    public class WechatSign
    {
        public string Signature { get; set; }
        public string Timestamp { get; set; }
        public string Nonce { get; set; }
        public string Echostr { get; set; }
    }
}
