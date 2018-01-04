using System;

namespace AglTest.Domain.Exceptions
{
    public class AglDomainException: Exception
    {
        public AglDomainException(string message): base(message)
        {
            
        }

        public AglDomainException()
        {
            
        }
    }
}