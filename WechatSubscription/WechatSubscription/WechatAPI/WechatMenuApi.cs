using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WechatSubscription.Enums;
using WechatSubscription.WechatAPI;

namespace WechatSubscription.WechatAPI
{
    public class WechatMenuApi
    {

        public async Task<bool> CreateMenuAsync()
        {
            await WechatAPI.WechatAccessTokenApi.GetWechatAccessToken();

            using (HttpClient client = new HttpClient())
            {
                string url = $"https://api.weixin.qq.com/cgi-bin/menu/create?access_token={WechatContrants.WechatAccessToken}";

                Models.WechatMenu wechatMenu = new Models.WechatMenu
                {
                    Button = new List<Models.Button>
                    {
                        new Models.Button
                        {
                            Name = "查看提醒项",
                            Key = "reminder_key",
                            Type = WechatMenuType.Click.ToString().ToLower()
                        },
                        new Models.Button
                        {
                            Name = "帮助",
                            Key = "helper_key",
                            Type = WechatMenuType.Click.ToString().ToLower()
                        }
                    }
                };

                Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                var httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(wechatMenu, jsonSerializerSettings));

                var reponse = await client.PostAsync(url, httpContent);

                if (reponse.IsSuccessStatusCode)
                {
                    var result = await reponse.Content.ReadAsStringAsync();

                    var wechatMenuResult = Newtonsoft.Json.JsonConvert.DeserializeObject<WechatMenuResult>(result);

                    return wechatMenuResult.Errcode == 0;
                }
            }

            return false;
        }

        public async Task<bool> DeleteMenuAsync()
        {
            await WechatAPI.WechatAccessTokenApi.GetWechatAccessToken();

            using (HttpClient client = new HttpClient())
            {
                string url = $"https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={WechatContrants.WechatAccessToken}";

                var reponse = await client.GetAsync(url);

                if (reponse.IsSuccessStatusCode)
                {
                    var result = await reponse.Content.ReadAsStringAsync();

                    var wechatMenuResult = Newtonsoft.Json.JsonConvert.DeserializeObject<WechatMenuResult>(result);

                    return wechatMenuResult.Errcode == 0;
                }
            }

            return false;
        }
    }
}
