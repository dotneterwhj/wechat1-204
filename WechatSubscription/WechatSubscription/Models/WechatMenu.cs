using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WechatSubscription.Models
{
    public class WechatMenu
    {
        public List<Button> Button { get; set; }
    }

    public class Button
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public List<SubButton> Sub_button { get; set; }
    }

    public class SubButton
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Appid { get; set; }
        public string Pagepath { get; set; }
        public string Key { get; set; }
    }

}
