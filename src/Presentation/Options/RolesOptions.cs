using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Options
{
    public class RolesOptions
    {
        public required IEnumerable<string> RolesNames { get; set; }
    }
}