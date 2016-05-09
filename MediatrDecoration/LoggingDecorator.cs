using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using MediatR;

namespace MediatrDecoration
{
    public class LoggingDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>

    {
        private readonly IRequestHandler<TRequest, TResponse> _innerHandler;

        public LoggingDecorator(IRequestHandler<TRequest, TResponse> innerHandler )
        {
            _innerHandler = innerHandler;
        }

        public TResponse Handle(TRequest message)
        {
            Trace.Write("Entering");

            var response =  _innerHandler.Handle(message);


            Trace.Write("Exiting");


            return response;

        }
    }
}