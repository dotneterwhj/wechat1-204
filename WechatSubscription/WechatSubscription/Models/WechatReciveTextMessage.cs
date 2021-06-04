using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WechatSubscription.Models
{
    public class WechatReciveTextMessage : WechatMessage
    {
        private string contentField;

        private ulong msgIdField;
        /// <remarks/>
        public string Content
        {
            get
            {
                return this.contentField;
            }
            set
            {
                this.contentField = value;
            }
        }

        /// <remarks/>
        public ulong MsgId
        {
            get
            {
                return this.msgIdField;
            }
            set
            {
                this.msgIdField = value;
            }
        }
    }
}
