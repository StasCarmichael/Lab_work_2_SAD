using System;
using BLL.EventArgs;

namespace BLL.Handler.Console
{
    public static class LoggerHandler
    {
        public static void Handler(object obj, LoggerArgs loggerArgs)
        {
            System.Console.ResetColor();


            System.Console.WriteLine("----------------------------------------------------");

            System.Console.WriteLine("Object = " + obj.ToString());
            System.Console.WriteLine("Logger Info = " + loggerArgs.Info);
            System.Console.WriteLine("Action = " + loggerArgs.Action.ToString());

            System.Console.WriteLine("----------------------------------------------------");
        }
    }
}
