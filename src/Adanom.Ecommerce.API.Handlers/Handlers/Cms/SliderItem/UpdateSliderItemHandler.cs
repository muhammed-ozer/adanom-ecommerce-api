using System.Security.Claims;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateSliderItemHandler : IRequestHandler<UpdateSliderItem, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateSliderItemHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateSliderItem command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var sliderItem = await _applicationDbContext.SliderItems
                .Where(e => e.Id == command.Id)
                .SingleAsync();

            sliderItem = _mapper.Map(command, sliderItem, options =>
            {
                options.AfterMap((source, target) =>
                {
                    target.UpdatedByUserId = userId;
                    target.UpdatedAtUtc = DateTime.UtcNow;
                });
            });

            _applicationDbContext.Update(sliderItem);

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
                    TransactionType = TransactionType.UPDATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));

                return false;
            }

            await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
            {
                UserId = userId,
                EntityType = EntityType.SLIDERITEM,
                TransactionType = TransactionType.UPDATE,
                Description = string.Format(LogMessages.AdminTransaction.DatabaseSaveChangesSuccessful, sliderItem.Id),
            }));

            return true;
        }

        #endregion
    }
}
