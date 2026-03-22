using FluentValidation;
using MediatR;
using SolTestBackend.Core.Results;
using System.Linq.Expressions;
using System.Reflection;

namespace SolTestBackend.Application.Shared
{
    public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    var error = Error.Validation(
                        "Validation.Error",
                        string.Join(" | ", failures.Select(f => f.ErrorMessage)));

                    return CreateFailureResult(error);
                }
            }

            try
            {
                return await next();
            }
            catch (DomainException ex)
            {
                return CreateFailureResult(ex.Error);
            }
            catch (Exception ex)
            {
                var genericError = Error.Failure(
                    "Server.InternalError",
                    "Ocurrió un error inesperado en el servidor.");

                return CreateFailureResult(genericError);
            }
        }

        private static TResponse CreateFailureResult(Error error)
        {
            // Si TResponse es Result<TValue>
            if (typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                Type valueType = typeof(TResponse).GetGenericArguments()[0];

                // Buscamos el método Failure<T>(Error error)
                var method = typeof(Result)
                    .GetMethods()
                    .First(m => m.Name == nameof(Result.Failure) && m.IsGenericMethod)
                    .MakeGenericMethod(valueType);

                return (TResponse)method.Invoke(null, new object[] { error })!;
            }

            // Si TResponse es simplemente Result
            return (TResponse)(object)Result.Failure(error);
        }
    }
}
