using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WechatSubscription.WechatAPI
{
    public static class WechatContrants
    {
        public static string WechatToken { get; set; }
        public static string GrantType { get; set; }
        public static string Appid { get; set; }
        public static string Secret { get; set; }

        public static string WechatAccessToken { get; set; }

        public static DateTime ExpireInTime { get; set; }
    }
}
