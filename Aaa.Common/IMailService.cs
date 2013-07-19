using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaa.Common
{
    public interface IMailService
    {
        void Send(MailAddress to, MailAddress cc, string subject, string message);
        void Send(IEnumerable<MailAddress> recipients, IEnumerable<MailAddress> ccers, string subject, string message);
    }

    public class MailAddress
    {
        public string Address { get; set; }
        public string DisplayName { get; set; }
    }
}
