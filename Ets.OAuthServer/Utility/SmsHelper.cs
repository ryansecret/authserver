using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Ets.OAuthServer.Bll.EtsSmsService;

namespace Ets.OAuthServer.Utility
{
   public class SmsHelper
    {
        public string ErrorMessage { get; set; }

        public SmsHelper()
        {
            ErrorMessage = "";
        }

        /// <summary>
        /// 发送短信接口(20140218日添加)
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="content">内容</param>
        /// <param name="smsSource">调用接口来源</param>
        /// <param name="supplierId">餐厅Id</param>
        /// <param name="isVoiceSms">是否发送语言短信</param>
        /// <returns></returns>
        public string SendSms(string mobile, string content, string smsSource, int? supplierId, bool isVoiceSms)
        {
            var sms = new SmsSoapClient();
            return sms.SendSmsSaveLog(mobile, content, smsSource, supplierId, isVoiceSms);
            ////return sms.SendSmsSaveLogB2B(mobile, content, smsSource, supplierId, 0, "MW");
        }
        /// <summary>
        /// 发送短信接口(20140219日添加)
        /// 发送短信，如果失败，ErrorMessage会附带错误信息
        /// 自动检测发送频率，如果间隔过短会发送失败
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="message">短信内容</param>
        /// <returns></returns>
        public bool Send(string mobile, string message, string smsSource, int? supplierId, bool isVoiceSms)
        {
            var session = System.Web.HttpContext.Current.Session;
            var lastTime = session["smshelper_send_time"] as DateTime?; //上次支付时间
            var randT = new Random().Next(3, 6);
            if (lastTime.HasValue && lastTime.Value.AddMinutes(randT).CompareTo(DateTime.Now) < 0)
            {
                this.ErrorMessage = "操作过于频繁，请稍后重试";
                return false;
            }
            try
            {
                var sms = new SmsSoapClient();
                sms.SendSmsSaveLog(mobile, message, smsSource, supplierId, isVoiceSms);
                return true;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 无频率限制的发送短信
        /// </summary>
        /// <param name="mobiles">手机号使用逗号分隔</param>
        /// <param name="message">短信内容</param>
        public string SendSms(string mobile, string message)
        {
            var sms = new SmsSoapClient();
            return sms.SendSms(mobile, message);

        }

        public string SendSmsSaveLog(string mobile, string message, string smsSource, int? SupplierID)
        {
            var sms = new SmsSoapClient();
            return sms.SendSmsSaveLog(mobile, message, smsSource, SupplierID, false);

        }

        public string SendSmsSaveLog(string mobile, string message, string smsSource, int? SupplierID, bool isVoiceSms)
        {
            var sms = new SmsSoapClient();
            return sms.SendSmsSaveLog(mobile, message, smsSource, SupplierID, isVoiceSms);

        }
        /// <summary>
        /// 无频率限制的语音短信
        /// </summary>
        /// <param name="mobiles">手机号使用逗号分隔</param>
        /// <param name="message">语音内容</param>
        public string SendVoiceSms(string mobile, string message)
        {
            var sms = new SmsSoapClient();
            return sms.SendSmsSaveLog(mobile, message, "ETD", null, true);

        }
        //public string SendSmsSaveLogSpeed(string mobile, string message)
        //{
        //    var sms = new SmsSoapClient();
        //    string MessgePlatform = ConfigurationManager.AppSettings["MessgePlatform"];
        //    string rtnstr = sms.SendSmsSaveLogB2B(mobile, message, "ETD", null, 1, MessgePlatform);
        //    SmsReturn trnmodel = JsonHelper.DeserializeJsonToObject<SmsReturn>(rtnstr);
        //    return trnmodel.Desc;//sms.SendSmsSaveLogB2B(mobile, message, "ETD", null, 1, MessgePlatform);
        //    //return sms.SpeedSend(mobile, message, "ETD", null, "JWK");
        //   // return sms.SendSmsSaveLog(mobile, message, "ETD", null, true);

        //}
        public string SendSmsSaveLogSpeed(string mobile, string message, string smsSource, int? SupplierID)
        {
            var sms = new SmsSoapClient();
            string MessgePlatform = ConfigurationManager.AppSettings["MessgePlatform"];
            string rtnstr = sms.SendSmsSaveLogB2B(mobile, message, smsSource, 0, 1, MessgePlatform);
            //SmsReturn trnmodel = JsonHelper.DeserializeJsonToObject<SmsReturn>(rtnstr);
            return rtnstr;
        }

        public string SendVoiceSmsLogSpeed(string mobile, string message)
        {
            var sms = new SmsSoapClient();
            return sms.SendSmsSaveLogNew(mobile, message, "ETD", null, true, 1);//sms.SendSmsSaveLog(mobile, message, "ETD", null, true);

        }



        /// <summary>
        /// 发送短信，如果失败，ErrorMessage会附带错误信息
        /// 自动检测发送频率，如果间隔过短会发送失败
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="message">短信内容</param>
        /// <returns></returns>
        public bool Send(string mobile, string message)
        {
            var session = System.Web.HttpContext.Current.Session;
            var lastTime = session["smshelper_send_time"] as DateTime?; //上次支付时间
            var randT = new Random().Next(3, 6);
            if (lastTime.HasValue && lastTime.Value.AddMinutes(randT).CompareTo(DateTime.Now) < 0)
            {
                this.ErrorMessage = "操作过于频繁，请稍后重试";
                return false;
            }
            try
            {
                var sms = new SmsSoapClient();
                sms.SendSms(mobile, message);
                return true;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 随机选择通道发送,用于营销使用(不使用主通道)
        /// </summary>
        public string SendSmsRandom(string mobile, string message)
        {
            var sms = new SmsSoapClient();
            return sms.SendSmsBase(mobile, message, SmsSendType.营销渠道);
        }

        /// <summary>
        /// 生成随机指定长度的数字
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string RandNum(int length)
        {
            string code = "";
            for (int i = 0; i < length; i++)
            {
                var next = new Random(i * ((int)DateTime.Now.Ticks)).Next(0, 10);
                code += next;
            }
            return code;
        }

        /// <summary>
        /// 生成随机指定长度的数字
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string VoiceRandNum(int length)
        {
            string code = "";
            for (int i = 0; i < length; i++)
            {
                var next = new Random(i * ((int)DateTime.Now.Ticks)).Next(0, 10);
                code += next + ",";
            }
            code = code.TrimEnd(',');
            return code;
        }
    }
    public class SmsReturn
    {
        public string Code { get; set; }
        public string Desc { get; set; }
        public string Platform { get; set; }
        public string BatchNumber { get; set; }
    }
}