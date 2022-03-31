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

            ISmartphone computer = GetWorkedSmartphone();

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

            ISmartphone computer = GetWorkedSmartphone();

            computer.ElectricalConnections = false;
            computer.ElectricalConnections = false;
            computer.ElectricalConnections = true;
            computer.ElectricalConnections = true;
            computer.ElectricalConnections = false; 
            computer.ElectricalConnections = true;
            computer.ElectricalConnections = false;

            Assert.Equal(result, computer.ElectricalConnections);
        }
    }
}
