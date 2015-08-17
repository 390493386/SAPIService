using System.Collections.Generic;

namespace SiweiSoft.SAPIService.Core
{
    public abstract class Controller
    {
        /// <summary>
        /// Server configurations
        /// </summary>
        public Dictionary<string, object> ServerConfigs
        {
            protected get;
            set;
        }

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

        public virtual Controller Clone()
        {
            return (Controller)this.MemberwiseClone();
        }
    }
}
