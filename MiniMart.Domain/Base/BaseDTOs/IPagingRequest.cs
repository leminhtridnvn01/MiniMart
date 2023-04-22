using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.Base.BaseDTOs
{
    public interface IPagingRequest
    {
        int PageNo { get; set; }
        int PageSize { get; set; }
    }
}
