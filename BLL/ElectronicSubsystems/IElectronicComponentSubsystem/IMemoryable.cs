using BLL.ComputerPrograms.Interface;
using BLL.MemoryComponent.Interface;

namespace BLL.IElectronicComponentSubsystem
{
    public interface IMemoryable
    {
        IROMMemory OpenRomMemory();
        bool AddNewROMMemory(IROMMemory memory);


        bool OpenProgram(string programName, int time);
        bool StartGame(string gameName, int time);


        bool InstallProgram(IComputerProgram computerProgram);
        bool RemoveProgram(string programName);
    }
}
