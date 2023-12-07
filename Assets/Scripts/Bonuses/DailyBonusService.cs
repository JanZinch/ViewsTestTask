using System;
using Models;
using Progress;

namespace Bonuses
{
    public class DailyBonusService
    {
        private readonly ProgressDataModel _progressDataModel;
        private readonly DailyBonusesContainer _dailyBonusesContainer;

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

        public DailyBonusService(ProgressDataModel progressDataModel, DailyBonusesContainer dailyBonusesContainer)
        {
            _progressDataModel = progressDataModel;
            _dailyBonusesContainer = dailyBonusesContainer;
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

        public void AcceptAvailableBonus()
        {
            BonusInfo bonusInfo = new BonusInfo(AvailableBonusIndex, DateTime.Now);
            _progressDataModel.AppendReceivedBonus(bonusInfo);
        }
    }
}