using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteSliderItemHandler : IRequestHandler<DeleteSliderItem, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteSliderItemHandler(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteSliderItem command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var sldierItem = await _applicationDbContext.SliderItems
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            await _applicationDbContext.Database.BeginTransactionAsync();

            _applicationDbContext.Remove(sldierItem);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.SLIDERITEM,
                    TransactionType = TransactionType.DELETE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.SLIDERITEM,
                TransactionType = TransactionType.DELETE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, sldierItem.Id),
            }));

            return true;
        }

        #endregion
    }
}
