using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MixItUp.GoogleAuthentication
{
    public interface IAuthenticator
    {
        Task<string> Authenticate();
    }
}
