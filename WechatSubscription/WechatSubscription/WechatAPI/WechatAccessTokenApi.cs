using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WechatSubscription.WechatAPI
{
    public class WechatAccessTokenApi
    {
        public static async Task GetWechatAccessToken()
        {
            WechatContrants.WechatAccessToken = "45_IhonqknndteR_R7VjoJXKj-sVsf8tpAP2lxUe-UH5z4ueW_5pm5EijPJZ-S-TXBGH-BNJbEVjsBRe5X-prVw-PlGpjZ40TW0IklE44IQ7LP5U8hHMqane3wg7KLePaFAdrcWk37wOe2z19CLNSIfAHAUZP";

            // 表示快要过期了
            if (DateTime.Now.AddMinutes(5) > WechatContrants.ExpireInTime)
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={WechatContrants.Appid}&secret={WechatContrants.Secret}";

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        var accessTokenResult = Newtonsoft.Json.JsonConvert.DeserializeObject<WechatAccessTokenResult>(result);

                        WechatContrants.WechatAccessToken = accessTokenResult.Access_token;

                        WechatContrants.ExpireInTime = DateTime.Now.AddSeconds(accessTokenResult.Expires_in);
                    }
                }
            }
        }
    }
}
