using BLL.BatteryComponent.Interface;

namespace BLL.BatteryComponent.Abstract
{
    public abstract class AbstractBattery : IBattery
    {
        private int countOfCharge;
        private readonly int maxCharge;


        public AbstractBattery(int maxCountOfCharge)
        {
            maxCharge = maxCountOfCharge;
            countOfCharge = 0;
        }


        public int MaxAmountCharge { get { return maxCharge; } }
        public int CurrentAmountCharge
        {
            get { return countOfCharge; }
            private set
            {
                if (value <= MaxAmountCharge)
                {
                    countOfCharge = value;
                }
            }
        }


        public bool IsDischarged
        {
            get
            {
                if (CurrentAmountCharge == 0)
                    return true;

                return false;
            }
        }


        public void Charge()
        {
            CurrentAmountCharge = MaxAmountCharge;
        }
        public bool Unload(int amountOfCharge)
        {
            if (CurrentAmountCharge >= amountOfCharge)
            {
                CurrentAmountCharge -= amountOfCharge;
                return true;
            }

            return false;
        }
    }
}
