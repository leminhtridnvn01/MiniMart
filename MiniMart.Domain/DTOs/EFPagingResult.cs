using Microsoft.EntityFrameworkCore;
using MiniMart.Domain.Base.BaseDTOs;

namespace MiniMart.Domain.DTOs
{
    public class EFPagingResult<TItem> : PagingResult<TItem>
    {
        public EFPagingResult()
        {
        }

        public EFPagingResult(bool isSuccess
            , string? message
            , int pageNo
            , int pageSize
            ) : base(isSuccess, message, pageNo, pageSize)
        {
        }

        public EFPagingResult(bool isSuccess
            , List<TItem> data
            , long totalRecords
            , int pageNo
            , int pageSize) : base(isSuccess, data, totalRecords, pageNo, pageSize)
        {
        }

        private async Task GetPagedListAsync(IQueryable<TItem> list)
        {
            TotalRecords = list.Count();
            Data = await list
                .Skip(Skip)
                .Take(PageSize)
                .ToListAsync();
        }

        public static async Task<PagingResult<TItem>> OKAsync(IQueryable<TItem> data
            , int pageNo
            , int pageSize
            , string? message = default)
        {
            var page = new EFPagingResult<TItem>(true, message, pageNo, pageSize);
            await page.GetPagedListAsync(data);
            return page;
        }
    }
}
