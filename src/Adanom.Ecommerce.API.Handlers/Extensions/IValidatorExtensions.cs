using HotChocolate;
using HotChocolate.Execution;

namespace FluentValidation
{
    public static class IValidatorExtensions
    {
        public static async Task ValidateAndThrowAsQueryExceptionAsync<T>(this IValidator<T> validator, T instance)
        {
            var validationResults = await validator.ValidateAsync(instance);

            if (!validationResults.IsValid)
            {
                var errors = validationResults.Errors
                    .Select(e =>
                    {
                        return ErrorBuilder.New()
                            .SetCode(e.ErrorCode)
                            .SetMessage(e.ErrorMessage)
                            .SetExtension("severity", e.Severity)
                            .SetExtension("propertyName", e.PropertyName)
                            .Build();
                    });

                throw new QueryException(errors);
            }
        }
    }
}
