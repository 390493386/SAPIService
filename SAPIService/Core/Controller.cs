using System;
using System.Collections.Generic;

namespace SiweiSoft.SAPIService.Core
{
    public abstract class Controller: ICloneable
    {
        /// <summary>
        /// 服务层传递给Controller层的参数
        /// </summary>
        public Dictionary<string, object> Parameters
        {
            protected get;
            set;
        }

        /// <summary>
        /// Current session
        /// </summary>
        public Session Session
        {
            get
            {
                return Parameters.ContainsKey("Session") ? (Session)Parameters["Session"] : null;
            }
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public Controller Clone()
        {
            return (Controller)this.MemberwiseClone();
        }
    }
}
