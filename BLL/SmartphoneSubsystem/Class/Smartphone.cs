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

        #region PowerConsumption

        private readonly double powerConsumptionMusic = 1;
        private readonly double powerConsumptionInternet = 2;
        private readonly double powerConsumptionVideo = 3.125;
        private readonly double powerConsumptionProgram = 6.25;
        private readonly double powerConsumptionGame = 12.5;

        #endregion


        #region ServiceData

        bool internetConnection;
        bool electricalConnections;

        IBattery battery;
        ISoundHeadset soundHeadset;
        IVideoCard videoCard;
        IROMMemory ROMMemory;

        #endregion


        #region ServiceMethod

        private void NoConnection()
        {
            Logger?.Invoke(this, new LoggerArgs("В смартфона немає живлення, ні від мережі ні від акумулятора", ActionResult.Error));
            Error?.Invoke(this, new ErrorArgs("В смартфона немає живлення, ні від мережі ні від акумулятора", 80, null));
        }

        private void NoInternetConnection()
        {
            Logger?.Invoke(this, new LoggerArgs("Смартфон не підключений до інтернету", ActionResult.Error));
            Error?.Invoke(this, new ErrorArgs("Смартфон не підключений до інтернету", 20, null));
        }

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


        public bool InternetConnection
        {
            get { return internetConnection; }
            set
            {
                if (value == true)
                {
                    if (internetConnection == true)
                    {
                        Logger?.Invoke(this, new LoggerArgs("Інтернет вже підключений", ActionResult.Error));
                        Error?.Invoke(this, new ErrorArgs("Інтернет вже підключений", 1, null));
                    }
                    else
                    {
                        Logger?.Invoke(this, new LoggerArgs("Підключаємо комп'ютер до інтернету", ActionResult.Result));
                        Result?.Invoke(this, new ResultArgs("Підключаємо комп'ютер до інтернету"));
                    }

                    internetConnection = value;
                }
                else
                {
                    if (internetConnection == false)
                    {
                        Logger?.Invoke(this, new LoggerArgs("Інтернет вже відключений", ActionResult.Error));
                        Error?.Invoke(this, new ErrorArgs("Інтернет вже відключений", 1, null));
                    }
                    else
                    {
                        Logger?.Invoke(this, new LoggerArgs("Вимикаємо комп'ютер від інтернету", ActionResult.Result));
                        Result?.Invoke(this, new ResultArgs("Вимикаємо комп'ютер від інтернету"));
                    }

                    internetConnection = value;
                }
            }
        }
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
        //
        public bool SearchInternet(int time)
        {
            if (ElectricalConnections || !battery.IsDischarged)
            {
                if (InternetConnection)
                {
                    if (ElectricalConnections)
                    {
                        Logger?.Invoke(this, new LoggerArgs("Пошук в інтернті успішно виконаний", ActionResult.Result));
                        Result?.Invoke(this, new ResultArgs("Пошук в інтернті успішно виконаний"));

                        return true;
                    }
                    else if (battery.Unload((int)(time * powerConsumptionInternet)))
                    {
                        Logger?.Invoke(this, new LoggerArgs("Пошук в інтернті успішно виконаний", ActionResult.Result));
                        Result?.Invoke(this, new ResultArgs("Пошук в інтернті успішно виконаний"));

                        return true;
                    }
                    else
                    {
                        Logger?.Invoke(this, new LoggerArgs("Пошук в інтернті не виконаний докінця, бо розрядився акумулятор", ActionResult.Error));
                        Error?.Invoke(this, new ErrorArgs("Пошук в інтернті не виконаний докінця, бо розрядився акумулятор", 100, null));

                        return false;
                    }
                }
                else
                {
                    NoInternetConnection();
                    return false;
                }
            }
            else
            {
                NoConnection();
                return false;
            }
        }
        //
        public bool WatchVideo(int time)
        {
            if (ElectricalConnections || !battery.IsDischarged)
            {
                if (videoCard.CanWatchVideo())
                {
                    if (ElectricalConnections)
                    {
                        Logger?.Invoke(this, new LoggerArgs("Відео успішно переглянуте", ActionResult.Result));
                        Result?.Invoke(this, new ResultArgs("Відео успішно переглянуте"));

                        return true;
                    }
                    else if (battery.Unload((int)(time * powerConsumptionVideo)))
                    {
                        Logger?.Invoke(this, new LoggerArgs("Відео успішно переглянуте", ActionResult.Result));
                        Result?.Invoke(this, new ResultArgs("Відео успішно переглянуте"));

                        return true;
                    }
                    else
                    {
                        Logger?.Invoke(this, new LoggerArgs("Відео переглянути не до кінця, бо розрядився акумулятор", ActionResult.Error));
                        Error?.Invoke(this, new ErrorArgs("Відео переглянути не до кінця, бо розрядився акумулятор", 100, null));

                        return false;
                    }

                }
                else
                {
                    Logger?.Invoke(this, new LoggerArgs("Відеокарта не підтримує можливість перегляду відео", ActionResult.Error));
                    Error?.Invoke(this, new ErrorArgs("Відеокарта не підтримує можливість перегляду відео", 12, videoCard));

                    return false;
                }
            }
            else
            {
                NoConnection();
                return false;
            }
        }
        //
        public bool ListenMusic(int time)
        {
            if (ElectricalConnections || !battery.IsDischarged)
            {
                if (ElectricalConnections || battery.Unload((int)(time * powerConsumptionMusic)))
                {
                    if (soundHeadset.IsUserOnly())
                    {
                        Logger?.Invoke(this, new LoggerArgs("Музику слухаєте тільки користувач", ActionResult.Result));
                        Result?.Invoke(this, new ResultArgs("Музику слухаєте тільки користувач"));

                        return true;
                    }
                    else
                    {
                        Logger?.Invoke(this, new LoggerArgs("Музику слухають всі хто біля звукової гарнітури", ActionResult.Result));
                        Result?.Invoke(this, new ResultArgs("Музику слухають всі хто біля звукової гарнітури"));

                        return true;
                    }
                }
                else
                {
                    if (soundHeadset.IsUserOnly())
                    {
                        Logger?.Invoke(this, new LoggerArgs("Музику слухав тільки користувач, але не дослухав немає заряду акумулятора", ActionResult.Error));
                        Error?.Invoke(this, new ErrorArgs("Музику слухав тільки користувач, але не дослухав немає заряду акумулятора", 100, null));

                        return false;
                    }
                    else
                    {
                        Logger?.Invoke(this, new LoggerArgs("Музику слухали всі, але акумулятор розрядився", ActionResult.Error));
                        Error?.Invoke(this, new ErrorArgs("Музику слухали всі, але акумулятор розрядився", 100, null));

                        return false;
                    }
                }
            }
            else
            {
                NoConnection();
                return false;
            }
        }
        //
        public bool OpenProgram(string programName, int time)
        {
            if (ElectricalConnections || !battery.IsDischarged)
            {
                if (videoCard.CanPlayGame())
                {
                    if (ROMMemory.IsProgramInstalled(programName))
                    {
                        if (!ROMMemory.FindProgram(programName).IsGame)
                        {
                            if (ElectricalConnections || battery.Unload((int)(time * powerConsumptionProgram)))
                            {
                                if (ROMMemory.FindProgram(programName).NeedInternet)
                                {
                                    if (InternetConnection)
                                    {
                                        Logger?.Invoke(this, new LoggerArgs("Програма успішно запущена", ActionResult.Result));
                                        Result?.Invoke(this, new ResultArgs("Програма успішно запущена"));

                                        return true;
                                    }
                                    else
                                    {
                                        Logger?.Invoke(this, new LoggerArgs("Для даного застосування потрібен інтернет, але інтернету немає", ActionResult.Error));
                                        Error?.Invoke(this, new ErrorArgs("Для даного застосування потрібен інтернет, але інтернету немає", 12, null));

                                        return false;
                                    }
                                }
                                else
                                {
                                    Logger?.Invoke(this, new LoggerArgs("Програма успішно запущена", ActionResult.Result));
                                    Result?.Invoke(this, new ResultArgs("Програма успішно запущена"));

                                    return true;
                                }
                            }
                            else
                            {
                                Logger?.Invoke(this, new LoggerArgs("Програма закрилась не закінчив роботи, розрядився акумулятор", ActionResult.Error));
                                Error?.Invoke(this, new ErrorArgs("Програма закрилась не закінчив роботи, розрядився акумулятор", 100, null));

                                return false;
                            }
                        }
                        else
                        {
                            Logger?.Invoke(this, new LoggerArgs("Дане застосування є грою тому воно не запущене", ActionResult.Error));
                            Error?.Invoke(this, new ErrorArgs("Дане застосування є грою тому воно не запущене", 12, null));

                            return false;
                        }
                    }
                    else
                    {
                        Logger?.Invoke(this, new LoggerArgs("Дане застосування не встановлене", ActionResult.Error));
                        Error?.Invoke(this, new ErrorArgs("Дане застосування не встановлене", 12, null));

                        return false;
                    }
                }
                else
                {
                    Logger?.Invoke(this, new LoggerArgs("Відеокарта не підтримує можливість роботи з програмою", ActionResult.Error));
                    Error?.Invoke(this, new ErrorArgs("Відеокарта не підтримує можливість роботи з програмою", 18, videoCard));

                    return false;
                }
            }
            else
            {
                NoConnection();
                return false;
            }
        }
        //
        public bool StartGame(string gameName, int time)
        {
            if (ElectricalConnections || !battery.IsDischarged)
            {
                if (videoCard.CanPlayGame())
                {
                    if (ROMMemory.IsProgramInstalled(gameName))
                    {
                        if (ROMMemory.FindProgram(gameName).IsGame)
                        {
                            if (ElectricalConnections || battery.Unload((int)(time * powerConsumptionGame)))
                            {
                                if (ROMMemory.FindProgram(gameName).NeedInternet)
                                {
                                    if (InternetConnection)
                                    {
                                        Logger?.Invoke(this, new LoggerArgs("Гра успішно запущена", ActionResult.Result));
                                        Result?.Invoke(this, new ResultArgs("Гра успішно запущена"));

                                        return true;
                                    }
                                    else
                                    {
                                        Logger?.Invoke(this, new LoggerArgs("Для даної гри потрібен інтернет, але інтернету немає", ActionResult.Error));
                                        Error?.Invoke(this, new ErrorArgs("Для даної гри потрібен інтернет, але інтернету немає", 12, null));

                                        return false;
                                    }
                                }
                                else
                                {
                                    Logger?.Invoke(this, new LoggerArgs("Гра успішно запущена", ActionResult.Result));
                                    Result?.Invoke(this, new ResultArgs("Гра успішно запущена"));

                                    return true;
                                }
                            }
                            else
                            {
                                Logger?.Invoke(this, new LoggerArgs("Гра закрилась не закінчив роботи, розрядився акумулятор", ActionResult.Error));
                                Error?.Invoke(this, new ErrorArgs("Гра закрилась не закінчив роботи, розрядився акумулятор", 100, null));

                                return false;
                            }
                        }
                        else
                        {
                            Logger?.Invoke(this, new LoggerArgs("Дане застосування є програмою тому воно не запущене", ActionResult.Error));
                            Error?.Invoke(this, new ErrorArgs("Дане застосування є програмою тому воно не запущене", 12, null));

                            return false;
                        }
                    }
                    else
                    {
                        Logger?.Invoke(this, new LoggerArgs("Дане застосування не встановлене", ActionResult.Error));
                        Error?.Invoke(this, new ErrorArgs("Дане застосування не встановлене", 12, null));

                        return false;
                    }
                }
                else
                {
                    Logger?.Invoke(this, new LoggerArgs("Відеокарта не підтримує можливість запуску гри", ActionResult.Error));
                    Error?.Invoke(this, new ErrorArgs("Відеокарта не підтримує можливість запуску гри", 18, videoCard));

                    return false;
                }
            }
            else
            {
                NoConnection();
                return false;
            }
        }

        #endregion

    }
}
