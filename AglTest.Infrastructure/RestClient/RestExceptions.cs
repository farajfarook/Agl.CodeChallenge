using System;

namespace AglTest.Infrastructure.RestClient
{
    public class RestInvalidUrlException: Exception
    {
        public RestInvalidUrlException(string msg) : base(msg)
        {
            
        }
    }

    public class RestRequestFailedException : Exception
    {
        public RestRequestFailedException(string msg) : base(msg)
        {
        }
    }
}