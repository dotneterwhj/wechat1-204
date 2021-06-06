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
                    var userContent = dic["Content"].Trim(' ');
                    if (userContent == "1")
                    {
                        var contents = "";
                        foreach (var reminder in _wechatDbContext.Reminders.Where(r => !r.IsDelete))
                        {
                            TimeSpan timeSpan = new TimeSpan(reminder.NextRemindTime.Ticks - DateTime.Now.Ticks);
                            contents += $"Id={reminder.Id},下一次{reminder.Name}的时间为:{reminder.NextRemindTime.ToString("yyyy-MM-dd")}，还剩{Math.Ceiling(timeSpan.TotalDays)}天\r";
                        }

                        if (string.IsNullOrEmpty(contents))
                        {
                            contents = "暂无提醒事项\r回复【2 事项 周期】设置提醒事项\r\t例：2 打扫卫生 7\r\t表示每隔7天提醒打扫卫生";
                        }

                        wechatMessage = new WechatSendTextMessage()
                        {
                            FromUserName = dic["ToUserName"],
                            ToUserName = dic["FromUserName"],
                            Content = contents.Trim('\r')
                        };
                    }
                    else if (userContent[0] == '2')
                    {
                        wechatMessage = new WechatSendTextMessage()
                        {
                            FromUserName = dic["ToUserName"],
                            ToUserName = dic["FromUserName"]
                        };
                        var tempArray = userContent.Split(" ");
                        if (tempArray.Length != 3)
                        {
                            ((WechatSendTextMessage)wechatMessage).Content = "您输入的格式不正确，例：2 打扫房间 7";
                        }
                        else
                        {
                            if (!int.TryParse(tempArray[2], out int remindDays))
                            {
                                ((WechatSendTextMessage)wechatMessage).Content = "您输入的格式不正确，例：2 打扫房间 7";
                            }
                            else
                            {
                                await _wechatDbContext.Reminders.AddAsync(new EFModels.Reminder
                                {
                                    Name = tempArray[1],
                                    PreRemindTime = DateTimeOffset.Now,
                                    NextRemindTime = new DateTimeOffset(DateTime.Now.AddDays(remindDays)),
                                    IntervalDays = remindDays,
                                    CreateTime = DateTimeOffset.Now,
                                    Creator = dic["FromUserName"],
                                    LastModifyTime = DateTimeOffset.Now,
                                    LastModifer = dic["FromUserName"],
                                    IsDelete = false
                                });

                                await _wechatDbContext.SaveChangesAsync();

                                ((WechatSendTextMessage)wechatMessage).Content = "已设置完成，回复【1】查看";
                            }
                        }
                    }
                    else if (userContent[0] == '3')
                    {
                        wechatMessage = new WechatSendTextMessage()
                        {
                            FromUserName = dic["ToUserName"],
                            ToUserName = dic["FromUserName"]
                        };
                        var tempArray = userContent.Split(" ");
                        if (tempArray.Length != 2)
                        {
                            ((WechatSendTextMessage)wechatMessage).Content = "您输入的格式不正确，例：3 2";
                        }
                        else
                        {
                            if (!int.TryParse(tempArray[1], out int findId))
                            {
                                ((WechatSendTextMessage)wechatMessage).Content = "您输入的格式不正确，例：3 2";
                            }
                            var findReminder = await _wechatDbContext.Reminders.AsNoTracking().Where(r => r.Id == findId).FirstOrDefaultAsync();
                            if (findReminder != null)
                            {
                                findReminder.IsDelete = true;
                                _wechatDbContext.Reminders.Update(findReminder);
                                await _wechatDbContext.SaveChangesAsync();
                                ((WechatSendTextMessage)wechatMessage).Content = $"您已成功删除提醒事项,{findReminder.Name}";
                            }
                            else
                            {
                                ((WechatSendTextMessage)wechatMessage).Content = $"您要删除Id={findId}提醒事项不存在";
                            }
                        }
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
                            Content = "感谢您的订阅\r" +
                            "回复【1】查看最近提醒事项\r" +
                            "回复【2 事项 周期】设置提醒事项\r" +
                            "\t(举例：2 打扫房间 7)\r\t表示每隔7天提醒打扫房间\r" +
                            "回复【3 Id】删除提醒事项\r" +
                            "\t(举例：3 2)\r\t表示删除Id=2的提醒事项"
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