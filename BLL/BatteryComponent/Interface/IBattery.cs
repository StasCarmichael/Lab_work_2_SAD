namespace BLL.BatteryComponent.Interface
{
    public interface IBattery
    {
        int MaxAmountCharge { get; }
        int CurrentAmountCharge { get; }


        bool IsDischarged { get; }

        void Charge();
        bool Unload(int amountOfCharge);
    }
}
