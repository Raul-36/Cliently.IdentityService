using System;

namespace Application.Identity.Exceptions
{
    public class SignInException : Exception
    {
        public SignInException()
            : base("Incorrect email or password.")
        {
        }
    }
}