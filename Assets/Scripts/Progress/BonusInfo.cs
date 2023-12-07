using System;

namespace Progress
{
    [Serializable]
    public struct BonusInfo
    {
        public int Index { get; private set; }
        public DateTime ReceivingTime { get; private set; }

        public BonusInfo(int index, DateTime receivingTime)
        {
            Index = index;
            ReceivingTime = receivingTime;
        }
    }
}