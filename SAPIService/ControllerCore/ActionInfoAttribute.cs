using System;

namespace SiweiSoft.SAPIService.ControllerCore
{
    /// <summary>
    /// Action特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionInfoAttribute : Attribute
    {
        /// <summary>
        /// Action 别名
        /// </summary>
        public string Alias { get; set; }

        public ActionInfoAttribute(string alias)
        {
            Alias = alias;
        }
    }
}
