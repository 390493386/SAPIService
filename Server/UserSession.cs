using SiweiSoft.SAPIService.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class UserSession : Session
    {
        public string UserName { get; set; }
    }
}
