using BLL.ComputerPrograms.Interface;

namespace BLL.MemoryComponent.Interface
{
    public interface IROMMemory
    {
        string MemoryType { get; }
        int MaxDownloadSpeed { get; }

        int MaxMemorySize { get; }
        int CurrentMemorySize { get; }


        bool InstallProgram(IComputerProgram computerProgram);
        bool RemoveProgram(string programName);
        bool IsProgramInstalled(string programName);
        IComputerProgram FindProgram(string programName);


        IComputerProgram[] GetInstalledProgram();


        void CleanMemory();
    }
}
