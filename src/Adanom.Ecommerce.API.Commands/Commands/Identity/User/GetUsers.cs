namespace Adanom.Ecommerce.API.Commands
{
    public class GetUsers : IRequest<PaginatedData<UserResponse>>
    {
        #region Ctor

        public GetUsers(GetUsersFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetUsersFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
