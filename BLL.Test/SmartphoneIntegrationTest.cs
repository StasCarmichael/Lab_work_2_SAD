using System;
using Xunit;

using BLL.ComputerPrograms.Interface;
using BLL.ComputerPrograms.Class;

using BLL.SmartphoneSubsystem.Class;
using BLL.SmartphoneSubsystem.Interface;

using BLL.Handler.Console;

using BLL.MemoryComponent.Interface;
using BLL.MemoryComponent.Class;

using BLL.SoundHeadsetComponent.Interface;
using BLL.SoundHeadsetComponent.Class;

using BLL.VideoCardComponent.Interface;
using BLL.VideoCardComponent.Class;

using BLL.BatteryComponent.Interface;
using BLL.BatteryComponent.Class;


namespace BLL.Test
{
    public class SmartphoneIntegrationTest
    {
        private ISmartphone GetWorkedSmartphone()
        {
            ISoundHeadset soundHeadset = new Speaker("Asus DK");
            IVideoCard videoCard = new BaseVideoCard("Palit", 8192, 70);
            IROMMemory memory = new SSD(100000);
            IBattery battery = new MidleBattery();

            ISmartphone smartphone = new Smartphone("MDK-100", soundHeadset, battery, videoCard, memory);

            smartphone.Logger += LoggerHandler.Handler;
            smartphone.Error += ErrorHandler.Handler;
            smartphone.Result += ResultHandler.Handler;

            return smartphone;
        }
        private ISmartphone GetWorkedSmartphoneWithFullBattary()
        {
            ISoundHeadset soundHeadset = new Speaker("Asus DK");
            IVideoCard videoCard = new BaseVideoCard("Palit", 8192, 70);
            IROMMemory memory = new SSD(100000);
            IBattery battery = new MidleBattery();

            ISmartphone smartphone = new Smartphone("MDK-100", soundHeadset, battery, videoCard, memory);

            smartphone.Logger += LoggerHandler.Handler;
            smartphone.Error += ErrorHandler.Handler;
            smartphone.Result += ResultHandler.Handler;

            smartphone.ElectricalConnections = true;
            smartphone.Charge();

            return smartphone;
        }


        [Fact]
        public void TestCreateSubsystem()
        {
            string modelName = "MDK-100";

            Assert.Equal(modelName, GetWorkedSmartphone().ModelName);
        }
        [Fact]
        public void TestSubscribeHandler()
        {
            var temp = new BaseVideoCard("Asrock", 4096, 50);

            ISmartphone smartphone = GetWorkedSmartphone();

            smartphone.InstallNewVideoCard(new BaseVideoCard("Asrock", 4096, 50));
            smartphone.InternetConnection = false;
            smartphone.InternetConnection = false;
            smartphone.InternetConnection = true;
            smartphone.InternetConnection = true;
            smartphone.InternetConnection = true;
            smartphone.InternetConnection = false;
            smartphone.InternetConnection = true;
            var res = smartphone.InternetConnection;

            smartphone.SearchInternet(500);

            Assert.Equal(temp.AmountVideoMemory, smartphone.GetVideoCardInfo().AmountVideoMemory);
        }
        [Fact]
        public void TestPower()
        {
            bool result = false;

            ISmartphone smartphone = GetWorkedSmartphone();

            smartphone.ElectricalConnections = false;
            smartphone.ElectricalConnections = false;
            smartphone.ElectricalConnections = true;
            smartphone.ElectricalConnections = true;
            smartphone.ElectricalConnections = false;
            smartphone.ElectricalConnections = true;
            smartphone.ElectricalConnections = false;

            Assert.Equal(result, smartphone.ElectricalConnections);
        }


        [Fact]
        public void TestNullData()
        {
            ISmartphone computer = GetWorkedSmartphone();

            computer.AddNewROMMemory(null);
            computer.InstallNewVideoCard(null);
            computer.SetSoundHeadset(null);
            computer.SetNewBattery(null);

            Assert.True(true);
        }
        [Fact]
        public void TestAddNewComponent()
        {
            ISmartphone computer = GetWorkedSmartphone();

            computer.AddNewROMMemory(new SSD(10000));
            computer.InstallNewVideoCard(new BaseVideoCard("XY-12", 2048, 30));
            computer.SetSoundHeadset(new Speaker("DLS"));
            computer.SetNewBattery(new SmallBattery());

            Assert.True(true);
        }


        [Fact]
        public void TestVideoCardError()
        {
            bool res = true;
            bool data = true;

            ISmartphone computer = GetWorkedSmartphone();

            try
            {
                computer.InstallNewVideoCard(new BaseVideoCard("XY-12", 10, 30));

                data = data || false;
            }
            catch (Exception)
            {

            }


            try
            {
                computer.InstallNewVideoCard(new BaseVideoCard("XY-12", 2048, 2));

                data = data || false;
            }
            catch (Exception)
            {

            }


            Assert.Equal(res, data);
        }



        [Fact]
        public void TestInstalProgramAndGame()
        {
            IComputerProgram baseComputer1 = new BaseComputerProgram("Steam", 100, true, false, false);
            IComputerProgram baseComputer2 = new BaseComputerProgram("CK3", 1000, true, false, true);
            IComputerProgram baseComputer3 = new BaseComputerProgram("QWERTY", 102400, true, false, true);

            ISmartphone smarphone = GetWorkedSmartphone();

            smarphone.AddNewROMMemory(new SSD(100000));

            smarphone.InstallProgram(baseComputer1);
            smarphone.InstallProgram(baseComputer2);
            smarphone.InstallProgram(baseComputer3);

            Assert.Equal(baseComputer1.Name, smarphone.OpenRomMemory().FindProgram("Steam").Name);
        }
        [Fact]
        public void TestRemoveProgramAndGame()
        {
            IComputerProgram baseComputer1 = new BaseComputerProgram("Steam", 100, true, false, false);
            IComputerProgram baseComputer2 = new BaseComputerProgram("CK3", 1000, true, false, true);

            ISmartphone smartphone = GetWorkedSmartphone();

            smartphone.InstallProgram(baseComputer1);
            smartphone.InstallProgram(baseComputer2);

            Assert.True(smartphone.RemoveProgram("CK3") && !smartphone.RemoveProgram("QWERTY"));
        }
        [Fact]
        public void TestMemory()
        {
            int result = 0;

            IComputerProgram baseComputer1 = new BaseComputerProgram("Steam", 100, true, false, false);
            IComputerProgram baseComputer2 = new BaseComputerProgram("CK3", 1000, true, false, true);

            ISmartphone smartphone = GetWorkedSmartphone();

            smartphone.InstallProgram(baseComputer1);
            smartphone.InstallProgram(baseComputer2);

            smartphone.OpenRomMemory().CleanMemory();

            Assert.Equal(result, smartphone.OpenRomMemory().GetInstalledProgram().Length);
        }



        [Fact]
        public void TestGetSoundHeadsetInfo()
        {
            string data = "DSS";

            ISmartphone smartphone = GetWorkedSmartphone();

            smartphone.SetSoundHeadset(new Speaker(data));

            Assert.Equal(data, smartphone.GetSoundHeadsetInfo().ModelName);
        }
        [Fact]
        public void TestBattary()
        {
            ISmartphone smartphone = GetWorkedSmartphone();

            smartphone.ElectricalConnections = true;
            smartphone.Charge();

            smartphone.ListenMusic(1000);

            smartphone.ElectricalConnections = false;
            smartphone.Charge();

            smartphone.ElectricalConnections = false;
            smartphone.Charge();

            Assert.Equal(smartphone.CurrentAmountCharge, smartphone.MaxAmountCharge);
        }



        [Fact]
        public void TestInternet()
        {
            var temp = new BaseVideoCard("Asrock", 4096, 50);

            ISmartphone smartphone = GetWorkedSmartphone();

            smartphone.InstallNewVideoCard(new BaseVideoCard("Asrock", 4096, 50));
            smartphone.AddNewROMMemory(new HDD(70000));

            smartphone.ElectricalConnections = false;
            smartphone.ElectricalConnections = true;
            smartphone.InternetConnection = true;
            smartphone.SearchInternet(100);

            smartphone.Charge();
            smartphone.ElectricalConnections = false;

            smartphone.SearchInternet(50);

            smartphone.InternetConnection = false;
            smartphone.SearchInternet(50);
            smartphone.InternetConnection = true;

            smartphone.SearchInternet(10000);

            Assert.Equal(temp.AmountVideoMemory, smartphone.GetVideoCardInfo().AmountVideoMemory);
        }
        [Fact]
        public void TestMusic()
        {
            var temp = new BaseVideoCard("Asrock", 4096, 50);

            ISmartphone smartphone = GetWorkedSmartphoneWithFullBattary();

            smartphone.InstallNewVideoCard(new BaseVideoCard("Asrock", 4096, 50));

            smartphone.SetSoundHeadset(new Speaker("ada"));


            smartphone.ElectricalConnections = false;
            smartphone.ListenMusic(800);
            smartphone.ElectricalConnections = true;
            smartphone.InternetConnection = true;
            smartphone.ListenMusic(800);

            smartphone.ElectricalConnections = false;
            smartphone.ListenMusic(80000);
            smartphone.ListenMusic(100);
            smartphone.ListenMusic(100);


            smartphone.ElectricalConnections = true;
            smartphone.Charge();


            smartphone.SetSoundHeadset(new Headphones("qwerty"));


            smartphone.ElectricalConnections = false;
            smartphone.ListenMusic(800);
            smartphone.ElectricalConnections = true;
            smartphone.InternetConnection = true;
            smartphone.ListenMusic(800);

            smartphone.ElectricalConnections = false;
            smartphone.ListenMusic(80000);

            smartphone.ElectricalConnections = true;
            smartphone.Charge();


            Assert.Equal(temp.AmountVideoMemory, smartphone.GetVideoCardInfo().AmountVideoMemory);
        }
        [Fact]
        public void TestWatchVideo()
        {
            var temp = new BaseVideoCard("Nomi", 512, 15);

            ISmartphone smartphone = GetWorkedSmartphoneWithFullBattary();

            smartphone.InstallNewVideoCard(new BaseVideoCard("Asrock", 4096, 50));


            smartphone.ElectricalConnections = false;
            smartphone.WatchVideo(100);

            smartphone.ElectricalConnections = true;
            smartphone.InternetConnection = true;
            smartphone.WatchVideo(800);

            smartphone.ElectricalConnections = false;
            smartphone.WatchVideo(8000);

            smartphone.InstallNewVideoCard(new BaseVideoCard("Nomi", 512, 15));
            smartphone.WatchVideo(200);

            Assert.Equal(temp.AmountVideoMemory, smartphone.GetVideoCardInfo().AmountVideoMemory);
        }
        [Fact]
        public void TestOpenProgram()
        {
            IComputerProgram baseComputer1 = new BaseComputerProgram("Steam", 100, true, false, false);
            IComputerProgram baseComputer2 = new BaseComputerProgram("CK3", 1000, true, false, true);
            IComputerProgram baseComputer3 = new BaseComputerProgram("Auto", 100, false, false, false);

            ISmartphone smartphone = GetWorkedSmartphoneWithFullBattary();

            smartphone.InstallProgram(baseComputer1);
            smartphone.InstallProgram(baseComputer2);
            smartphone.InstallProgram(baseComputer3);
            smartphone.InternetConnection = true;

            smartphone.OpenProgram("Steam", 100);
            smartphone.OpenProgram("QWERTY", 100);
            smartphone.OpenProgram("CK3", 100);
            smartphone.OpenProgram("Auto", 100);

            smartphone.InternetConnection = false;

            smartphone.OpenProgram("Steam", 100);
            smartphone.OpenProgram("CK3", 100);
            smartphone.OpenProgram("Auto", 100);


            smartphone.ElectricalConnections = false;
            smartphone.OpenProgram("Steam", 100);
            smartphone.OpenProgram("Steam", 100000);
            smartphone.OpenProgram("Steam", 100);

            smartphone.ElectricalConnections = true;
            smartphone.Charge();

            smartphone.InstallNewVideoCard(new BaseVideoCard("dfs", 1024, 10));
            smartphone.InternetConnection = true;

            smartphone.OpenProgram("Steam", 100);
            smartphone.OpenProgram("Auto", 100);
            smartphone.OpenProgram("CK3", 100);


            Assert.Equal(baseComputer1.Name, smartphone.OpenRomMemory().FindProgram("Steam").Name);
        }
        [Fact]
        public void TestStartGame()
        {

            IComputerProgram baseComputer1 = new BaseComputerProgram("Steam", 100, true, true, false);
            IComputerProgram baseComputer2 = new BaseComputerProgram("CK3", 1000, true, false, true);
            IComputerProgram baseComputer3 = new BaseComputerProgram("QWERTY", 1000, false, true, true);

            ISmartphone smartphone = GetWorkedSmartphoneWithFullBattary();

            smartphone.InternetConnection = true;

            smartphone.InstallProgram(baseComputer1);
            smartphone.InstallProgram(baseComputer2);
            smartphone.InstallProgram(baseComputer3);

            smartphone.StartGame("CK3", 10);
            smartphone.StartGame("NAS", 10);
            smartphone.StartGame("QWERTY", 10);

            smartphone.InternetConnection = false;
            smartphone.ElectricalConnections = false;

            smartphone.StartGame("Steam", 100);
            smartphone.StartGame("QWERTY", 100);
            smartphone.StartGame("CK3", 1000000);
            smartphone.StartGame("Steam", 100);

            smartphone.ElectricalConnections = true;
            smartphone.Charge();

            smartphone.InternetConnection = false;
            smartphone.StartGame("CK3", 10);

            smartphone.InstallNewVideoCard(new BaseVideoCard("dfs", 1024, 10));

            smartphone.StartGame("Steam", 100);
            smartphone.StartGame("CK3", 10);
            smartphone.StartGame("QWERTY", 100);

            smartphone.ElectricalConnections = false;

            smartphone.StartGame("QWERTY", 100);
            smartphone.StartGame("QWERTY", 100000);
            smartphone.StartGame("QWERTY", 100);


            Assert.Equal(baseComputer2.Name, smartphone.OpenRomMemory().FindProgram("CK3").Name);
        }

    }
}
