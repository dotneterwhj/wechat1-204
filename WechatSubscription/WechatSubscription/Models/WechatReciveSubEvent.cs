using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WechatSubscription.Enums;

namespace WechatSubscription.Models
{
    public class WechatReciveSubEvent : WechatMessage
    {
        public override string MsgType { get; set; } = WechatMsgType.Event.ToString().ToLower();

        public string Event { get; set; }
    }
}
