using MiniMart.Domain.Interfaces.Repositories;
using MiniMart.Domain.Interfaces;
using Quartz;
using Microsoft.EntityFrameworkCore;

namespace MiniMart.API.Jobs
{
    public class PriceDecreaseStrategyExpiredJob : IJob
    {
        private readonly IStrategyRepository _strategyRepository;
        private readonly IStrategyDetailRepository _strategyDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PriceDecreaseStrategyExpiredJob(IStrategyRepository strategyRepository
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
                Console.WriteLine("-----------PriceDecreaseStrategyExpiredJob-----------");
                var strategies = await _strategyRepository.GetQuery(_ => _.LK_ActivatedStrategyStatus == Domain.Enums.LK_ActivatedStrategyStatus.Active
                                                                         && _.ActivatedDateTo.Value < DateTime.Now)
                                                           .ToListAsync();
                if (strategies.Count() > 0)
                {
                    foreach (var strategy in strategies)
                    {
                        strategy.LK_ActivatedStrategyStatus = Domain.Enums.LK_ActivatedStrategyStatus.Pass;
                        strategy.UpdateProductPriceInExpiredStrategy();
                        Console.WriteLine("Da update expired item co id la: " + strategy.Id);
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
