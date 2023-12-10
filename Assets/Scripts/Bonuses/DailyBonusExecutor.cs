using Bonuses.Views;
using Core.Basics;

namespace Bonuses
{
    public class DailyBonusExecutor
    {
        private readonly ViewsFactory _viewsFactory;
        private readonly DailyBonusService _dailyBonusService;

        public DailyBonusExecutor(ViewsFactory viewsFactory, DailyBonusService dailyBonusService)
        {
            _viewsFactory = viewsFactory;
            _dailyBonusService = dailyBonusService;
        }

        public BaseView Execute()
        {
            if (_dailyBonusService.AvailableBonusIndex == DailyBonusService.WeeklyBonusIndex)
            {
                _dailyBonusService.AcceptAvailableBonus();
                
                return _viewsFactory.ShowView<CongratsDailyBonusView>()
                    .Initialize(DailyBonusService.WeeklyBonusIndex, 
                        _dailyBonusService.Container.GetBonusByIndex(DailyBonusService.WeeklyBonusIndex));
            }
            else
            {
                return _viewsFactory.ShowView<DailyBonusPresenter>().InjectDependencies(_viewsFactory, _dailyBonusService);
            }
            
            
        }

    }
}