using Microsoft.EntityFrameworkCore;
using MiniMart.API.Exceptions;
using MiniMart.API.Extensions;
using MiniMart.Domain.Base.BaseDTOs;
using MiniMart.Domain.DTOs.Strategies;
using MiniMart.Domain.Entities;
using MiniMart.Domain.Interfaces;
using MiniMart.Domain.Interfaces.Repositories;
using MiniMart.Infrastructure.Data.Repositories;
using System.Net;
using System.Security.Claims;

namespace MiniMart.API.Services
{
    public class StrategyService : BaseService
    {
        private readonly IStrategyRepository _strategyRepository;
        private readonly IStrategyDetailRepository _strategyDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IProductStoreRepository _productStoreRepository;
        private readonly ClaimsPrincipal _user;

        public StrategyService(IStrategyRepository strategyRepository
            , IStrategyDetailRepository strategyDetailRepository
            , IProductRepository productRepository
            , IStoreRepository storeRepository
            , IProductStoreRepository productStoreRepository
            , ClaimsPrincipal user
            , IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _strategyRepository = strategyRepository;
            _strategyDetailRepository = strategyDetailRepository;
            _productRepository = productRepository;
            _storeRepository = storeRepository;
            _productStoreRepository = productStoreRepository;
            _user = user;
        }

        private async Task<Product> ValidateProduct(int productId)
        {
            var product = await _productRepository.GetAsync(x => x.Id == productId);
            if (product == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the Product with Id equal " + productId);
            }
            return product;
        }

        private async Task<Store> ValidateStore(int storeId)
        {
            var store = await _storeRepository.GetAsync(x => x.Id == storeId);
            if (store == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, "Could not found the Store with Id equal " + storeId);
            }
            return store;
        }

        private async Task<(Product, Store)> ValidateProductInStore(int productId, int storeId)
        {
            var product = await ValidateProduct(productId);
            var store = await ValidateStore(storeId);
            var isValid = await _productStoreRepository.AnyAsync(x => x.Store.Id == store.Id && x.Product.Id == product.Id && x.Quantity > 0);
            if (!isValid)
            {
                throw new HttpException(HttpStatusCode.BadRequest, "This product is currently out of stock at this store.");
            }
            return (product, store);
        }

        public async Task<bool> AddStrategyAsync(AddStrategyRequest request)
        {
            try
            {
                await _unitOfWork.BeginTransaction();
                var strategy = new Strategy()
                {
                    ActivatedDateFrom = request.ActivatedDateFrom,
                    ActivatedDateTo = request.ActivatedDateTo,
                    PercentageDecrease = request.PercentageDecrease,
                };

                foreach (var item in request.Products)
                {
                    var (product, store) = await ValidateProductInStore(item.ProductId, item.StoreId);
                    strategy.AddProductToStrategy(product, store, item.PercentageDecreases);
                }
                await _strategyRepository.InsertAsync(strategy);
                return await _unitOfWork.CommitTransaction();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransaction();
                throw new HttpException(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        public async Task<bool> Test()
        {
            await _unitOfWork.BeginTransaction();
            var strategies = _strategyRepository.GetQuery(_ => _.ActivatedDateFrom.Value <= DateTime.Now
                                                                     && _.ActivatedDateTo.Value >= DateTime.Now
                                                                     && _.LK_ActivatedStrategyStatus == null);
            if (strategies.Any())
            {
                foreach (var strategy in strategies)
                {
                    strategy.LK_ActivatedStrategyStatus = Domain.Enums.LK_ActivatedStrategyStatus.Active;
                    strategy.UpdateProductPriceInStrategy();
                    Console.WriteLine("Da update item co id la: " + strategy.Id);
                }
            }

            //var products = _productRepository.GetQuery(x => !x.IsDelete);
            //if (products.Any())
            //{
            //    foreach(var product in products)
            //    {
            //        foreach (var productDetail in product.ProductDetails)
            //        {
            //            var a = productDetail.Product.Id;
            //            Console.WriteLine(a);
            //        }
            //    }
            //}
            Console.WriteLine("Da update thanh cong");
            await _unitOfWork.CommitTransaction();
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<PagingResult<GetStrategyResponse>> GetManagerStrategy (GetStrategyRequest request)
        {
            var a =  _user.GetUserId();
            var strategies = await _strategyRepository.GetQuery(x => request.StartDate <= x.ActivatedDateFrom.Value 
                                                                     &&  request.EndDate >= x.ActivatedDateTo.Value
                                                                     && x.StrategyDetails.Single().Store.Manager.UserId == _user.GetUserId() )
                                                        .Select(x => new GetStrategyResponse()
                                                        {
                                                            Name = x.Name,
                                                            Description = x.Description,
                                                            ActivatedDateFrom = x.ActivatedDateFrom,
                                                            ActivatedDateTo = x.ActivatedDateTo,
                                                            PercentageDecrease = x.PercentageDecrease,
                                                            LK_ActivatedStrategyStatus = x.LK_ActivatedStrategyStatus,
                                                            Products = x.StrategyDetails.Select(sd => 
                                                                new GetStrategyProductResponse()
                                                                {
                                                                    ProductId = sd.ProductId,
                                                                    ProductName = sd.Product.Name,
                                                                    StoreId = sd.StoreId,
                                                                    StoreName = sd.Store.Name,
                                                                    PercentageDecrease = sd.PercentageDecreases,
                                                                    OriginalPrice = sd.Product.Price,
                                                                    OriginalPriceDecreases = sd.Product.PriceDecreases,
                                                                    CurrentPrice = sd.Product.ProductStores.FirstOrDefault(ps => ps.Store.Id == sd.StoreId).Price,
                                                                    CurrentPriceDecreases = sd.Product.ProductStores.FirstOrDefault(ps => ps.Store.Id == sd.StoreId).PriceDecreases,
                                                                })
                                                            })
                                                        .ToPagedListAsync(request.PageNo, request.PageSize);
            return strategies;                  
        }
    }
}
