using System;
using System.Collections.Generic;
using System.Linq;


namespace BLL.MemoryComponent.Interface
{
    public interface IROMMemory
    {
        int MaxMemorySize { get; }
        int CurrentMemorySize { get; }



    }
}
