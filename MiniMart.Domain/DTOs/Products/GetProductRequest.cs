using MiniMart.Domain.Base.BaseDTOs;

namespace MiniMart.Domain.DTOs.Products
{
    public class GetProductRequest : PagingRequestBase
    {
        public GetProductRequest()
        {

        }

        public string? Search { get; set; }
        public bool? IsSale { get; set; }
    }
}
