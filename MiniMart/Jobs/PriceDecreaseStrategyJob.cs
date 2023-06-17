using Microsoft.EntityFrameworkCore;
using MiniMart.Domain.Interfaces;
using MiniMart.Domain.Interfaces.Repositories;
using Quartz;

namespace MiniMart.API.Jobs
{
    public class PriceDecreaseStrategyJob : IJob
    {
        private readonly IStrategyRepository _strategyRepository;
        private readonly IStrategyDetailRepository _strategyDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PriceDecreaseStrategyJob(IStrategyRepository strategyRepository
            , IStrategyDetailRepository strategyDetailRepository
            , IUnitOfWork unitOfWork)
        {
            _strategyRepository = strategyRepository;
            _strategyDetailRepository = strategyDetailRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Console.WriteLine("-----------PriceDecreaseStrategyJob-----------");
                var strategies = await _strategyRepository.GetQuery(_ => _.ActivatedDateFrom.Value <= DateTime.Now
                                                                         && _.ActivatedDateTo.Value >= DateTime.Now
                                                                         && _.LK_ActivatedStrategyStatus == null)
                                                           .ToListAsync();
                if (strategies.Count() > 0)
                {
                    foreach (var strategy in strategies)
                    {
                        strategy.LK_ActivatedStrategyStatus = Domain.Enums.LK_ActivatedStrategyStatus.Active;
                        strategy.UpdateProductPriceInStrategy();
                    }
                }
                await _unitOfWork.SaveChangeAsync();
                await Task.CompletedTask;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            await Task.CompletedTask;
        }
    }
}
