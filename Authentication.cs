using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JWT.Authen
{
    public class Authentication
    {
        /// <summary>
        /// เป็น function ที่ใช้สำหรับทำ signin
        /// </summary>
        /// <typeparam name="T">type model ที่ต้องการ encode</typeparam>
        /// <param name="Model">model ที่ต้องการ encode</param>
        /// <param name="time">time ที่ต้องการ encode</param>
        /// <returns>คืนค่าเป็น token</returns>
        public static string SetAuthentication<T>(T Model,int time = 60) {
            Authen<T> model = new Authen<T>
            {
                Auth = Model,
                exp = GetNow.AddMinutes(time).ToUnixTimeSeconds()
            };
            return JWTSecurities.JWTEncode(model);
        }
        /// <summary>
        /// เป็น function เพื่อ get เอาผู้ใช้งานปัจจุบันของระบบ
        /// </summary>
        /// <typeparam name="T">type model ที่ต้องการ decode</typeparam>
        /// <returns>คือค่าเป็น Authen</returns>
        public static Authen<T> GetAuthentication<T>()
        {
            string token = HttpContext.Current.Request.Headers["Authorization"];
            if (token != null) {
                Authen<T> decode = JWTSecurities.JWTDecode<Authen<T>>(token);
                if (decode.Auth != null) {
                    bool check = decode.exp >= GetNow.ToUnixTimeSeconds();
                    if (check) {
                        return decode;
                    }
                }
            }
            return null;
        }
        public static DateTimeOffset GetNow
        {
            get
            {
                TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var destination = TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, timeZoneInfo);
                return destination;
            }
        }
    }
    public class Authen<T> {
        public T Auth { get; set; }
        public long exp { get; set; }
    }
}