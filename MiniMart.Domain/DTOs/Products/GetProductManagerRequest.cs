using MiniMart.Domain.Base.BaseDTOs;

namespace MiniMart.Domain.DTOs.Products
{
    public class GetProductManagerRequest : PagingRequestBase
    {
        public GetProductManagerRequest()
        {

        }

        public string? Search { get; set; }
        public int StoreId { get; set; }
    }
}
