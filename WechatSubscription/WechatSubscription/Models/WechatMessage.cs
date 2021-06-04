using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WechatSubscription.Models
{
    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(ElementName = "xml", Namespace = "", IsNullable = false)]
    public abstract partial class WechatMessage
    {

        private string toUserNameField;

        private string fromUserNameField;

        private long createTimeField;

        private string msgTypeField;



        /// <remarks/>
        public string ToUserName
        {
            get
            {
                return this.toUserNameField;
            }
            set
            {
                this.toUserNameField = value;
            }
        }

        /// <remarks/>
        public string FromUserName
        {
            get
            {
                return this.fromUserNameField;
            }
            set
            {
                this.fromUserNameField = value;
            }
        }

        /// <remarks/>
        public long CreateTime
        {
            get
            {
                if (this.createTimeField != 0)
                {
                    return this.createTimeField;
                }

                System.DateTime startTime = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
                long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds;

                return timeStamp;

            }
            set
            {
                this.createTimeField = value;
            }
        }

        /// <remarks/>
        public virtual string MsgType
        {
            get
            {
                return this.msgTypeField;
            }
            set
            {
                this.msgTypeField = value;
            }
        }


    }



}
