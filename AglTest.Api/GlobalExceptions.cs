using System;
using System.Threading.Tasks;
using AglTest.Domain;
using AglTest.Infrastructure.RestClient;
using Enbiso.NLib.Cqrs;
using Enbiso.NLib.DependencyInjection;
using Enbiso.NLib.GlobalExceptions;

namespace AglTest.Api
{
    [SingletonService]
    public class ExceptionHandler: GlobalExceptionHandler<Exception>
    {
        protected override Task<GlobalExceptionResponse> Handle(Exception ex)
        {
            var res = new GlobalExceptionResponse(new 
            {
                ex.Message
            }, 500);
            return Task.FromResult(res);
        }
    }

    [SingletonService]
    public class ValidateExceptionHandler: GlobalExceptionHandler<CommandValidationException>
    {
        protected override Task<GlobalExceptionResponse> Handle(CommandValidationException ex)
        {
            var res = new GlobalExceptionResponse(new 
            {
                ex.Message,
                ex.Errors
            }, 400);
            return Task.FromResult(res);
        }
    }
    
    [SingletonService]
    public class NotFoundExceptionHandler : GlobalExceptionHandler<NotFoundException>
    {
        protected override Task<GlobalExceptionResponse> Handle(NotFoundException ex)
        {
            var res = new GlobalExceptionResponse(new
            {
                ex.Message
            }, 404);
            return Task.FromResult(res);
        }
    }
    
    [SingletonService]
    public class RestRequestFailedExceptionHandler : GlobalExceptionHandler<RestRequestFailedException>
    {
        protected override Task<GlobalExceptionResponse> Handle(RestRequestFailedException ex)
        {
            var res = new GlobalExceptionResponse(new
            {
                ex.Message
            }, 400);
            return Task.FromResult(res);
        }
    }
}