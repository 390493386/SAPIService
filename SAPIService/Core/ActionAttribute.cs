using System;

namespace SiweiSoft.SAPIService.Core
{
    /// <summary>
    /// Action attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionAttribute : Attribute
    {
        /// <summary>
        /// Action alias
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Need authorization
        /// </summary>
        public bool NeedAuthorize { get; set; }

        /// <summary>
        /// Construcor
        /// </summary>
        /// <param name="alias">Alias</param>
        /// <param name="needAuthorize">Need authorize</param>
        public ActionAttribute(string alias, bool needAuthorize = true)
        {
            Alias = alias;
            NeedAuthorize = needAuthorize;
        }
    }
}
