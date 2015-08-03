using System;

namespace SiweiSoft.SAPIService.Core
{
    public class Session
    {
        /// <summary>
        /// Session expire date
        /// </summary>
        private DateTime ExpireDate;

        /// <summary>
        /// 是否经过认证
        /// </summary>
        public bool IsAuthorized
        {
            set;
            get;
        }

        /// <summary>
        /// Reset session expire date
        /// </summary>
        /// <param name="hours"></param>
        public void ResetExpireDate(int hours)
        {
            ExpireDate = DateTime.Now.AddHours(hours);
        }

        /// <summary>
        /// Is session expired
        /// </summary>
        /// <returns></returns>
        public bool IsSessionExpired()
        {
            return DateTime.Now > ExpireDate;
        }
    }
}
