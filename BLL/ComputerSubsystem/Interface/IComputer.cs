using BLL.IElectronicComponentSubsystem;

namespace BLL.ComputerSubsystem.Interface
{
    public interface IComputer : IWatchable, ISoundHeadsetable, IMemoryable, IVideoCardable
    {
        string ModelName { get; }
    }
}
