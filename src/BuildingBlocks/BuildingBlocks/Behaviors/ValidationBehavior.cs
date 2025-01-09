﻿using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors {
    /// <summary>
    /// TRequest ve TResponse alır ,validate edilen TRequest ICommand dan kalıtılmalıdır ve ilgili validate edilecek sınıfın tüm validasyonları ele alınır
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="validators"></param>
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
        {

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
            
            var context = new ValidationContext<TRequest>(request);

            var validationResult = await Task.WhenAll(validators.Select(v=>v.ValidateAsync(context,cancellationToken)));

            var failures = validationResult
                .Where(v => v.Errors.Any())
                .SelectMany(v => v.Errors)
                .ToList();

            if (failures.Any()) {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
