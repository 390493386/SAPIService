using System;

namespace SiweiSoft.SAPIService.Core
{
    /// <summary>
    /// For user session
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Session expire date
        /// </summary>
        private DateTime ExpireDate;

        /// <summary>
        /// Is authorized
        /// </summary>
        public bool IsAuthorized
        {
            set;
            get;
        }

        /// <summary>
        /// Reset session expire date
        /// </summary>
        /// <param name="seconds"></param>
        public void ResetExpireDate(int seconds)
        {
            ExpireDate = DateTime.Now.AddSeconds(seconds);
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
