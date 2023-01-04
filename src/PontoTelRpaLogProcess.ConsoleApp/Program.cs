#region - Imports
using PontoTelRpa.Domain.Models;
using PontoTelRpaLogProcess.ConsoleApp.Controller;
#endregion

namespace PontoTelRpaLogProcess.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                    throw new Exception("No arguments was sended");
                else if (int.Parse(args[0]) > 2 && int.Parse(args[0]) < 1)
                    throw new Exception("Arguments is invalid, select values between 1 and 2");

                var navType = (ENavigationType)int.Parse(args[0]);
                await RegisterLogController.MakeRegister(navType);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                Console.ReadKey();
            }
        }
    }
}