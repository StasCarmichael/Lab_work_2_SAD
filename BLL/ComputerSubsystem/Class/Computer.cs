using System;

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
        #region ServiceData

        bool electricalConnections;
        bool internetConnection;

        ISoundHeadset soundHeadset;
        IVideoCard videoCard;
        IROMMemory ROMMemory;

        #endregion


        #region ServiceMethod

        private void NoConnection()
        {
            Logger?.Invoke(this, new LoggerArgs("Комп'ютер не підключений до мережі", ActionResult.Error));
            Error?.Invoke(this, new ErrorArgs("Комп'ютер не підключений до мережі", 50, null));
        }

        #endregion


        //Event
        public event EventHandler<LoggerArgs> Logger;
        public event EventHandler<ErrorArgs> Error;
        public event EventHandler<ResultArgs> Result;


        //ctor
        public Computer(string modelName, ISoundHeadset soundHeadset, IVideoCard videoCard, IROMMemory ROMMemory)
        {
            ModelName = modelName;

            if (!SetSoundHeadset(soundHeadset)) { throw new Exception("Не встановлена гарніткра"); }
            if (!InstallNewVideoCard(videoCard)) { throw new Exception("Не встановлена відіокарта"); }
            if (!AddNewROMMemory(ROMMemory)) { throw new Exception("Не додана ПЗП (пам'ять)"); }

            ElectricalConnections = true;
        }

        //ready
        public string ModelName { get; private set; }

        //ready
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

        //
        #region GetHardware
        //ready
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

        //
        #region WorkWithProgram
        //ready
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
                Error?.Invoke(this, new ErrorArgs("Дана програма не встановлена", 6, ROMMemory.FindProgram(programName)));

                return false;
            }

        }

        #endregion

        //
        #region InstallerHardware
        //ready
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

        #endregion

        //
        #region Function

        public bool SearchInternet(int time)
        {
            if (ElectricalConnections)
            {
                if (InternetConnection)
                {
                    Logger?.Invoke(this, new LoggerArgs("Пошук в інтернті успішно виконаний", ActionResult.Result));
                    Result?.Invoke(this, new ResultArgs("Пошук в інтернті успішно виконаний"));

                    return true;
                }
                else
                {
                    Logger?.Invoke(this, new LoggerArgs("Комп'ютер не підключений до інтернету", ActionResult.Error));
                    Error?.Invoke(this, new ErrorArgs("Комп'ютер не підключений до інтернету", 20, null));

                    return false;
                }
            }
            else
            {
                NoConnection();
                return false;
            }
        }
        public bool WatchVideo(int time)
        {
            if (ElectricalConnections)
            {
                if (videoCard.CanWatchVideo())
                {
                    Logger?.Invoke(this, new LoggerArgs("Відео успішно переглянуте", ActionResult.Result));
                    Result?.Invoke(this, new ResultArgs("Відео успішно переглянуте"));

                    return true;
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
        public bool ListenMusic(int time)
        {
            if (ElectricalConnections)
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
                NoConnection();
                return false;
            }
        }
        public bool OpenProgram(string programName, int time)
        {
            if (ElectricalConnections)
            {
                if (videoCard.CanPlayGame())
                {
                    if (ROMMemory.IsProgramInstalled(programName))
                    {
                        if (!ROMMemory.FindProgram(programName).IsGame)
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
        public bool StartGame(string gameName, int time)
        {
            if (ElectricalConnections)
            {
                if (videoCard.CanPlayGame())
                {
                    if (ROMMemory.IsProgramInstalled(gameName))
                    {
                        if (ROMMemory.FindProgram(gameName).IsGame)
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
