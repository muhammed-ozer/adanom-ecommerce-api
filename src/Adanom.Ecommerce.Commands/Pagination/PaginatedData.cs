namespace Adanom.Ecommerce.Pagination
{
    public sealed class PaginatedData<TObject>
    {
        public IEnumerable<TObject> Rows
        {
            get;
        }

        public PaginationInfo Pagination
        {
            get;
        }

        public PaginatedData(IEnumerable<TObject> rows, int totalCount, int currentPage, int pageSize)
        {
            Rows = rows;
            Pagination = new PaginationInfo(currentPage, totalCount, pageSize);
        }
    }
}
