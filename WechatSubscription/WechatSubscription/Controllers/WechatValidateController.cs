using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WechatSubscription.DbContexts;
using WechatSubscription.Enums;
using WechatSubscription.Helpers;
using WechatSubscription.Models;

namespace WechatSubscription.Controllers
{
    [Route("api/Wechat5000")]
    [ApiController]
    public class WechatValidateController : ControllerBase
    {
        private readonly WechatDbContext _wechatDbContext;

        public WechatValidateController(IConfiguration configuration, WechatDbContext wechatDbContext)
        {
            Configuration = configuration;
            this._wechatDbContext = wechatDbContext;
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
                        var contents = "";
                        foreach (var reminder in _wechatDbContext.Reminders)
                        {
                            contents += $"下一次{reminder.Name}的时间为:{reminder.NextRemindTime.ToString("yyyy-MM-dd")}，还剩{(reminder.NextRemindTime - DateTime.Now).Days}天\r";
                        }
                        
                        wechatMessage = new WechatSendTextMessage()
                        {
                            FromUserName = dic["ToUserName"],
                            ToUserName = dic["FromUserName"],
                            Content = contents.Trim('\r')
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