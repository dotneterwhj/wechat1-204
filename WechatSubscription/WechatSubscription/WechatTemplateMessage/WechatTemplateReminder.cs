using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WechatSubscription.WechatTemplateMessage
{
    public class WechatTemplateReminder
    {
        public const string RemindTemplate = "回复【1】查看最近提醒事项\r" +
                            "回复【2 事项 周期】设置提醒事项\r" +
                            "\t(举例：2 打扫房间 7)\r\t表示每隔7天提醒打扫房间\r" +
                            "回复【3 Id】删除提醒事项\r" +
                            "\t(举例：3 2)\r\t表示删除Id=2的提醒事项";

        public const string Reply2Template = "回复【2 事项 周期】设置提醒事项\r\t例：2 打扫卫生 7\r\t表示每隔7天提醒打扫卫生";

        public const string Reply3Template = "回复【3 Id】删除提醒事项\r\t例：3 2\r\t表示删除Id=2的提醒事项";

        public const string Example2Template = "您输入的格式不正确，例：2 打扫房间 7";

        public const string Example3Template = "您输入的格式不正确，例：3 2";
    }
}
