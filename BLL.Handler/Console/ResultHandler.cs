using System;
using BLL.EventArgs;

namespace BLL.Handler.Console
{
    public static class ResultHandler
    {
        public static void Handler(object obj, ResultArgs resultArgs)
        {
            System.Console.ForegroundColor = ConsoleColor.Green;


            System.Console.WriteLine("====================================================");

            System.Console.WriteLine("Object = " + obj.ToString());
            System.Console.WriteLine("Result Info = " + resultArgs.Info);

            System.Console.WriteLine("====================================================");


            System.Console.ResetColor();
        }
    }
}
