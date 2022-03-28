using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.IElectronicComponentSubsystem;

namespace BLL.ComputerSubsystem.Interface
{
    public interface IComputer : IWatchable, ISoundHeadsetable, IMemoryable, IBatteryable, IVideoCardable
    {

    }
}
