using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ets.OAuthServer.Utility
{
    public class OAuthContants
    {
        public static class Paths
        {
           
            /// <summary>
            /// AuthorizationCodeGrant project should be running on this URL.
            /// </summary>
            public const string AuthorizeCodeCallBackPath = "http://localhost:38500/";

            public const string AuthorizePath = "/OAuth/Authorize";
            public const string TokenPath = "/OAuth/Token";

          
        }

        public static class Clients
        {
            //todo:需要从库中读取
            public readonly static Client Client1 = new Client
            {
                Id = "123456",
                Secret = "abcdef",
                RedirectUrl = Paths.AuthorizeCodeCallBackPath
            };
        }

        public class Client
        {
            public string Id { get; set; }
            public string Secret { get; set; }
            public string RedirectUrl { get; set; }
        }
    }
}