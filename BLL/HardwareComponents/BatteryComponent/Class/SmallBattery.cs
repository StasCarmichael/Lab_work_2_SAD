using BLL.BatteryComponent.Abstract;

namespace BLL.BatteryComponent.Class
{
    public sealed class SmallBattery : AbstractBattery
    {
        private const int maxCountOfCharge = 3000;

        public SmallBattery()
            : base(maxCountOfCharge)
        { }
    }
}
