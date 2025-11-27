using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Users.Events
{
    public class UserDeletedEvent
    {
        public Guid Id { get; set; }
    }
}