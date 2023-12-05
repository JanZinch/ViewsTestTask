using System;

namespace Progress
{
    [Serializable]
    public struct LastReceivedBonusData
    {
        public int Index { get; private set; }
        public DateTime ReceivingTime { get; private set; }
    }
}