using SiweiSoft.SAPIService.Helper;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

namespace SiweiSoft.SAPIService.Core
{
    public class ActionResult
    {
        /// <summary>
        /// 需要添加到响应包中的头，构造方法中已初始化变量，无需再初始化
        /// </summary>
        public List<string> Headers { get; set; }

        /// <summary>
        /// 文件流，文件下载请求时使用，构造方法中没有初始化变量，如使用需初始化
        /// </summary>
        public FileStream FileStream { get; set; }

        /// <summary>
        /// 请求响应数据，构造方法中已初始化变量，无需再初始化
        /// </summary>
        public Dictionary<string, object> Result { get; set; }

        /// <summary>
        /// 响应结果字符串编码方式，默认UTF-8
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public ActionResult()
        {
            Headers = new List<string>();
            Result = new Dictionary<string, object>();
            Encoding = Encoding.UTF8;
        }

        /// <summary>
        /// 构造方法，带参数
        /// </summary>
        /// <param name="headers">请求头</param>
        /// <param name="result">请求数据部分</param>
        /// <param name="encoding">数据部分编码方式</param>
        /// <param name="fileStream">文件流，文件下载时用到</param>
        public ActionResult(List<string> headers, Dictionary<string, object> result,
            Encoding encoding, FileStream fileStream)
        {
            Headers = headers;
            Result = result;
            Encoding = encoding;
            FileStream = fileStream;
        }

        /// <summary>
        /// 获取返回结果，字符串形式
        /// </summary>
        /// <returns></returns>
        public virtual string GetResultString()
        {
            return new JavaScriptSerializer().Serialize(this.Result);
        }

        /// <summary>
        /// 获取返回结果，字节流形式
        /// </summary>
        /// <returns></returns>
        public virtual byte[] GetResultBytes()
        {
            byte[] result = null;
            if (Encoding == null)
                Log.Comment(CommentType.Error, "响应结果编码方式无效！");
            else
                result = Encoding.GetBytes(new JavaScriptSerializer().Serialize(this.Result));
            return result;
        }
    }

    /// <summary>
    /// 未授权请求
    /// </summary>
    public class ActionNotAuthorized : ActionResult
    {
        public ActionNotAuthorized()
        {
            Result = new Dictionary<string, object>();
            Result.Add("code", -1);
            Result.Add("message", "Request not authorized.");
        }
    }
}
