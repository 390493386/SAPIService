using System;
using System.Collections.Generic;
using System.Reflection;

namespace SiweiSoft.SAPIService.Core
{
    public class ControllerReflectionInfo
    {
        /// <summary>
        /// Controller对应的Type
        /// </summary>
        public Type ControllerType { get; set; }

        /// <summary>
        /// Controller中方法和别名对应
        /// </summary>
        private Dictionary<string, MethodInfo> Actions;

        /// <summary>
        /// 初始化Action别名和方法的对应表
        /// </summary>
        /// <param name="controllerType"></param>
        public ControllerReflectionInfo(Type controllerType)
        {
            ControllerType = controllerType;
            if (ControllerType != null)
            {
                Actions = new Dictionary<string, MethodInfo>();

                MethodInfo[] actions = ControllerType.GetMethods();
                foreach (MethodInfo action in actions)
                {
                    ActionInfoAttribute actionAttribute = action.GetCustomAttribute<ActionInfoAttribute>();
                    if (actionAttribute != null && !String.IsNullOrEmpty(actionAttribute.Alias) && !Actions.ContainsKey(actionAttribute.Alias))
                        Actions.Add(actionAttribute.Alias, action);
                }
            }
        }

        /// <summary>
        /// 根据别名获取对应的方法
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public MethodInfo GetMethodInfoByAlias(string alias)
        {
            MethodInfo action = null;

            if (Actions != null)
            {
                action = Actions.ContainsKey(alias) ? Actions[alias] : null;
            }

            return action;
        }
    }
}
