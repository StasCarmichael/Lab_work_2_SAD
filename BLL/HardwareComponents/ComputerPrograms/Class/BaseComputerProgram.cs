using System;
using BLL.ComputerPrograms.Interface;

namespace BLL.ComputerPrograms.Class
{
    public class BaseComputerProgram : IComputerProgram
    {
        private const int minSize = 1;
        private const int maxSize = 102400;

        private int size;


        public BaseComputerProgram(string name, int size, bool needInternet, bool needHeadset, bool isGame)
        {
            Name = name;
            Size = size;
            NeedInternet = needInternet;
            NeedHeadset = needHeadset;
            IsGame = isGame;
        }


        public string Name { get; private set; }
        public int Size
        {
            get { return size; }
            private set
            {
                if (value >= minSize && value <= maxSize)
                    size = value;
                else { throw new ArgumentException("Недійсний формат розміру програми"); }
            }
        }


        public bool NeedInternet { get; private set; }
        public bool NeedHeadset { get; private set; }


        public bool IsGame { get; private set; }

    }
}
