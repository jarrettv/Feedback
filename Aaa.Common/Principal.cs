using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;

namespace Aaa.Common
{
    [Serializable]
    public class Principal : MarshalByRefObject, IPrincipal
    {
        public static Principal Current
        {
            get { return Thread.CurrentPrincipal as Principal; }
        }

        public static Principal GetUnauthorized()
        {
            return new Principal(Aaa.Common.Identity.GetUnauthorized(), new string[0]);
        }

        public Principal(Identity identity, string[] roles)
        {
            this.Identity = identity;
            this.Roles = roles;
        }

        protected string[] Roles { get; set; }

        public IIdentity Identity { get; protected set; }

        public bool IsInRole(string role)
        {
            return this.Roles.Any(x => x == role);
        }

        public bool IsAuth(string role)
        {
            // admins are authorized to do anything
            return this.IsInRole("Admin") || this.IsInRole(role);
        }
    }
}
