using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WechatSubscription.Enums
{
    public enum WechatMsgType
    {
        Text = 0,
        Image = 1,
        Voice = 2,
        Video = 3,
        Shortvideo = 4,
        Location = 5,
        Link = 6,
        Event = 7
    }
}
