using Adanom.Ecommerce.API.Commands;
using FluentValidation;
using MediatR;

namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class TestValidator : AbstractValidator<Test>
    {
        private readonly IMediator _mediator;

        public TestValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Id).GreaterThan(0);
        }

        #region Private Methods

        #endregion
    }
}
