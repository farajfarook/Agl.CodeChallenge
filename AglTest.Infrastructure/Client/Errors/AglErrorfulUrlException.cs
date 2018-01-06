using AglTest.Domain.Exceptions;

namespace AglTest.Infrastructure.Client.Errors
{
    public class AglErrorfulUrlException : AglDomainException
    {
        public AglErrorfulUrlException(string request) : base(request)
        {
        }
    }
}