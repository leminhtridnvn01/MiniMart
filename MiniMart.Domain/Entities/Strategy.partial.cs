using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniMart.Domain.Entities
{
    public partial class Strategy
    {
        public void AddProductToStrategy(Product product, Store store, int? percentageDecreases)
        {
            var strategyDetail = new StrategyDetail()
            {
                Product = product,
                Store = store,
                PercentageDecreases = percentageDecreases.HasValue
                                      ? percentageDecreases.Value
                                      : this.PercentageDecrease ?? 0
            };
            this.StrategyDetails.Add(strategyDetail);
        }

        public void UpdateProductPriceInStrategy()
        {
            foreach (var strategyDetail in this.StrategyDetails)
            {
                var percentageDecreases = strategyDetail.PercentageDecreases.HasValue
                                     ? strategyDetail.PercentageDecreases.Value
                                     : this.PercentageDecrease ?? 0;
                if (percentageDecreases > 0)
                {
                    //if (this.PercentageDecrease.HasValue && this.PercentageDecrease > 0)
                    //{
                    //    strategyDetail.Product.PriceDecreases = (int?)(strategyDetail.Product.Price * ((100.0 - percentageDecreases) / 100.0));
                    //}

                    var productStore = strategyDetail.Product.ProductStores.FirstOrDefault(x => x.Store.Id == strategyDetail.StoreId);
                    productStore.PriceDecreases = (int?)(strategyDetail.Product.Price * ((100.0 - percentageDecreases) / 100.0));
                }
            }
        }

        public void UpdateProductPriceInExpiredStrategy()
        {
            foreach (var strategyDetail in this.StrategyDetails)
            {
                if (this.PercentageDecrease.HasValue && this.PercentageDecrease > 0)
                {
                    strategyDetail.Product.PriceDecreases = null;
                }

                var productStore = strategyDetail.Product.ProductStores.FirstOrDefault(x => x.Store.Id == strategyDetail.StoreId);
                productStore.PriceDecreases = null;
            }
        }
    }
}
