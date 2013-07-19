using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Aaa.Common
{
    public static class XElementExtensions
    {
        public static string InnerXml(this XElement el)
        {
            return el.Nodes()
                .Aggregate(string.Empty, (xml, node) => xml += node.ToString());
        }
    }
}
