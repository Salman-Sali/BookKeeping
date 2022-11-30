﻿using FluentValidation;
using MediatR;
using Bk.Shared.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using System.Windows;

namespace Bk.UserInterface.Extentions
{
    public class MediatRValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator> _validators;
        public MediatRValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationFailures = _validators
                .Select(validator => validator.Validate(new ValidationContext<TRequest>(request)))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure != null)
                .ToList();

            if (validationFailures.Any())
            {
                var errorMessage = string.Join(" ", validationFailures);
                MessageBox.Show(errorMessage, "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                throw new Exception();
            }
            return next();
        }
    }
}
