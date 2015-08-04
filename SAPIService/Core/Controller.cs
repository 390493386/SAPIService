using System;
using System.Collections.Generic;

namespace SiweiSoft.SAPIService.Core
{
    public abstract class Controller: ICloneable
    {
        /// <summary>
        /// Paramenters
        /// </summary>
        public Dictionary<string, object> Parameters
        {
            protected get;
            set;
        }

        /// <summary>
        /// Get current session
        /// </summary>
        /// <typeparam name="TSession"></typeparam>
        /// <returns></returns>
        public TSession GetSession<TSession>() where TSession : Session
        {
            return Parameters.ContainsKey("Session") ? (TSession)Parameters["Session"] : default(TSession);
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
