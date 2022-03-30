using System;
using BLL.EventArgs;

namespace BLL.Handler.Console
{
    public static class ErrorHandler
    {
        public static void Handler(object obj, ErrorArgs errorArgs)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;


            System.Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++");

            System.Console.WriteLine("Object = " + obj.ToString());
            System.Console.WriteLine("Error Info = " + errorArgs.Info);
            System.Console.WriteLine("Number Error = " + errorArgs.ErrorCriticality);
            System.Console.WriteLine("Who caused error = " + errorArgs.WhoInvolvedInError);

            System.Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++");


            System.Console.ResetColor();
        }
    }
}
