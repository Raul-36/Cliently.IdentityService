using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Users.Options
{
    public class UserQueuesOptions
    {
        public required string CreateUser { get; set; }
        public required string DeleteUser { get; set; }
    }
}