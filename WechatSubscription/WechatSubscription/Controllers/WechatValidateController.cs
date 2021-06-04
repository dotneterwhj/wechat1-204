using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WechatSubscription.Enums;
using WechatSubscription.Helpers;
using WechatSubscription.Models;

namespace WechatSubscription.Controllers
{
    [Route("api/Wechat5000")]
    [ApiController]
    public class WechatValidateController : ControllerBase
    {
        public WechatValidateController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Validate([FromQuery] WechatSign wechatSign)
        {
            if (wechatSign == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(wechatSign.Signature) || string.IsNullOrEmpty(wechatSign.Timestamp) || string.IsNullOrEmpty(wechatSign.Nonce))
            {
                return BadRequest();
            }

            List<string> lists = new List<string>();

            lists.Add(Configuration.GetSection("WeChatToken").Value);
            lists.Add(wechatSign.Timestamp);
            lists.Add(wechatSign.Nonce);

            var sign = string.Join("", lists.OrderBy(s => s)).SHA1Encrypt();

            if (!wechatSign.Signature.Equals(sign, StringComparison.CurrentCultureIgnoreCase))
            {
                return BadRequest();
            }

            if ("GET".Equals(Request.Method, StringComparison.CurrentCultureIgnoreCase))
            {
                return Ok(wechatSign.Echostr);
            }
            else if ("POST".Equals(Request.Method, StringComparison.CurrentCultureIgnoreCase))
            {
                var buffer = new byte[Request.ContentLength.Value];
                var bytes = await Request.Body.ReadAsync(buffer, 0, buffer.Length);
                var result = Encoding.UTF8.GetString(buffer);

                var dic = XmlHelper.XmlToDic(result);

                WechatMessage wechatMessage = null;

                if (dic["MsgType"].Equals(WechatMsgType.Text.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    if (dic["Content"] == "1")
                    {
                        var ppExpireTime = new DateTime(2021, 9, 1);
                        var udExpireTime = new DateTime(2021, 12, 1);
                        var roExpireTime = new DateTime(2022, 12, 1);
                        var prExpireTime = new DateTime(2021, 6, 15);
                        wechatMessage = new WechatSendTextMessage()
                        {
                            FromUserName = dic["ToUserName"],
                            ToUserName = dic["FromUserName"],
                            Content = $"下一次pp棉滤芯更换的时间为:{ppExpireTime.ToString("yyyy-MM-dd")}，还剩{(ppExpireTime - DateTime.Now).Days}天\r" +
                            $"下一次活性炭滤芯更换的时间为:{udExpireTime.ToString("yyyy-MM-dd")}，还剩{(udExpireTime - DateTime.Now).Days}天\r" +
                            $"下一次RO反渗透膜更换的时间为:{roExpireTime.ToString("yyyy-MM-dd")}，还剩{(roExpireTime - DateTime.Now).Days}天\r" +
                            $"下一次前置过滤器清洗的时间为:{prExpireTime.ToString("yyyy-MM-dd")}，还剩{(prExpireTime - DateTime.Now).Days}天\r"
                        };
                    }
                    else if (dic["Content"] == "2")
                    {

                    }
                }
                else if (dic["MsgType"].Equals(WechatMsgType.Event.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    if (dic["Event"] == "subscribe")
                    {
                        wechatMessage = new WechatSendTextMessage()
                        {
                            FromUserName = dic["ToUserName"],
                            ToUserName = dic["FromUserName"],
                            Content = "感谢您的订阅\r回复【1】查看最近提醒事项\r回复【2】..."
                        };
                    }
                    else if (dic["Event"] == "unsubscribe")
                    {
                        wechatMessage = new WechatSendTextMessage()
                        {
                            FromUserName = dic["ToUserName"],
                            ToUserName = dic["FromUserName"],
                            Content = "再见..."
                        };
                    }
                }

                var xmlSendStr = XmlHelper.ObjectToXmlSerializer(wechatMessage);

                return Ok(xmlSendStr);
            }

            return Ok();
        }
    }
}