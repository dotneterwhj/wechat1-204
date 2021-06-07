using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WechatSubscription.WechatAPI
{
    public class WechatAccessTokenResult
    {
        public string Access_token { get; set; }
        public int Expires_in { get; set; }
    }

}
