namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteImagesHandler : IRequestHandler<DeleteImages, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteImagesHandler(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper, 
            IMediator mediator)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteImages command, CancellationToken cancellationToken)
        {
            var images = await _applicationDbContext.Images
                .Where(e => e.DeletedAtUtc == null &&
                            e.EntityId == command.EntityId &&
                            e.EntityType == command.EntityType)
                .ToListAsync();

            if (!images.Any())
            {
                return true;
            }

            // TODO: Test here after blob storage created
            foreach (var image in images) 
            {
                var deleteImageRequest = new DeleteImageRequest()
                {
                    Id = image.Id,
                };

                var deleteImageCommand = _mapper.Map(deleteImageRequest, new DeleteImage(command.Identity));

                await _mediator.Send(deleteImageCommand);
            }

            return true;
        }

        #endregion
    }
}
