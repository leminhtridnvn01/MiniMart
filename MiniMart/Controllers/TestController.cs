using Microsoft.AspNetCore.Mvc;
using MiniMart.Domain.Enums;
using MiniMart.Domain.Interfaces;
using MiniMart.Domain.Interfaces.Repositories;

namespace MiniMart.API.Controllers
{
    public class TestController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TestController(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
    
}
