using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WechatSubscription.Enums;

namespace WechatSubscription.Models
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "")]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "xml", Namespace = "", IsNullable = false)]
    public class WechatSendTextMessage : WechatMessage
    {
        public string Content { get; set; }

        public override string MsgType { get; set; } = WechatMsgType.Text.ToString().ToLower();
    }
}
