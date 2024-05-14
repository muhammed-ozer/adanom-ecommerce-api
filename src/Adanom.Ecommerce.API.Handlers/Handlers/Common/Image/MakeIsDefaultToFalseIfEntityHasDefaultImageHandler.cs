namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class MakeIsDefaultToFalseIfEntityHasDefaultImageHandler : IRequestHandler<MakeIsDefaultToFalseIfEntityHasDefaultImage, bool>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public MakeIsDefaultToFalseIfEntityHasDefaultImageHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<bool> Handle(MakeIsDefaultToFalseIfEntityHasDefaultImage command, CancellationToken cancellationToken)
        {
            var image_Entity_Mapping = await _applicationDbContext.Image_Entity_Mappings
                .Where(e => e.EntityId == command.EntityId &&
                            e.EntityType == command.EntityType &&
                            e.IsDefault)
                .SingleOrDefaultAsync();

            if (image_Entity_Mapping == null) 
            {
                return true;
            }

            image_Entity_Mapping.IsDefault = false;

            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
