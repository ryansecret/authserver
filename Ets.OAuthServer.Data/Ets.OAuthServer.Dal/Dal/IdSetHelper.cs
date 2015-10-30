using Easy.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ets.OAuthServer.Dal
{
    /// <summary>
    /// 设置ID帮助类
    /// </summary>
    static class IdSetHelper
    {
        /// <summary>
        /// 设置实体的ID值
        /// </summary>
        /// <typeparam name="Model">ID的类型</typeparam>
        /// <param name="model">实体实例</param>
        /// <param name="id">ID值</param>
        public static void SetId<Key>(IEntity<Key> model, object id)
        {
            Type type = model.GetType();
            type.GetProperty("Id").SetValue(model, id);
        }
    }
}
