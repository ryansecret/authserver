using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Model
{
    public class Application
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int Id
        {
            get;
            set;
        }
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
    }
}
