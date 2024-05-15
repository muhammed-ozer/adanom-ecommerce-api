namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetMetaInformationHandler : IRequestHandler<GetMetaInformation, MetaInformationResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetMetaInformationHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<MetaInformationResponse?> Handle(GetMetaInformation command, CancellationToken cancellationToken)
        {
            MetaInformation? metaInformation = null;

            if (command.Id > 0)
            {
                metaInformation = await _applicationDbContext.MetaInformations
                    .AsNoTracking()
                    .Where(e => e.Id == command.Id)
                    .SingleOrDefaultAsync();
            }
            else if(command.EntityId > 0)
            {
                metaInformation = await _applicationDbContext.MetaInformations
                    .AsNoTracking()
                    .Where(e => e.EntityId == command.EntityId &&
                                e.EntityType == command.EntityType)
                    .FirstOrDefaultAsync();
            }
           
            return _mapper.Map<MetaInformationResponse>(metaInformation);
        } 

        #endregion
    }
}
