using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaa.Common
{
    public interface IAuthService
    {
        Principal GetUser(string username);
    }
}
