using System;

namespace Application.Roles.Exceptions
{
    public class RoleNotFoundException : Exception
    {
        public RoleNotFoundException(string name)
            : base($"Role with name '{name}' was not found.")
        {
        }
        public RoleNotFoundException(Guid id)
            : base($"Role with id '{id}' was not found.")
        {
        }
    }
}