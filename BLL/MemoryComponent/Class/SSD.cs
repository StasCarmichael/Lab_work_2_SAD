using System;
using BLL.MemoryComponent.Abstract;

namespace BLL.MemoryComponent.Class
{
    public sealed class SSD : ROMMemory
    {
        private const string memoryType = "SSD";
        private const int maxDownloadSpeed = 1200;

        public SSD(int maxMemorySize)
            : base(memoryType, maxDownloadSpeed, maxMemorySize)
        {

        }
    }
}
