using Easy.Domain.Base;
using Easy.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Model
{
    public class Application : EntityBase<Int32>
    {
        /// <summary>
        /// app名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// appKey
        /// </summary>
        public string AppKey
        {
            get;
            set;
        }
        /// <summary>
        /// APP密钥
        /// </summary>
        public string AppSecret
        {
            get;
            set;
        }
        /// <summary>
        /// 回调地址
        /// </summary>
        public string CallbackUrl
        {
            get;
            set;
        }

        protected override BrokenRuleMessage GetBrokenRuleMessages()
        {
            return new ApplicationBrokenRuleMessage();
        }

        public override bool Validate()
        {
            return new ApplicationValidation().IsSatisfy(this);
        }
    }

    class ApplicationValidation : EntityValidation<Application>
    {
        public ApplicationValidation()
        {
            this.IsNullOrWhiteSpace(m => m.Name, ApplicationBrokenRuleMessage.NameIsEmpty);
            this.IsNullOrWhiteSpace(m => m.AppKey, ApplicationBrokenRuleMessage.AppKeyIsEmpty);
            this.IsNullOrWhiteSpace(m => m.AppKey, ApplicationBrokenRuleMessage.AppSecretIsEmpty);
            this.IsNullOrWhiteSpace(m => m.CallbackUrl, ApplicationBrokenRuleMessage.CallBackUrlIsEmpty);
            this.AddRule((Application application) =>
            {
                string url = application.CallbackUrl.ToUpper();

                if (url.StartsWith("HTTP://") || url.StartsWith("HTTPS://"))
                {
                    return true;
                }
                return false;
            }, ApplicationBrokenRuleMessage.CallBackUrlError);
        }
    }

    class ApplicationBrokenRuleMessage : BrokenRuleMessage
    {
        public const string NameIsEmpty = "name is empty";
        public const string AppKeyIsEmpty = " app key is empty";
        public const string AppSecretIsEmpty = "app secret is empty";
        public const string CallBackUrlIsEmpty = "callback url is empty";
        public const string CallBackUrlError = "callback url error";

        protected override void PopulateMessage()
        {
            this.Messages.Add(NameIsEmpty, "名称不能为空");
            this.Messages.Add(AppKeyIsEmpty, "APPKey不能为空");
            this.Messages.Add(AppSecretIsEmpty, "AppSecret不能为空");
            this.Messages.Add(CallBackUrlIsEmpty, "回调地地不能为空");
            this.Messages.Add(CallBackUrlIsEmpty,"回调地址必须以http或https开头");
        }
    }

}
