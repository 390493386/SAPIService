using System.Collections.Generic;
using System.IO;

namespace SiweiSoft.SAPIService.Core
{
    public class ActionResult
    {
        /// <summary>
        /// 需要添加到响应包中的头
        /// </summary>
        public List<string> Headers { get; set; }

        /// <summary>
        /// 文件流，文件下载请求时使用
        /// </summary>
        public FileStream FileStream { get; set; }

        /// <summary>
        /// 请求响应数据
        /// </summary>
        public Dictionary<string, object> Result { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ActionResult()
        {
            Headers = new List<string>();
        }
    }
}
