using System;
using BLL.VideoCardComponent.Interface;

namespace BLL.VideoCardComponent.Class
{
    public class BaseVideoCard : IVideoCard
    {
        private const int minVideoMemory = 512;
        private const int maxVideoMemory = 20480;

        private const int minGPUpower = 10;
        private const int maxGPUpower = 100;


        private int amountVideoMemory;
        private int gpuPower;


        public BaseVideoCard(string modelName, int amountVideoMemory, int gpuPower)
        {
            ModelName = modelName;
            AmountVideoMemory = amountVideoMemory;
            GPUPower = gpuPower;
        }


        public string ModelName { get; private set; }


        public int AmountVideoMemory
        {
            get { return amountVideoMemory; }
            private set
            {
                if (value >= minVideoMemory && value <= maxVideoMemory)
                {
                    amountVideoMemory = value;
                }
                else { throw new ArgumentException("Недійсний формат об'єму пам'яті"); }
            }
        }
        public int GPUPower
        {
            get { return gpuPower; }
            private set
            {
                if (value >= minGPUpower && value <= maxGPUpower)
                {
                    gpuPower = value;
                }
                else { throw new ArgumentException("Недійсний формат потожності ядра відеокарти"); }
            }
        }


        public bool CanPlayGame()
        {
            if (AmountVideoMemory >= 4096 && GPUPower >= 50)
                return true;

            return false;
        }
        public bool CanWatchVideo()
        {
            if (AmountVideoMemory >= 2048 && GPUPower >= 30)
                return true;

            return false;
        }

    }
}
