namespace Adanom.Ecommerce.Pagination
{
    public sealed class PaginationInfo
    {
        public int CurrentPage
        {
            get;
        }

        public int TotalCount
        {
            get;
        }

        public int PageSize
        {
            get;
        }

        public int PageCount
        {
            get;
        }

        public PaginationInfo(int currentPage, int totalCount, int pageSize)
        {
            CurrentPage = currentPage;
            TotalCount = totalCount;
            PageSize = pageSize;
            PageCount = CalculatePageCount(totalCount, pageSize);
        }

        private int CalculatePageCount(int totalCount, int pageSize)
        {
            var result = Math.Ceiling(Convert.ToDouble(totalCount) / Convert.ToDouble(pageSize));

            if (double.IsNaN(result))
            {
                return 0;
            }

            return Convert.ToInt32(result);
        }
    }
}
