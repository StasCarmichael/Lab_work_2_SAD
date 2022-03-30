using System;
using System.Collections.Generic;
using System.Linq;

using BLL.BatteryComponent.Interface;
using BLL.ComputerPrograms.Interface;
using BLL.EventArgs;
using BLL.MemoryComponent.Interface;
using BLL.SmartphoneSubsystem.Interface;
using BLL.SoundHeadsetComponent.Interface;
using BLL.VideoCardComponent.Interface;

namespace BLL.SmartphoneSubsystem.Class
{
    public class Smartphone : ISmartphone
    {
        #region ServiceData

        bool electricalConnections;

        IBattery battery;
        ISoundHeadset soundHeadset;
        IVideoCard videoCard;
        IROMMemory ROMMemory;

        #endregion


        //Event
        public event EventHandler<LoggerArgs> Logger;
        public event EventHandler<ErrorArgs> Error;
        public event EventHandler<ResultArgs> Result;


        //Ctor
        public Smartphone(string modelName, ISoundHeadset soundHeadset, IBattery battery, IVideoCard videoCard, IROMMemory ROMMemory)
        {
            ModelName = modelName;

            if (!SetSoundHeadset(soundHeadset)) { throw new Exception("Не встановлена гарніткра"); }
            if (!InstallNewVideoCard(videoCard)) { throw new Exception("Не встановлена відіокарта"); }
            if (!AddNewROMMemory(ROMMemory)) { throw new Exception("Не додана ПЗП (пам'ять)"); }
            if (!SetNewBattery(battery)) { throw new Exception("Не додана батарейка"); }
        }


        public string ModelName { get; private set; }


        public int MaxAmountCharge { get { return battery.MaxAmountCharge; } }
        public int CurrentAmountCharge { get { return battery.CurrentAmountCharge; } }


        public bool ElectricalConnections
        {
            get { return electricalConnections; }
            set
            {
                if (value == true)
                {
                    if (electricalConnections == true)
                    {
                        Logger?.Invoke(this, new LoggerArgs("Мережа вже включена", ActionResult.Error));
                        Error?.Invoke(this, new ErrorArgs("Мережа вже включена", 1, null));
                    }
                    else
                    {
                        Logger?.Invoke(this, new LoggerArgs("Вмикаємо комп'ютер в мережу", ActionResult.Result));
                        Result?.Invoke(this, new ResultArgs("Вмикаємо комп'ютер в мережу"));
                    }

                    electricalConnections = value;
                }
                else
                {
                    if (electricalConnections == false)
                    {
                        Logger?.Invoke(this, new LoggerArgs("Мережа вже вимкнена", ActionResult.Error));
                        Error?.Invoke(this, new ErrorArgs("Мережа вже вимкнена", 1, null));
                    }
                    else
                    {
                        Logger?.Invoke(this, new LoggerArgs("Вимикаємо комп'ютер з мережі", ActionResult.Result));
                        Result?.Invoke(this, new ResultArgs("Вимикаємо комп'ютер з мережі"));
                    }

                    electricalConnections = value;
                }
            }
        }


        public bool Charge()
        {
            if (ElectricalConnections)
            {
                battery.Charge();

                Logger?.Invoke(this, new LoggerArgs("Батарейка успішно заряжена", ActionResult.Result));
                Result?.Invoke(this, new ResultArgs("Батарейка успішно заряжена"));

                return true;
            }
            else
            {
                Logger?.Invoke(this, new LoggerArgs("Підключення до мережі немає, зарядити батарейку неможливо", ActionResult.Error));
                Error?.Invoke(this, new ErrorArgs("Підключення до мережі немає, зарядити батарейку неможливо", 16, null));
                return false;
            }
        }


        #region GetHardware

        public IVideoCard GetVideoCardInfo()
        {
            Logger?.Invoke(this, new LoggerArgs("Отримати дані про відео карту", ActionResult.Message));
            return videoCard;
        }
        public IROMMemory OpenRomMemory()
        {
            Logger?.Invoke(this, new LoggerArgs("Отримати дані про ПЗП(пам'ять)", ActionResult.Message));
            return ROMMemory;
        }
        public ISoundHeadset GetSoundHeadsetInfo()
        {
            Logger?.Invoke(this, new LoggerArgs("Отримати дані про звукову гарнітуру", ActionResult.Message));
            return soundHeadset;
        }

        #endregion


        #region WorkWithProgram

        public bool InstallProgram(IComputerProgram computerProgram)
        {
            var result = ROMMemory.InstallProgram(computerProgram);

            if (result)
            {
                Logger?.Invoke(this, new LoggerArgs("Програма успішно встановлена", ActionResult.Result));
                Result?.Invoke(this, new ResultArgs("Програма успішно встановлена"));

                return true;
            }
            else
            {
                Logger?.Invoke(this, new LoggerArgs("Програма НЕ встановлена", ActionResult.Error));
                Error?.Invoke(this, new ErrorArgs("Програма НЕ встановлена", 10, computerProgram));

                return false;
            }
        }
        public bool RemoveProgram(string programName)
        {
            var result = ROMMemory.RemoveProgram(programName);

            if (result)
            {
                Logger?.Invoke(this, new LoggerArgs("Програма успішно видалена", ActionResult.Result));
                Result?.Invoke(this, new ResultArgs("Програма успішно видалена"));

                return true;
            }
            else
            {
                Logger?.Invoke(this, new LoggerArgs("Дана програма не встановлена", ActionResult.Error));
                Error?.Invoke(this, new ErrorArgs("Дана програма не встановлена", 7, ROMMemory.FindProgram(programName)));

                return false;
            }
        }

        #endregion


        #region InstallerHardware            

        public bool InstallNewVideoCard(IVideoCard videoCard)
        {
            if (videoCard != null)
            {
                this.videoCard = videoCard;

                Logger?.Invoke(this, new LoggerArgs("Відеокарта успішно встановлена", ActionResult.Result));
                Result?.Invoke(this, new ResultArgs("Відеокарта успішно встановлена"));

                return true;
            }

            Logger?.Invoke(this, new LoggerArgs("Неможливо встановити відео карту", ActionResult.Error));
            Error?.Invoke(this, new ErrorArgs("Неможливо встановити відео карту", 10, videoCard));
            return false;
        }
        public bool SetSoundHeadset(ISoundHeadset soundHeadset)
        {
            if (soundHeadset != null)
            {
                this.soundHeadset = soundHeadset;

                Logger?.Invoke(this, new LoggerArgs("Звукова гарнітура успішно встановлена", ActionResult.Result));
                Result?.Invoke(this, new ResultArgs("Звукова гарнітура успішно встановлена"));

                return true;
            }

            Logger?.Invoke(this, new LoggerArgs("Неможливо встановити звукову гарнітуру", ActionResult.Error));
            Error?.Invoke(this, new ErrorArgs("Неможливо встановити звукову гарнітуру", 8, soundHeadset));
            return false;
        }
        public bool AddNewROMMemory(IROMMemory memory)
        {
            if (memory != null)
            {
                ROMMemory = memory;

                Logger?.Invoke(this, new LoggerArgs("ПЗП успішно встановлена", ActionResult.Result));
                Result?.Invoke(this, new ResultArgs("ПЗП успішно встановлена"));

                return true;
            }

            Logger?.Invoke(this, new LoggerArgs("Неможливо встановити ПЗП", ActionResult.Error));
            Error?.Invoke(this, new ErrorArgs("Неможливо встановити ПЗП", 50, memory));
            return false;
        }
        public bool SetNewBattery(IBattery battery)
        {
            if (battery != null)
            {
                this.battery = battery;

                Logger?.Invoke(this, new LoggerArgs("Батарейка успішно встановлена", ActionResult.Result));
                Result?.Invoke(this, new ResultArgs("Батарейка успішно встановлена"));

                return true;
            }

            Logger?.Invoke(this, new LoggerArgs("Неможливо встановити батарейку", ActionResult.Error));
            Error?.Invoke(this, new ErrorArgs("Неможливо встановити батарейку", 20, battery));
            return false;
        }

        #endregion


        #region Function

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

        #endregion

    }
}
