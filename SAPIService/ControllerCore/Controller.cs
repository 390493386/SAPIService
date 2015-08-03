using SiweiSoft.SAPIService.Core;
using System.Collections.Generic;

namespace SiweiSoft.SAPIService.ControllerCore
{
    public abstract class Controller<TSession> where TSession : Session
    {
        /// <summary>
        /// 服务层传递给Controller层的参数
        /// </summary>
        public Dictionary<string, object> Parameters
        {
            protected get;
            set;
        }

        public TSession Session
        {
            get
            {
                return Parameters.ContainsKey("Session") ? (TSession)Parameters["Session"] : null;
            }
        }
    }
}
