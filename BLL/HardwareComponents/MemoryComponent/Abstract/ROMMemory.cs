using System;
using System.Collections.Generic;

using BLL.ComputerPrograms.Interface;
using BLL.MemoryComponent.Interface;

namespace BLL.MemoryComponent.Abstract
{
    public abstract class ROMMemory : IROMMemory
    {
        private const int MIN_Memory_Size = 512;
        private const int MAX_Memory_Size = 1048576;

        private List<IComputerProgram> computerPrograms;


        public ROMMemory(string memoryType, int maxDownloadSpeed, int maxMemorySize)
        {
            MemoryType = memoryType;
            MaxDownloadSpeed = maxDownloadSpeed;

            if (maxMemorySize >= MIN_Memory_Size && maxMemorySize <= MAX_Memory_Size)
                MaxMemorySize = maxMemorySize;
            else { throw new ArgumentException("Максимальний розмір диску невідповідає вимозі"); }

            CurrentMemorySize = 0;
            computerPrograms = new List<IComputerProgram>();
        }


        public string MemoryType { get; private set; }
        public int MaxDownloadSpeed { get; private set; }


        public int MaxMemorySize { get; private set; }
        public int CurrentMemorySize { get; private set; }


        public void CleanMemory()
        {
            CurrentMemorySize = 0;
            computerPrograms.Clear();
        }


        public IComputerProgram FindProgram(string programName)
        {
            return computerPrograms.Find((match) => match.Name == programName);
        }
        public IComputerProgram[] GetInstalledProgram() { return computerPrograms.ToArray(); }

        public bool InstallProgram(IComputerProgram computerProgram)
        {
            var freeMemory = MaxMemorySize - CurrentMemorySize;
            if(freeMemory >= computerProgram.Size)
            {
                CurrentMemorySize += computerProgram.Size;
                computerPrograms.Add(computerProgram);

                return true;
            }

            return false;
        }
        public bool IsProgramInstalled(string programName)
        {
            if (computerPrograms.Find((match) => match.Name == programName) != null)
                return true;

            return false;
        }
        public bool RemoveProgram(string programName)
        {
            if (IsProgramInstalled(programName))
            {
                var removeProgram = FindProgram(programName);
                CurrentMemorySize -= removeProgram.Size;
                computerPrograms.Remove(FindProgram(programName));

                return true;
            }

            return false;
        }

    }
}
