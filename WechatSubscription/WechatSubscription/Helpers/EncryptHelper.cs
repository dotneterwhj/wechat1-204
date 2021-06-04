using System;
using System.Security.Cryptography;
using System.Text;

namespace WechatSubscription.Helpers
{
    public static class EncryptHelper
    {
        #region 用SHA1加密字符串

        /// <summary>
        /// 用SHA1加密字符串
        /// </summary>
        /// <param name="source">要扩展的对象</param>
        /// <param name="isReplace">是否替换掉加密后的字符串中的"-"字符</param>
        /// <param name="isToLower">是否把加密后的字符串转小写</param>
        /// <returns></returns>
        public static string SHA1Encrypt(this string source, bool isReplace = true, bool isToLower = false)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(source));
            string shaStr = BitConverter.ToString(hash);
            if (isReplace)
            {
                shaStr = shaStr.Replace("-", "");
            }
            if (isToLower)
            {
                shaStr = shaStr.ToLower();
            }
            return shaStr;
        }

        #endregion 用SHA1加密字符串
    }
}