using System;
using InAppResources;
using Models;
using Progress;

namespace Bonuses
{
    public class DailyBonusService
    {
        private readonly ProgressDataModel _progressDataModel;
        private readonly DailyBonusesContainer _dailyBonusesContainer;
        private readonly ResourceService _resourceService;

        public const int WeeklyBonusIndex = 6;
        
        public DailyBonusesContainer Container => _dailyBonusesContainer;

        public int LastReceivedBonusIndex => _progressDataModel.LastReceivedBonus.Index;
        
        public int AvailableBonusIndex
        {
            get
            {
                BonusInfo lastReceivedBonus = _progressDataModel.LastReceivedBonus;

                if (DateTime.Now.DayOfYear != lastReceivedBonus.ReceivingTime.DayOfYear)
                {
                    return NextBonusIndex(lastReceivedBonus.Index);
                }
                else 
                    return -1;

            }
        }

        public DailyBonus AvailableBonus
        {
            get
            {
                int availableBonusIndex = AvailableBonusIndex;

                return availableBonusIndex == -1 ? 
                    null : _dailyBonusesContainer.GetBonusByIndex(availableBonusIndex);
            }
        }

        public DailyBonusService(ProgressDataModel progressDataModel, DailyBonusesContainer dailyBonusesContainer, 
            ResourceService resourceService)
        {
            _progressDataModel = progressDataModel;
            _dailyBonusesContainer = dailyBonusesContainer;
            _resourceService = resourceService;
        }

        private int NextBonusIndex(int previousIndex)
        {
            if (previousIndex >= _dailyBonusesContainer.AllBonusesCount - 1)
            {
                return 0;
            }
            else
            {
                return ++previousIndex;
            }
        }

        public bool AcceptAvailableBonus()
        {
            int availableBonusIndex = AvailableBonusIndex;

            if (availableBonusIndex == -1)
            {
                return false;
            }
            
            DailyBonus dailyBonus = _dailyBonusesContainer.GetBonusByIndex(availableBonusIndex);
            _resourceService.AppendResourceAmount(dailyBonus.ResourceType, dailyBonus.ResourceAmount);
            
            BonusInfo bonusInfo = new BonusInfo(availableBonusIndex, DateTime.Now);
            _progressDataModel.AppendReceivedBonus(bonusInfo);

            return true;
        }
    }
}