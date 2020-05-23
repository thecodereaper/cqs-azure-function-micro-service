using System;
using System.Linq;
using System.Threading.Tasks;
using Demo.Core.Services;
using FluentValidation;
using FluentValidation.Results;
using MediatR.DDD.Exceptions;

namespace Demo.Infrastructure.Services
{
    internal sealed class ValidatorService : IValidatorService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidatorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ValidateAsync<T>(T obj)
        {
            if (obj == null)
                throw new BadRequestException();

            IValidator validator = GetValidator<T>();

            if (validator == null)
                throw new NotSupportedException();

            ValidationResult validationResult = await validator.ValidateAsync(obj);

            if (validationResult.IsValid)
                return;

            string errorMessage = string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            throw new BadRequestException(errorMessage);
        }

        private IValidator GetValidator<T>()
        {
            Type validatorType = typeof(IValidator<>).MakeGenericType(typeof(T));
            IValidator validator = (IValidator) _serviceProvider.GetService(validatorType);

            return validator;
        }
    }
}