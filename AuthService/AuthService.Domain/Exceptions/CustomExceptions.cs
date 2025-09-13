using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message) { }
        public UserNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class DuplicateUserException : Exception
    {
        public DuplicateUserException(string message) : base(message) { }
        public DuplicateUserException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string message) : base(message) { }
        public InvalidCredentialsException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class TokenExpiredException : Exception
    {
        public TokenExpiredException(string message) : base(message) { }
        public TokenExpiredException(string message, Exception innerException) : base(message, innerException) { }
    }
}