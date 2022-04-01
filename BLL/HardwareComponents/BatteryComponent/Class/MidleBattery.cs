using BLL.BatteryComponent.Abstract;

namespace BLL.BatteryComponent.Class
{
    public class MidleBattery : AbstractBattery
    {
        private const int maxCountOfCharge = 5000;

        public MidleBattery()
            : base(maxCountOfCharge)
        { }
    }
}
