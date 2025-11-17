using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Identity.Commands;

namespace Presentation.Options
{
    public class FirstUsersOptions
    {
        public required IEnumerable<RegisterCommand> Users { get; set; }
    
    }
}