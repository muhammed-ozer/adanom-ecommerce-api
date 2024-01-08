namespace Adanom.Ecommerce.API.Pagination
{
    public class PaginationRequest
    {
        private int _pageSize;

        private int _page;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;

                if (_pageSize <= 10)
                {
                    _pageSize = 10;
                }
                else if (_pageSize > 50)
                {
                    _pageSize = 50;
                }
            }
        }

        public int Page
        {
            get
            {
                return _page;
            }
            set
            {
                _page = value > 0 ? value : 1;
            }
        }
    }
}
