﻿namespace MiniMart.Domain.Base.BaseDTOs
{
    public class PaginationFilter
    {
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize < 1 ? 10 : pageSize;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        
    }
}
