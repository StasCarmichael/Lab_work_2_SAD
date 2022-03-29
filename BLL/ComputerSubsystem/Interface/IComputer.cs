using BLL.IElectronicComponentSubsystem;
using BLL.EventArgs;

namespace BLL.ComputerSubsystem.Interface
{
    public interface IComputer : IWatchable, IElectricalConnectable, ISoundHeadsetable, IMemoryable, IVideoCardable, IEventLoggerable
    {
        string ModelName { get; }
    }
}
