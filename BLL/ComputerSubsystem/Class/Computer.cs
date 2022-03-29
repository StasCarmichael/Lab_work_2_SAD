using System;
using System.Collections.Generic;
using System.Linq;
using BLL.ComputerPrograms.Interface;
using BLL.ComputerSubsystem.Interface;
using BLL.EventArgs;
using BLL.MemoryComponent.Interface;
using BLL.SoundHeadsetComponent.Interface;
using BLL.VideoCardComponent.Interface;


namespace BLL.ComputerSubsystem.Class
{
    public class Computer : IComputer
    {
        ISoundHeadset soundHeadset;
        IVideoCard videoCard;
        IROMMemory ROMMemory;


        public event EventHandler<LoggerArgs> Logger;
        public event EventHandler<ErrorArgs> Error;
        public event EventHandler<ResultArgs> Result;


        public Computer(string modelName, ISoundHeadset soundHeadset, IVideoCard videoCard, IROMMemory ROMMemory)
        {
            ModelName = modelName;
            this.soundHeadset = soundHeadset;
            this.videoCard = videoCard;
            this.ROMMemory = ROMMemory;

            ElectricalConnections = true;
        }


        public string ModelName { get; private set; }


        public bool ElectricalConnections { get; set; }


        public IVideoCard GetVideoCardInfo() { return videoCard; }
        public IROMMemory OpenRomMemory() { return ROMMemory; }



        public bool InstallProgram(IComputerProgram computerProgram)
        {
            return ROMMemory?.InstallProgram(computerProgram) ?? throw new Exception();
        }
        public bool RemoveProgram(string programName)
        {
            return ROMMemory?.RemoveProgram(programName) ?? throw new Exception();
        }



        public bool InstallNewVideoCard(IVideoCard videoCard)
        {
            if (videoCard != null)
            {
                this.videoCard = videoCard;
                return true;
            }

            return false;
        }
        public bool SetSoundHeadset(ISoundHeadset soundHeadset)
        {
            if (soundHeadset != null)
            {
                this.soundHeadset = soundHeadset;
                return true;
            }

            return false;
        }
        public bool AddNewROMMemory(IROMMemory memory)
        {
            if (memory != null)
            {
                ROMMemory = memory;
                return true;
            }

            return false;
        }



        public bool SearchInternet(int time)
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
        public bool StartGame(string gameName, int time)
        {
            throw new NotImplementedException();
        }

    }
}
