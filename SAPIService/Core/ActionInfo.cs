using System.Reflection;

namespace SiweiSoft.SAPIService.Core
{
    public class ActionInfo
    {
        /// <summary>
        /// Action
        /// </summary>
        public MethodInfo Action { get; set; }

        /// <summary>
        /// Action attribute
        /// </summary>
        public bool NeedAuthorize { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ActionInfo()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action"></param>
        /// <param name="needAuthorize"></param>
        public ActionInfo(MethodInfo action, bool needAuthorize)
        {
            Action = action;
            NeedAuthorize = needAuthorize;
        }
    }
}
