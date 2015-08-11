using SiweiSoft.SAPIService.Helper;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SiweiSoft.SAPIService.Core
{
    internal class ControllerReflectionInfo
    {
        /// <summary>
        /// Controller instance
        /// </summary>
        public Object ControllerInstance { get; set; }

        /// <summary>
        /// Controller中方法和别名对应
        /// </summary>
        private Dictionary<string, ActionInfo> Actions;

        /// <summary>
        /// 初始化Action别名和方法的对应表
        /// </summary>
        /// <param name="controllerType"></param>
        public ControllerReflectionInfo(Type controllerType)
        {
            if (controllerType != null)
            {
                ControllerInstance = controllerType.Assembly.CreateInstance(controllerType.FullName);
                Actions = new Dictionary<string, ActionInfo>();
                MethodInfo[] actions = controllerType.GetMethods();
                foreach (MethodInfo action in actions)
                {
                    ActionAttribute actionAttribute = action.GetCustomAttribute<ActionAttribute>();
                    if (actionAttribute != null)
                    {
                        if (!Actions.ContainsKey(actionAttribute.Alias))
                            Actions.Add(actionAttribute.Alias, new ActionInfo(action, actionAttribute.NeedAuthorize));
                        else
                            Log.LogCommentC(CommentType.Warn, "There exist action alias with ths same name.");
                    }
                }
            }
        }

        /// <summary>
        /// 根据别名获取对应的方法
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public ActionInfo GetMethodInfoByAlias(string alias)
        {
            ActionInfo actionInfo = null;

            if (Actions != null)
            {
                actionInfo = Actions.ContainsKey(alias) ? Actions[alias] : null;
            }

            return actionInfo;
        }
    }
}
