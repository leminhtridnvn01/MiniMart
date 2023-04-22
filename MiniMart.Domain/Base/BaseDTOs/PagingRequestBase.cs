using MiniMart.Domain.Constants;

namespace MiniMart.Domain.Base.BaseDTOs
{
    public class PagingRequestBase : IPagingRequest
    {
        public int PageNo { get; set; } = DefaultPaging.DEFAULT_PAGE_NO;
        public int PageSize { get; set; } = DefaultPaging.DEFAULT_PAGE_SIZE;
    }
}
