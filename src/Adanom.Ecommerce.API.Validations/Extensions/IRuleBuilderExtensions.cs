using Adanom.Ecommerce.API;

namespace FluentValidation
{
    public static class IRuleBuilderExtensions
    {
        //
        // Summary:
        //     Specifies a custom error code to use if validation fails.
        //
        // Parameters:
        //   rule:
        //     The current rule
        //
        //   errorCode:
        //     The error code to use
        public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, ValidationErrorCodesEnum errorCode)
        {
            DefaultValidatorOptions.Configurable(rule).Current.ErrorCode = errorCode.ToString();
            return rule;
        }
    }
}
