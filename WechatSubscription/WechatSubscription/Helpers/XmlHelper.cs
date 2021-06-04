using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WechatSubscription.Helpers
{
    public class XmlHelper
    {
        public static Dictionary<string, string> XmlToDic(string xml)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(xml);
           
            foreach (XmlNode item in xmlDocument.FirstChild.ChildNodes)
            {
                dic.Add(item.Name, item.InnerText);
            }

            return dic;
        }


        public static string ObjectToXmlSerializer(Object Obj)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            //去除xml声明
            settings.OmitXmlDeclaration = true;
            settings.Encoding = Encoding.Default;
            System.IO.MemoryStream mem = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(mem, settings))
            {
                //去除默认命名空间xmlns:xsd和xmlns:xsi
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                XmlSerializer formatter = new XmlSerializer(Obj.GetType());
                formatter.Serialize(writer, Obj, ns);
            }
            return Encoding.Default.GetString(mem.ToArray());
        }

    }
}
