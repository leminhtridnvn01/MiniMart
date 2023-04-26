using Microsoft.AspNetCore.Mvc;
using MiniMart.API.Exceptions;
using MiniMart.Domain.DTOs.Products;
using System.Net;

namespace MiniMart.API.Services
{
    public partial class CategoryService
    {
        public async Task<int> AddProductAsyncs(AddProductRequest request, int categoryId)
        {
            var category = await ValidateCategory(categoryId);

            var product = request.GetMap();

            category.AddProduct(product);

            await _unitOfWork.SaveChangeAsync();

            return product.Id;
        }
    }
}
