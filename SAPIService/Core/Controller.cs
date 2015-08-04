using System.Collections.Generic;

namespace SiweiSoft.SAPIService.Core
{
    public abstract class Controller
    {
        /// <summary>
        /// 服务层传递给Controller层的参数
        /// </summary>
        public Dictionary<string, object> Parameters
        {
            protected get;
            set;
        }

        public Session Session
        {
            get
            {
                return Parameters.ContainsKey("Session") ? (Session)Parameters["Session"] : null;
            }
        }
    }
}
