using BLL.IElectronicComponentSubsystem;
using BLL.EventArgs;

namespace BLL.SmartphoneSubsystem.Interface
{
    public interface ISmartphone : IWatchable, IElectricalConnectable, IBatteryable, ISoundHeadsetable, IMemoryable, IVideoCardable, IEventLoggerable
    {
        string ModelName { get; }
    }
}
