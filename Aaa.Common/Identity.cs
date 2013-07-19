using System;
using System.Security.Principal;
using System.Threading;

namespace Aaa.Common
{
    [Serializable]
    public class Identity : MarshalByRefObject, IIdentity
    {
        public static readonly string Windows = "Windows";
        public static readonly string Forms = "Forms";
        public static readonly string ApiKey = "ApiKey";

        public static Identity Current
        {
            get { return Thread.CurrentPrincipal.Identity as Identity; }
        }

        public static Identity GetUnauthorized()
        {
            return new Identity() { IsAuthenticated = false };
        }

        public Identity()
        {
            this.AuthenticationType = Forms;
            this.IsAuthenticated = true;
        }

        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }

        public string Name { get; set; } // NTID
        public string Username { get; set; }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return this.DisplayName;
        }

        public static string GetNameWithoutDomain(string username)
        {
            return username.Substring(username.IndexOf('\\') + 1);
        }
    }
}
