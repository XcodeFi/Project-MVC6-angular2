using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;

namespace Graduation.Infrastructure.Core
{
    public sealed class Common
    {
        /// <summary>
        /// Convert String to URL Format
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToUrlString(string str)
        {
            str = (new Regex(@"[-]{2}")).Replace((new Regex(@"[^\w]")).Replace(Ucs2Convert(str.ToLower()), "-"), "-");

            return str;
        }

        public static String Ucs2Convert(String sContent)
        {
            sContent = sContent.Trim();
            const string SUtf8Lower = "a|á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ|đ|e|é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ|i|í|ì|ỉ|ĩ|ị|o|ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ|u|ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự|y|ý|ỳ|ỷ|ỹ|ỵ";

            const string SUtf8Upper = "A|Á|À|Ả|Ã|Ạ|Ă|Ắ|Ằ|Ẳ|Ẵ|Ặ|Â|Ấ|Ầ|Ẩ|Ẫ|Ậ|Đ|E|É|È|Ẻ|Ẽ|Ẹ|Ê|Ế|Ề|Ể|Ễ|Ệ|I|Í|Ì|Ỉ|Ĩ|Ị|O|Ó|Ò|Ỏ|Õ|Ọ|Ô|Ố|Ồ|Ổ|Ỗ|Ộ|Ơ|Ớ|Ờ|Ở|Ỡ|Ợ|U|Ú|Ù|Ủ|Ũ|Ụ|Ư|Ứ|Ừ|Ử|Ữ|Ự|Y|Ý|Ỳ|Ỷ|Ỹ|Ỵ";

            const string SUcs2Lower = "a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|d|e|e|e|e|e|e|e|e|e|e|e|e|i|i|i|i|i|i|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|u|u|u|u|u|u|u|u|u|u|u|u|y|y|y|y|y|y";

            const string SUcs2Upper = "A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|D|E|E|E|E|E|E|E|E|E|E|E|E|I|I|I|I|I|I|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|U|U|U|U|U|U|U|U|U|U|U|U|Y|Y|Y|Y|Y|Y";

            var aUtf8Lower = SUtf8Lower.Split('|');

            var aUtf8Upper = SUtf8Upper.Split('|');

            var aUcs2Lower = SUcs2Lower.Split('|');

            var aUcs2Upper = SUcs2Upper.Split('|');

            var nLimitChar = aUtf8Lower.GetUpperBound(0);

            for (int i = 1; i <= nLimitChar; i++)
            {
                sContent = sContent.Replace(aUtf8Lower[i], aUcs2Lower[i]);

                sContent = sContent.Replace(aUtf8Upper[i], aUcs2Upper[i]);
            }
            string sUCS2regex = @"[A-Za-z0-9- ]";
            string sEscaped =
                new Regex(sUCS2regex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture).
                    Replace(sContent, string.Empty);
            if (string.IsNullOrEmpty(sEscaped))
                return sContent;
            sEscaped = sEscaped.Replace("[", "\\[");
            sEscaped = sEscaped.Replace("]", "\\]");
            sEscaped = sEscaped.Replace("^", "\\^");
            string sEscapedregex = @"[" + sEscaped + "]";
            return
                new Regex(sEscapedregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture)
                    .Replace(sContent, string.Empty);
        }


        public static string SubString(string sSource, int length)
        {
            if (string.IsNullOrEmpty(sSource))
                return string.Empty;
            if (sSource.Length <= length)
                return sSource;

            string mSource = sSource;
            int nLength = length;

            //int m = mSource.Length;
            while (nLength > 0 && mSource[nLength].ToString() != " ")
            {
                nLength--;
            }
            mSource = mSource.Substring(0, nLength);
            return mSource + "...";
        }


        //public static void SendMail(string toList, string ccList, string subject, string body, string site)
        //{
        //    MailMessage message = new MailMessage();
        //    HttpClient smtpClient = new SmtpClient();
        //    //string msg = string.Empty;
        //    try
        //    {
        //        MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["smtp_username"], site);
        //        message.From = fromAddress;
        //        message.To.Add(toList);

        //        if (ccList != null && ccList != string.Empty)
        //            message.CC.Add(ccList);

        //        message.Subject = subject;
        //        message.IsBodyHtml = true;
        //        message.Body = body;
        //        smtpClient.Host = ConfigurationManager.AppSettings["smtp_host"];   // We use gmail as our smtp client
        //        smtpClient.Port = int.Parse(ConfigurationManager.AppSettings["port"]);
        //        smtpClient.EnableSsl = true;
        //        smtpClient.UseDefaultCredentials = true;
        //        smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtp_username"], ConfigurationManager.AppSettings["smtp_password"]);
        //        smtpClient.Send(message);

        //        new CommonLog(HttpContext.Current.Server.MapPath("/log"), "sendemail").ErrorLog("send sucess to email " + toList);
        //    }
        //    catch (Exception ex)
        //    {
        //        new CommonLog(HttpContext.Current.Server.MapPath("/log"), "send-error-email").ErrorLog(ex.ToString());
        //    }
        //}

        /// <summary>
        /// đổi thời gian thành chuỗi thời gian hh:mm:ss
        /// </summary> 
        /// <returns></returns>
        public static string GetTimeString(int second)
        {
            int h = second / 3600;
            int s1 = second % 3600;
            int m = s1 / 60;
            int s = s1 % 60;

            return (h == 0 ? string.Empty : string.Format("{0:00}", h) + ":") + string.Format("{0:00}", m) + ":" + string.Format("{0:00}", s);
        }
    }
}
