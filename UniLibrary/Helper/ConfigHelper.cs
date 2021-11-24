using System;
using System.Configuration;

namespace UniLibrary.Helper
{
    public static class ConfigHelper
    {
        /// <summary>
        /// Trả về setting trong app.config hay web.config
        /// </summary>
        public static string GetSettingWithDefault(this string key, string defaultValue = "")
        {
            string value = "";
            try
            {
                if (string.IsNullOrWhiteSpace(key)) return "";
                var appSettings = ConfigurationManager.AppSettings;
                var v = appSettings[key];
                if (v == null) return "";
                value = appSettings[key];
            }
            catch(Exception ex)
            {
                value = defaultValue;
            }
            return value;
        }
        /// <summary>
        /// Lấy UrlHost
        /// </summary>
        public static string ToUrlFull(this string uri)
        {
            if (string.IsNullOrEmpty(uri)) return ToUrlConfig();
            return ToUrlConfig() + ("@" + uri).Replace("@/", "/").Replace("@", "/");
        }

        public static string ToUrlConfig()
        {
            return GetSettingWithDefault("URL", "https://dehoctot.vn");
        }
    }
}