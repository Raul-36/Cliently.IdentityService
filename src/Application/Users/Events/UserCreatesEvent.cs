using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Users.Events
{
    public class UserCreatedEvent
    {
        public Guid Id { get; set; }
    }
}