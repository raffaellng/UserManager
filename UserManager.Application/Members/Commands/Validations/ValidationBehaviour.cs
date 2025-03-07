﻿using FluentValidation;
using MediatR;

namespace UserManager.Application.Members.Commands.Validations
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest: IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public async Task<TResponse> Handle(TRequest request, 
                                            RequestHandlerDelegate<TResponse> next, 
                                            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors.Where(f => f.FormattedMessagePlaceholderValues != null).ToList());

                if (failures.Count() != 0)
                    throw new FluentValidation.ValidationException(failures);
            }
            return await next();
        }
    }
}
