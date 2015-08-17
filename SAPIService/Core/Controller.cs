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
        /// Session
        /// </summary>
        public Session Session
        {
            protected get;
            set;
        }

        public virtual Controller Clone()
        {
            return (Controller)this.MemberwiseClone();
        }
    }
}
