using BLL.MemoryComponent.Abstract;

namespace BLL.MemoryComponent.Class
{
    public sealed class HDD : ROMMemory
    {
        private const string memoryType = "HDD";
        private const int maxDownloadSpeed = 150;

        public HDD(int maxMemorySize)
            : base(memoryType, maxDownloadSpeed, maxMemorySize)
        {

        }

    }
}
