using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using BLL.SmartphoneSubsystem.Class;
using BLL.SmartphoneSubsystem.Interface;

using BLL.ComputerSubsystem.Class;
using BLL.ComputerSubsystem.Interface;

using BLL.Handler.Console;

using BLL.MemoryComponent.Interface;
using BLL.MemoryComponent.Class;

using BLL.ComputerPrograms.Interface;
using BLL.ComputerPrograms.Class;

using BLL.SoundHeadsetComponent.Interface;
using BLL.SoundHeadsetComponent.Class;

using BLL.VideoCardComponent.Interface;
using BLL.VideoCardComponent.Class;

using BLL.BatteryComponent.Interface;
using BLL.BatteryComponent.Class;


namespace Project
{
    class Program
    {

        private static void ComputerTest()
        {
            ISoundHeadset soundHeadset = new Speaker("Asus DK");
            IVideoCard videoCard = new BaseVideoCard("Palit", 8192, 70);
            IROMMemory memory = new SSD(100000);

            IComputer computer = new Computer("MDK-100", soundHeadset, videoCard, memory);

            computer.Logger += LoggerHandler.Handler;
            computer.Error += ErrorHandler.Handler;
            computer.Result += ResultHandler.Handler;


            computer.ElectricalConnections = false;
            computer.ElectricalConnections = true;
            computer.InternetConnection = true;

            computer.ListenMusic(800);

            computer.SetSoundHeadset(new Headphones("Aurus"));

            computer.ListenMusic(800);
            computer.SearchInternet(100);

            computer.InternetConnection = true;

            computer.StartGame("CK3", 12);

            computer.InstallProgram(new BaseComputerProgram("CK3", 1000, true, false, true));
            computer.InstallProgram(new BaseComputerProgram("AutoCad", 8050, true, false, false));

            computer.StartGame("CK3", 400);

            computer.InternetConnection = false;
            computer.OpenProgram("AutoCad", 400);
            computer.InternetConnection = true;
            computer.OpenProgram("AutoCad", 400);
        }
        private static void SmartphoneTest()
        {
            IBattery battery = new MidleBattery();
            ISoundHeadset soundHeadset = new Speaker("Asus DK");
            IVideoCard videoCard = new BaseVideoCard("Palit", 8192, 70);
            IROMMemory memory = new SSD(100000);

            ISmartphone smartphone = new Smartphone("Nomi", soundHeadset, battery, videoCard, memory);

            smartphone.Logger += LoggerHandler.Handler;
            smartphone.Error += ErrorHandler.Handler;
            smartphone.Result += ResultHandler.Handler;


            smartphone.ElectricalConnections = true;
            smartphone.Charge();

            Console.WriteLine("\nПоточний заряд батарейки = " + smartphone.CurrentAmountCharge + "\n");

            smartphone.ElectricalConnections = false;
            smartphone.InternetConnection = true;

            smartphone.ListenMusic(250);

            Console.WriteLine("\nПоточний заряд батарейки = " + smartphone.CurrentAmountCharge + "\n");

            smartphone.SearchInternet(600);

            Console.WriteLine("\nПоточний заряд батарейки = " + smartphone.CurrentAmountCharge + "\n");


            smartphone.InstallProgram(new BaseComputerProgram("Nemo", 2000, true, false, true));
            smartphone.InstallProgram(new BaseComputerProgram("AutoCad", 8050, true, false, false));

            smartphone.StartGame("AutoCad", 200);
            smartphone.StartGame("Nemo", 200);

            Console.WriteLine("\nПоточний заряд батарейки = " + smartphone.CurrentAmountCharge + "\n");

            smartphone.SearchInternet(1000);

            Console.WriteLine("\nПоточний заряд батарейки = " + smartphone.CurrentAmountCharge + "\n");

            smartphone.Charge();
        }


        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;


            bool flag = true;

            while (flag)
            {
                Console.Clear();

                Console.WriteLine("Виберіть техніку яку потрібно симулювати.");
                Console.WriteLine("1 - Симулюємо роботу комп'ютера");
                Console.WriteLine("2 - Симулюємо роботу смартфону");
                Console.WriteLine("Будь яка інша клавіша завершити роботу");
                var keyCode = Console.ReadKey();
                Console.WriteLine();


                switch (keyCode.KeyChar)
                {
                    case ('1'):
                        {
                            ComputerTest();
                            break;
                        }
                    case ('2'):
                        {
                            SmartphoneTest();
                            break;
                        }
                    default:
                        flag = false;
                        break;
                }

                Console.WriteLine("Щоб продовжити натисніть будь-яку клавішу.");
                Console.ReadKey();
            }

        }
    }
}
