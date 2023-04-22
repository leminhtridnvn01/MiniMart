using MiniMart.Domain.Entities;
using System.Linq.Expressions;
using System.Security.Principal;

namespace MiniMart.Domain.DTOs.Categories
{
    public class GetAllCategoryResponse
    {
        public GetAllCategoryResponse()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }

        public Expression<Func<Category, GetAllCategoryResponse>> GetSelection()
        {
            return _ => new GetAllCategoryResponse() { Id = _.Id, Name = _.Name, Img = _.Img };
        }
    }
}
