namespace Adanom.Ecommerce.API.Validation.Validators
{
    public sealed class CreateReturnRequestValidator : AbstractValidator<CreateReturnRequest>
    {
        private readonly IMediator _mediator;

        public CreateReturnRequestValidator(IMediator mediator)
        {
            _mediator = mediator;

            RuleFor(e => e.Identity)
                .NotNull()
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Kullanıcı bilgilerine erişilemiyor.");

            RuleFor(e => e.OrderId)
                .GreaterThan(0)
                    .WithErrorCode(ValidationErrorCodesEnum.REQUIRED)
                    .WithMessage("Sipariş bulunamadı.");

            RuleFor(e => e)
                .CustomAsync(ValidateDoesReturnRequestCanBeCreateAsync);
        }

        #region Private Methods

        #region ValidateDoesReturnRequestCanBeCreateAsync

        private async Task ValidateDoesReturnRequestCanBeCreateAsync(
            CreateReturnRequest value,
            ValidationContext<CreateReturnRequest> context,
            CancellationToken cancellationToken)
        {
            var order = await _mediator.Send(new GetOrder(value.OrderId));

            if (order == null)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateReturnRequest.OrderId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş bulunamadı."
                });

                return;
            }

            if (order.DeliveredAtUtc == null)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateReturnRequest.OrderId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş teslim edilmediğinden dolayı iade oluşturulamaz."
                });

                return;
            }

            var orderDeliveredAtUtc = order.DeliveredAtUtc.Value;
            var todayUtc = DateTime.UtcNow;

            var dayDifferences = todayUtc - orderDeliveredAtUtc;

            if (dayDifferences.TotalDays > 14)
            {
                context.AddFailure(new ValidationFailure(nameof(CreateReturnRequest.OrderId), null)
                {
                    ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                    ErrorMessage = "Sipariş teslim tarihinden itibaren 14 gün içinde iade oluşturabilirsiniz."
                });

                return;
            }

            var returnRequest = await _mediator.Send(new GetReturnRequestByOrderId(order.Id));
            List<ReturnRequestItemResponse>? returnRequestItems = null;

            if (returnRequest != null)
            {
                var returnRequestItemsAsEnumerable = await _mediator.Send(new GetReturnRequestItems(
                new GetReturnRequestItemsFilter()
                {
                    ReturnRequestId = returnRequest.Id
                }));

                if (returnRequestItemsAsEnumerable.Any())
                {
                    returnRequestItems = returnRequestItemsAsEnumerable.ToList();
                }
            }

            foreach (var createReturnRequestItemRequest in value.CreateReturnRequest_ItemRequests)
            {
                if (createReturnRequestItemRequest.Amount <= 0)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateReturnRequest.OrderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                        ErrorMessage = "İade için 1 adet veya daha fazla talep oluşturmanız gerekmektedir."
                    });

                    return;
                }

                var orderItem = await _mediator.Send(new GetOrderItem(createReturnRequestItemRequest.OrderItemId));

                if (orderItem == null)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateReturnRequest.OrderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                        ErrorMessage = "Sipariş ürünü bulunamadı."
                    });

                    return;
                }

                var existedReturnRequestItem = returnRequestItems?
                    .Where(e => e.OrderItemId == orderItem.Id)
                    .SingleOrDefault();

                if (existedReturnRequestItem != null)
                {
                    if (createReturnRequestItemRequest.Amount > orderItem.Amount - existedReturnRequestItem.Amount)
                    {
                        context.AddFailure(new ValidationFailure(nameof(CreateReturnRequest.OrderId), null)
                        {
                            ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                            ErrorMessage = "İade talebi daha önce oluşturduğunuz iade talebi adetiyle sipariş adeti farkında fazla olamaz."
                        });

                        return;
                    }
                }

                if (createReturnRequestItemRequest.Amount > orderItem.Amount)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateReturnRequest.OrderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                        ErrorMessage = "Sipariş ürün adetinden fazla adette iade talebi oluşturamazsınız."
                    });

                    return;
                }

                if (createReturnRequestItemRequest.Description == null || string.IsNullOrEmpty(createReturnRequestItemRequest.Description))
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateReturnRequest.OrderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                        ErrorMessage = "İade ürünü için açıklama eklemeniz gerekmektedir."
                    });

                    return;
                }

                if (createReturnRequestItemRequest.Description.Length > 100)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateReturnRequest.OrderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                        ErrorMessage = "İade ürünü için açıklama en fazla 100 karakterden oluşmalıdır."
                    });

                    return;
                }
            }
        }

        #endregion

        #endregion
    }
}
