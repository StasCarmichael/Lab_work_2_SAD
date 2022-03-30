using System;
using Xunit;

using BLL.ComputerPrograms.Interface;
using BLL.ComputerPrograms.Class;

using BLL.ComputerSubsystem.Class;
using BLL.ComputerSubsystem.Interface;

using BLL.Handler.Console;

using BLL.MemoryComponent.Interface;
using BLL.MemoryComponent.Class;

using BLL.SoundHeadsetComponent.Interface;
using BLL.SoundHeadsetComponent.Class;

using BLL.VideoCardComponent.Interface;
using BLL.VideoCardComponent.Class;


namespace BLL.Test
{
    public class ComputerIntegrationTest
    {
        private IComputer GetWorkedComputer()
        {
            ISoundHeadset soundHeadset = new Speaker("Asus DK");
            IVideoCard videoCard = new BaseVideoCard("Palit", 8192, 70);
            IROMMemory memory = new SSD(100000);

            IComputer computer = new Computer("MDK-100", soundHeadset, videoCard, memory);

            computer.Logger += LoggerHandler.Handler;
            computer.Error += ErrorHandler.Handler;
            computer.Result += ResultHandler.Handler;

            return computer;
        }



        [Fact]
        public void TestCreateSubsystem()
        {
            string modelName = "MDK-100";

            ISoundHeadset soundHeadset = new Speaker("Asus DK");
            IVideoCard videoCard = new BaseVideoCard("Palit", 8192, 70);
            IROMMemory memory = new SSD(100000);

            IComputer computer = new Computer("MDK-100", soundHeadset, videoCard, memory);

            Assert.Equal(modelName, computer.ModelName);
        }
        [Fact]
        public void TestSubscribeHandler()
        {
            var temp = new BaseVideoCard("Asrock", 4096, 50);

            IComputer computer = GetWorkedComputer();

            computer.InstallNewVideoCard(new BaseVideoCard("Asrock", 4096, 50));
            computer.InternetConnection = false;
            computer.InternetConnection = false;
            computer.InternetConnection = true;
            computer.InternetConnection = true;
            computer.InternetConnection = true;
            computer.InternetConnection = false;
            computer.InternetConnection = true;
            computer.SearchInternet(500);

            Assert.Equal(temp.AmountVideoMemory, computer.GetVideoCardInfo().AmountVideoMemory);
        }
        [Fact]
        public void TestPower()
        {
            bool result = false;

            IComputer computer = GetWorkedComputer();

            computer.ElectricalConnections = false;
            computer.ElectricalConnections = false;
            computer.ElectricalConnections = true;
            computer.ElectricalConnections = true;
            computer.ElectricalConnections = false;

            Assert.Equal(result, computer.ElectricalConnections);
        }


        [Fact]
        public void TestNullData()
        {
            IComputer computer = GetWorkedComputer();

            computer.AddNewROMMemory(null);
            computer.InstallNewVideoCard(null);
            computer.SetSoundHeadset(null);

            Assert.True(true);
        }


        [Fact]
        public void TestInternet()
        {
            var temp = new BaseVideoCard("Asrock", 4096, 50);

            IComputer computer = GetWorkedComputer();

            computer.InstallNewVideoCard(new BaseVideoCard("Asrock", 4096, 50));
            computer.AddNewROMMemory(new HDD(70000));

            computer.ElectricalConnections = false;
            computer.ElectricalConnections = true;
            computer.InternetConnection = true;
            computer.SearchInternet(800);

            Assert.Equal(temp.AmountVideoMemory, computer.GetVideoCardInfo().AmountVideoMemory);
        }
        [Fact]
        public void TestMusicSpeker()
        {
            var temp = new BaseVideoCard("Asrock", 4096, 50);

            IComputer computer = GetWorkedComputer();

            computer.InstallNewVideoCard(new BaseVideoCard("Asrock", 4096, 50));
            computer.AddNewROMMemory(new HDD(70000));

            computer.ElectricalConnections = false;
            computer.ElectricalConnections = true;
            computer.InternetConnection = true;
            computer.ListenMusic(800);

            Assert.Equal(temp.AmountVideoMemory, computer.GetVideoCardInfo().AmountVideoMemory);
        }
        [Fact]
        public void TestMusicHeadphones()
        {
            var temp = new BaseVideoCard("Asrock", 4096, 50);

            IComputer computer = GetWorkedComputer();

            computer.InstallNewVideoCard(new BaseVideoCard("Asrock", 4096, 50));
            computer.AddNewROMMemory(new HDD(70000));
            computer.SetSoundHeadset(new Headphones("Orion"));

            computer.ElectricalConnections = false;
            computer.ElectricalConnections = true;
            computer.InternetConnection = true;
            computer.ListenMusic(800);

            Assert.Equal(temp.AmountVideoMemory, computer.GetVideoCardInfo().AmountVideoMemory);
        }
        [Fact]
        public void TestWatchVideo()
        {
            var temp = new BaseVideoCard("Nomi", 512, 15);

            IComputer computer = GetWorkedComputer();

            computer.InstallNewVideoCard(new BaseVideoCard("Asrock", 4096, 50));
            computer.AddNewROMMemory(new HDD(70000));

            computer.ElectricalConnections = false;

            computer.WatchVideo(800);

            computer.ElectricalConnections = true;
            computer.InternetConnection = true;
            computer.WatchVideo(800);

            computer.InstallNewVideoCard(new BaseVideoCard("Nomi", 512, 15));
            computer.WatchVideo(200);

            Assert.Equal(temp.AmountVideoMemory, computer.GetVideoCardInfo().AmountVideoMemory);
        }



        [Fact]
        public void TestInstalProgramAndGame()
        {
            IComputerProgram baseComputer1 = new BaseComputerProgram("Steam", 100, true, false, false);
            IComputerProgram baseComputer2 = new BaseComputerProgram("CK3", 1000, true, false, true);
            IComputerProgram baseComputer3 = new BaseComputerProgram("QWERTY", 102400, true, false, true);

            IComputer computer = GetWorkedComputer();

            computer.InstallProgram(baseComputer1);
            computer.InstallProgram(baseComputer2);
            computer.InstallProgram(baseComputer3);

            Assert.Equal(baseComputer1.Name, computer.OpenRomMemory().FindProgram("Steam").Name);
        }
        [Fact]
        public void TestRemoveProgramAndGame()
        {
            IComputerProgram baseComputer1 = new BaseComputerProgram("Steam", 100, true, false, false);
            IComputerProgram baseComputer2 = new BaseComputerProgram("CK3", 1000, true, false, true);

            IComputer computer = GetWorkedComputer();

            computer.InstallProgram(baseComputer1);
            computer.InstallProgram(baseComputer2);

            Assert.True(computer.RemoveProgram("CK3") && !computer.RemoveProgram("QWERTY"));
        }



        [Fact]
        public void TestOpenProgram()
        {
            IComputerProgram baseComputer1 = new BaseComputerProgram("Steam", 100, true, false, false);
            IComputerProgram baseComputer2 = new BaseComputerProgram("CK3", 1000, true, false, true);
            IComputerProgram baseComputer3 = new BaseComputerProgram("Auto", 100, false, false, false);

            IComputer computer = GetWorkedComputer();

            computer.InstallProgram(baseComputer1);
            computer.InstallProgram(baseComputer2);
            computer.InstallProgram(baseComputer3);
            computer.InternetConnection = true;

            computer.OpenProgram("Steam", 100);
            computer.OpenProgram("QWERTY", 100);
            computer.OpenProgram("CK3", 100);
            computer.OpenProgram("Auto", 100);

            computer.InternetConnection = false;

            computer.OpenProgram("Steam", 100);
            computer.OpenProgram("CK3", 100);
            computer.OpenProgram("Auto", 100);


            computer.ElectricalConnections = false;
            computer.OpenProgram("Steam", 100);
            computer.ElectricalConnections = true;


            computer.InstallNewVideoCard(new BaseVideoCard("dfs", 1024, 10));
            computer.InternetConnection = true;

            computer.OpenProgram("Steam", 100);
            computer.OpenProgram("Auto", 100);
            computer.OpenProgram("CK3", 100);


            Assert.Equal(baseComputer1.Name, computer.OpenRomMemory().FindProgram("Steam").Name);
        }
        [Fact]
        public void TestStartGame()
        {

            IComputerProgram baseComputer1 = new BaseComputerProgram("Steam", 100, true, true, false);
            IComputerProgram baseComputer2 = new BaseComputerProgram("CK3", 1000, true, false, true);
            IComputerProgram baseComputer3 = new BaseComputerProgram("CNN", 1000, false, true, true);

            IComputer computer = GetWorkedComputer();

            computer.InternetConnection = true;

            computer.InstallProgram(baseComputer1);
            computer.InstallProgram(baseComputer2);
            computer.InstallProgram(baseComputer3);

            computer.StartGame("CK3", 100);
            computer.StartGame("NAS", 100);
            computer.StartGame("CNN", 100);

            computer.InternetConnection = false;

            computer.StartGame("Steam", 100);

            computer.InstallNewVideoCard(new BaseVideoCard("dfs", 1024, 10));
            computer.InternetConnection = true;

            computer.StartGame("Steam", 100);

            computer.StartGame("CK3", 100);

            computer.ElectricalConnections = false;

            computer.StartGame("QWERTY", 100);


            Assert.Equal(baseComputer2.Name, computer.OpenRomMemory().FindProgram("CK3").Name);
        }
    }
}
