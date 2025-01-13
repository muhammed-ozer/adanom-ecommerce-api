namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class DeleteImagesHandler : IRequestHandler<DeleteImages, bool>
    {
        #region Fields

        private readonly IDbContextFactory<ApplicationDbContext> _applicationDbContextFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        #endregion

        #region Ctor

        public DeleteImagesHandler(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory, IMapper mapper, IMediator mediator)
        {
            _applicationDbContextFactory = applicationDbContextFactory ?? throw new ArgumentNullException(nameof(applicationDbContextFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(DeleteImages command, CancellationToken cancellationToken)
        {
            await using var applicationDbContext = await _applicationDbContextFactory.CreateDbContextAsync(cancellationToken);

            var images = await applicationDbContext.Images
                .Where(e => e.EntityId == command.EntityId &&
                            e.EntityType == command.EntityType)
                .ToListAsync();

            if (!images.Any())
            {
                return true;
            }

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
