using BLL.IElectronicComponentSubsystem;

namespace BLL.SmartphoneSubsystem.Interface
{
    public interface ISmartphone : IWatchable, IBatteryable, ISoundHeadsetable, IMemoryable, IVideoCardable
    {
        string ModelName { get; }
    }
}
