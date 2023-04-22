using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.DTOs;
using System.Linq.Expressions;

namespace MiniMart.API.Extensions
{
    public static class CollectionExtensions
    {
        public static PagingResult<T> ToPagedList<T>(this IEnumerable<T> source
            , int pageNo = 0
            , int pageSize = 0)
        {
            return PagingResult<T>.OK(source, pageNo, pageSize);
        }

        public static Task<PagingResult<T>> ToPagedListAsync<T>(this IQueryable<T> source
            , int pageNo = 0
            , int pageSize = 0)
        {
            return EFPagingResult<T>.OKAsync(source, pageNo, pageSize);
        }

        public static IOrderedQueryable<T> Order<T>(this IQueryable<T> source
            , SortInfo sort)
        {
            string method = sort.IsDesc ? "OrderByDescending" : "OrderBy";
            var parameterExpr = Expression.Parameter(typeof(T), "_");
            var orderPro = Expression.Property(parameterExpr, sort.Property);
            var sortErpx = Expression.Lambda(orderPro, parameterExpr);

            var call = Expression.Call(
                typeof(Queryable)
                , method
                , new[] { typeof(T), orderPro.Type },
                source.Expression,
                Expression.Quote(sortErpx));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }

        public static IOrderedQueryable<T> OrderThen<T>(this IOrderedQueryable<T> source
            , SortInfo sort)
        {
            string method = sort.IsDesc ? "ThenByDescending" : "ThenBy";
            var parameterExpr = Expression.Parameter(typeof(T), "_");
            var orderPro = Expression.Property(parameterExpr, sort.Property);
            var sortErpx = Expression.Lambda(orderPro, parameterExpr);

            var call = Expression.Call(
                typeof(Queryable)
                , method
                , new[] { typeof(T), orderPro.Type },
                source.Expression,
                Expression.Quote(sortErpx));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery<T>(call);
        }
    }
}
