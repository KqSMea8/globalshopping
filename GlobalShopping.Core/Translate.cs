
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GlobalShopping.Core
{
    public class Translate
    {
        //private static ConnectionMultiplexer redis;

        static Translate()
        {
            //var option = ConfigurationOptions.Parse(WebSetting.RedisForTranslate);
            //option.ClientName = "translate.client";
            //option.AllowAdmin = true;
            //option.AbortOnConnectFail = false;
            //option.ConnectRetry = 3;
            //redis = ConnectionMultiplexer.Connect(option);
        }

        /// <summary>
        /// 谷歌翻译
        /// </summary>
        /// <param name="sourceText">需要翻译的内容</param>
        /// <param name="sourceLanguageCode">源语言</param>
        /// <param name="targetLanguageCode">目标语言</param>
        /// <returns></returns>
        public static Dictionary<string, string> TranslateForGoogleApi(List<string> txtList, string sourceLanguageCode, string targetLanguageCode)
        {
            //todo use redis to translate?
            //var db = redis.GetDatabase(4);
            //var keys = new RedisKey[txtList.Count];
            //for (int i = 0; i < txtList.Count; i++)
            //{
            //    keys[i] = "TR:" + txtList[i];
            //}
            //var items = db.StringGet(keys);
            var items = txtList;

            var result = new Dictionary<string, string>();

            for (int i = 0; i < txtList.Count; i++)
            {
                var item = items[i];
                //if (item.HasValue)
                //{
                //    result[txtList[i]] = item.ToString();
                //}

                result[txtList[i]] = item.ToString();
            }
            return result;
        }
        /// <summary>
        /// 翻译缓存
        /// </summary>
        static ConcurrentDictionary<string, string> alreadyTranslateds = new ConcurrentDictionary<string, string>();
        static Regex regAllEnglish = new Regex("^[^\u4e00-\u9fa5]+$");
        static Regex regEnglish = new Regex("[^\u4e00-\u9fa5]+");
        /// <summary>
        /// 英文翻译
        /// </summary>
        /// <param name="keyword">需要翻译的内容</param>
        /// <returns></returns>
        public static string TranslationToEn(string keyword, bool need = true, bool cache = false)
        {
            if (!need)
                return keyword;
            try
            {
                if (string.IsNullOrEmpty(keyword) || regAllEnglish.IsMatch(keyword))
                    return keyword;
                string result;
                if (cache || !alreadyTranslateds.TryGetValue(keyword, out result))
                {
                    result = TranslationToEn(new List<string>() { keyword }, cache).First().Value;
                    if (cache)
                        alreadyTranslateds.TryAdd(keyword, result);
                }
                return result;
            }
            catch
            {
                return keyword;
            }
        }
        /// <summary>
        /// 英文翻译
        /// </summary>
        /// <param name="keywords">需要翻译的内容列表</param>
        /// <returns></returns>
        public static Dictionary<string, string> TranslationToEn(List<string> keywords, bool cache = false)
        {
            if (keywords.Count == 0)
                return new Dictionary<string, string>();
            Dictionary<string, string> model = new Dictionary<string, string>();
            List<string> needs = new List<string>();
            try
            {
                foreach (var keyword in keywords.Distinct())
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(keyword) && !regAllEnglish.IsMatch(keyword))
                        {
                            string result;
                            if (cache || !alreadyTranslateds.TryGetValue(keyword, out result))
                            {
                                needs.Add(keyword);
                            }
                            else
                            {
                                model.Add(keyword, result);
                            }
                        }
                    }
                    catch
                    {
                        model.Add(keyword, keyword);
                    }
                }
                var datas = TranslateForGoogleApi(needs, "zh-CN", "en");
                foreach (var data in datas)
                {
                    model.Add(data.Key, data.Value);
                    if (cache)
                        alreadyTranslateds.TryAdd(data.Key, data.Value);
                }
            }
            catch { }
            return model;
        }
    }
}
