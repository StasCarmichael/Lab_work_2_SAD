using BLL.IElectronicComponentSubsystem;
using BLL.EventArgs;

namespace BLL.ComputerSubsystem.Interface
{
    public interface IComputer : IWatchable, IElectricalConnectable, IInternetConnectable, ISoundHeadsetable, IMemoryable, IVideoCardable, IEventLoggerable
    {
        string ModelName { get; }
    }
}
