using System.Security.Claims;
using Adanom.Ecommerce.API.Data.Models;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class UpdateImage_EntityHandler : IRequestHandler<UpdateImage_Entity, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public UpdateImage_EntityHandler(ApplicationDbContext applicationDbContext, IMapper mapper, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(UpdateImage_Entity command, CancellationToken cancellationToken)
        {
            var userId = command.Identity.GetUserId();

            var image_Entity = await _applicationDbContext.Image_Entity_Mappings
                .Where(e => e.ImageId == command.ImageId)
                .SingleAsync();

            if (command.IsDefault && !image_Entity.IsDefault)
            {
                await _mediator.Send(new MakeIsDefaultToFalseIfEntityHasDefaultImage(image_Entity.EntityId, image_Entity.EntityType));
            }
            else
            {
                command.IsDefault = true;
            }

            image_Entity = _mapper.Map(command, image_Entity);

            _applicationDbContext.Update(image_Entity);

            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                await _mediator.Publish(new CreateLog(new AdminTransactionLogRequest()
                {
                    UserId = userId,
                    EntityType = EntityType.IMAGE_ENTITY,
                    TransactionType = TransactionType.UPDATE,
                    Description = LogMessages.AdminTransaction.DatabaseSaveChangesHasFailed,
                    Exception = exception.ToString()
                }));
                return false;
            }

            return true;
        }

        #endregion
    }
}
