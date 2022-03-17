namespace BLL.ComputerPrograms.Interface
{
    public interface IComputerProgram
    {
        string Name { get; }
        int Size { get; }

        bool NeedInternet { get; }
        bool NeedHeadset { get; }
    }
}
