﻿using AspNetCore.Example.Application.Mapping.Response;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore.Example.Application.Behaviors
{
    public class FailFastRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse> where TResponse : Response
    {
        private readonly IEnumerable<IValidator> _validators;
        private readonly ILogger<Response> _logger;

        public FailFastRequestBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<Response> logger)
        {
            _logger = logger;
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            return failures.Any()
                ? Errors(failures)
                : next();
        }

        private Task<TResponse> Errors(IEnumerable<ValidationFailure> failures)
        {
            var response = new Response();

            foreach (var failure in failures)
            {
                response.AddError(failure.ErrorMessage);

                _logger.LogError($"Error ao validar dados: Propriedade: {failure.PropertyName} -- Erro: {failure.ErrorMessage} -- Type: { failure.ErrorCode} ");

            }

            return Task.FromResult(response as TResponse);
        }
    }
}
