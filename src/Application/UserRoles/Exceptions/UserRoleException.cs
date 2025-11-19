using System;

namespace Application.UserRoles.Exceptions
{
    public class UserRoleException : Exception
    {
        public UserRoleException(string message) : base(message)
        {
        }
    }
}