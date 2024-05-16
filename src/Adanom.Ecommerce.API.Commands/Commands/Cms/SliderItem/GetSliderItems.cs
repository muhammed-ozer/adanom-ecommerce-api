using Adanom.Ecommerce.API.Commands.Models;

namespace Adanom.Ecommerce.API.Commands
{
    public class GetSliderItems : IRequest<PaginatedData<SliderItemResponse>>
    {
        #region Ctor

        public GetSliderItems(GetSliderItemsFilter? filter = null, PaginationRequest? pagination = null)
        {
            Filter = filter;
            Pagination = pagination;
        }

        #endregion

        #region Properties

        public GetSliderItemsFilter? Filter { get; set; }

        public PaginationRequest? Pagination { get; }

        #endregion
    }
}
