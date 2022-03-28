using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.BatteryComponent.Interface;
using BLL.ComputerPrograms.Interface;
using BLL.ComputerSubsystem.Interface;
using BLL.MemoryComponent.Interface;
using BLL.SoundHeadsetComponent.Interface;
using BLL.VideoCardComponent.Interface;

namespace BLL.ComputerSubsystem.Class
{
    public class Computer : IComputer
    {

        public int MaxAmountCharge => throw new NotImplementedException();

        public int CurrentAmountCharge => throw new NotImplementedException();



        public void Charge()
        {
            throw new NotImplementedException();
        }

        public IVideoCard GetVideoCardInfo()
        {
            throw new NotImplementedException();
        }
        public IROMMemory OpenRomMemory()
        {
            throw new NotImplementedException();
        }



        public bool InstallProgram(IComputerProgram computerProgram)
        {
            throw new NotImplementedException();
        }
        public bool RemoveProgram(string programName)
        {
            throw new NotImplementedException();
        }






        public bool InstallNewVideoCard(IVideoCard videoCard)
        {
            throw new NotImplementedException();
        }
        public bool SetNewBattery(IBattery battery)
        {
            throw new NotImplementedException();
        }
        public bool SetSoundHeadset(ISoundHeadset soundHeadset)
        {
            throw new NotImplementedException();
        }
        public bool AddNewROMMemory(IROMMemory memory)
        {
            throw new NotImplementedException();
        }



        public bool SearchInternet(int time)
        {
            throw new NotImplementedException();
        }
        public bool StartGame(string gameName, int time)
        {
            throw new NotImplementedException();
        }
        public bool WatchVideo(int time)
        {
            throw new NotImplementedException();
        }
        public bool ListenMusic(int time)
        {
            throw new NotImplementedException();
        }

        public bool OpenProgram(string programName, int time)
        {
            throw new NotImplementedException();
        }

    }
}
