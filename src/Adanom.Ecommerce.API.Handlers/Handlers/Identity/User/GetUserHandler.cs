namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetUserHandler : IRequestHandler<GetUser, UserResponse?>
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetUserHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<UserResponse?> Handle(GetUser command, CancellationToken cancellationToken)
        {
            var usersQuery = _applicationDbContext.Users
                .Where(e => e.DeletedAtUtc == null)
                .AsNoTracking();

            User? user;

            if (command.Email.IsNotNullOrEmpty())
            {
                user =  await usersQuery.SingleOrDefaultAsync(e => e.Email == command.Email);
            }
            else
            {
                user = await usersQuery.SingleOrDefaultAsync(e => e.Id == command.Id);
            }

            return _mapper.Map<UserResponse>(user);
        } 

        #endregion
    }
}
