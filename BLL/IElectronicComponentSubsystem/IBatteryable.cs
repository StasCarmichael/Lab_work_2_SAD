using BLL.BatteryComponent.Interface;

namespace BLL.IElectronicComponentSubsystem
{
    public interface IBatteryable
    {
        bool SetNewBattery(IBattery battery);


        int MaxAmountCharge { get; }
        int CurrentAmountCharge { get; }
        void Charge();
    }
}
