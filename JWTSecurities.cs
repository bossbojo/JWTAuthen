using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace JWT.Authen
{
    public class JWTSecurities
    {
        private static UTF8Encoding encodeing = new UTF8Encoding();

        private static string SecurityKey = "Kraken Project";

        public static string JWTEncode(object payload)
        {
            try
            {
                return JWT.Encode(payload, UTF8Encoding.ASCII.GetBytes(SecurityKey), JwsAlgorithm.HS256);
            }
            catch
            {
                return null;
            }
        }
        public static T JWTDecode<T>(string token)
        {
            try
            {
                return JWT.Decode<T>(token, UTF8Encoding.ASCII.GetBytes(SecurityKey), JwsAlgorithm.HS256);
            }
            catch
            {
                return default(T);
            }
        }
        public static string Encode<T>(T data, int time = 30)
        {
            JWTData<T> payload = new JWTData<T> { data = data, exp = Authentication.GetNow.AddMinutes(time).ToUnixTimeSeconds() };
            return JWTEncode(payload);
        }
        public static T Decode<T>(string token)
        {
            JWTData<T> payload = JWTDecode<JWTData<T>>(token);
            if (payload != null)
            {
                if (payload.exp >= Authentication.GetNow.ToUnixTimeSeconds())
                {
                    return payload.data;
                }
            }
            return default(T);
        }
    }
    public class JWTData<T>
    {
        public T data { get; set; }
        public long exp { get; set; }
    }
}