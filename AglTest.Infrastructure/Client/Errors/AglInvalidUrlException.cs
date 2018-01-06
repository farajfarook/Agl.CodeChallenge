using AglTest.Domain.Exceptions;

namespace AglTest.Infrastructure.Client.Errors
{
    public class AglInvalidUrlException: AglDomainException
    {
        public AglInvalidUrlException(string request) : base(request)
        {
            
        }
    }
}