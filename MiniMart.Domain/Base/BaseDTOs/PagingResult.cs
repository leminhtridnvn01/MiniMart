using MiniMart.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiniMart.Domain.Base.BaseDTOs
{
    public class PagingResult<TItem> : ResultBase<List<TItem>>
    {
        private int _pageNo = DefaultPaging.DEFAULT_PAGE_NO;
        private int _pageSize = DefaultPaging.DEFAULT_PAGE_SIZE;

        [JsonIgnore]
        public int Skip => Calculator.GetSkip(PageNo, PageSize);

        public long TotalPages => Calculator.GetTotalPages(TotalRecords, PageSize);

        public int PageNo
        {
            get => _pageNo;
            set => _pageNo = (value > 0) ? value : _pageNo;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > 0) ? value : _pageSize;
        }

        public long TotalRecords { get; set; }

        public PagingResult()
        {
        }

        public PagingResult(bool isSuccess
            , string message
            , int pageNo
            , int pageSize) : base(isSuccess, message, null)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }

        public PagingResult(bool isSuccess
            , List<TItem> data
            , long totalRecords
            , int pageNo
            , int pageSize) : base(isSuccess, null, data)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }

        private void GetPagedList(IEnumerable<TItem> list)
        {
            TotalRecords = list.Count();
            Data = list
                .Skip(Skip)
                .Take(PageSize)
                .ToList();
        }

        public new static PagingResult<TItem> Error(string message)
        {
            return new PagingResult<TItem>(false, message, 0, 0);
        }

        public static PagingResult<TItem> OK(IEnumerable<TItem> data
            , int pageNo
            , int pageSize
            , string message = default)
        {
            var page = new PagingResult<TItem>(true, message, pageNo, pageSize);
            page.GetPagedList(data);

            return page;
        }

        public static PagingResult<TItem> Instance(List<TItem> data
            , long totalRecords
            , int pageNo
            , int pageSize)
        {
            var page = new PagingResult<TItem>(true, data, totalRecords, pageNo, pageSize);
            return page;
        }

        public static PagingCalculator Calculator = new PagingCalculator();

        public class PagingCalculator
        {
            public int GetTotalPages(long totalRecord, int pageSize)
            {
                return (int)((totalRecord / pageSize) + (totalRecord % pageSize > 0 ? 1 : 0));
            }

            public int GetSkip(int pageNo, int pageSize)
            {
                return (pageNo - 1) * pageSize;
            }

            public int GetPageNo(long skip, int pageSize)
            {
                if (pageSize <= 0)
                    pageSize = DefaultPaging.DEFAULT_PAGE_SIZE;
                return ((int)(skip / pageSize) + 1);
            }
        }
    }
}
