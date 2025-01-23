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

            var returnRequests = await _mediator.Send(new GetReturnRequestsByOrderId(order.Id));

            if (!returnRequests.Any())
            {
                return;
            }

            // Get all active return requests (not cancelled)
            var activeReturnRequests = returnRequests.Where(r => r.ReturnRequestStatusType.Key != ReturnRequestStatusType.CANCEL).ToList();

            // Get all return request items for active requests
            var allReturnRequestItems = new List<ReturnRequestItemResponse>();
            foreach (var activeReturnRequest in activeReturnRequests)
            {
                var items = await _mediator.Send(new GetReturnRequestItems(
                    new GetReturnRequestItemsFilter
                    {
                        ReturnRequestId = activeReturnRequest.Id
                    }));

                allReturnRequestItems.AddRange(items);
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

                // Calculate total previously returned amount for this item
                var totalReturnedAmount = allReturnRequestItems
                    .Where(e => e.OrderItemId == orderItem.Id)
                    .Sum(e => e.Amount);

                // Check if new return request amount exceeds remaining available amount
                if (createReturnRequestItemRequest.Amount > orderItem.Amount - totalReturnedAmount)
                {
                    context.AddFailure(new ValidationFailure(nameof(CreateReturnRequest.OrderId), null)
                    {
                        ErrorCode = ValidationErrorCodesEnum.NOT_EXISTS.ToString(),
                        ErrorMessage = "Toplam iade miktarı sipariş ürün adetini aşamaz."
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
