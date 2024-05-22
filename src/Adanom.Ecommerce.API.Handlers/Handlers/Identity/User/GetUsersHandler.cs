using Microsoft.AspNetCore.Identity;

namespace Adanom.Ecommerce.API.Handlers
{
    public sealed class GetUsersHandler : IRequestHandler<GetUsers, PaginatedData<UserResponse>>

    {
        #region Fields

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public GetUsersHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region IRequestHandler Members

        public async Task<PaginatedData<UserResponse>> Handle(GetUsers command, CancellationToken cancellationToken)
        {
            var usersQuery = _userManager.Users
                .Where(e => e.DeletedAtUtc == null)
                .AsNoTracking();

            if (command.Filter is not null)
            {
                #region Apply filtering

                if (command.Filter.Query != null)
                {
                    usersQuery = usersQuery.Where(e => e.Email.Contains(command.Filter.Query) ||
                                                       e.FirstName.Contains(command.Filter.Query) ||
                                                       e.LastName.Contains(command.Filter.Query));
                }

                if (command.Filter.EmailConfirmed != null)
                {
                    usersQuery = usersQuery.Where(e => e.EmailConfirmed);
                }

                if (command.Filter.AllowCommercialEmails != null)
                {
                    usersQuery = usersQuery.Where(e => e.AllowCommercialEmails);
                }

                if (command.Filter.AllowCommercialSMS != null)
                {
                    usersQuery = usersQuery.Where(e => e.AllowCommercialSMS);
                }

                if (command.Filter.StartDate != null)
                {
                    var startDate = command.Filter.StartDate.Value.StartOfDate();

                    usersQuery = usersQuery.Where(e => e.CreatedAtUtc.Date >= startDate);
                }

                if (command.Filter.EndDate != null)
                {
                    var endDate = command.Filter.EndDate.Value.EndOfDate();

                    usersQuery = usersQuery.Where(e => e.CreatedAtUtc.Date <= endDate);
                }

                #endregion

                #region Apply ordering

                usersQuery = command.Filter.OrderBy switch
                {
                    GetUsersOrderByEnum.EMAIL_ASC =>
                        usersQuery.OrderBy(e => e.Email),
                    GetUsersOrderByEnum.EMAIL_DESC =>
                        usersQuery.OrderByDescending(e => e.Email),
                    GetUsersOrderByEnum.CREATED_AT_ASC =>
                        usersQuery.OrderBy(e => e.CreatedAtUtc),
                    _ =>
                        usersQuery.OrderByDescending(e => e.CreatedAtUtc)
                };

                #endregion
            }

            var totalCount = usersQuery.Count();

            #region Apply pagination

            if (command.Pagination is not null)
            {
                usersQuery = usersQuery
                    .Skip((command.Pagination.Page - 1) * command.Pagination.PageSize)
                    .Take(command.Pagination.PageSize);
            }

            #endregion

            var userResponses = _mapper.Map<IEnumerable<UserResponse>>(await usersQuery.ToListAsync());

            return new PaginatedData<UserResponse>(
                userResponses,
                totalCount,
                command.Pagination?.Page ?? 1,
                command.Pagination?.PageSize ?? totalCount);
        }

        #endregion
    }
}
