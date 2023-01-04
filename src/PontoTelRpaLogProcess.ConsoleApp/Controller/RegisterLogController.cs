#region - Imports
using PontoTelRpa.Domain.Models;
using PontoTelRpa.Navigation.Utilities;
using Microsoft.Extensions.Configuration;
#endregion

namespace PontoTelRpaLogProcess.ConsoleApp.Controller
{
    public static class RegisterLogController
    {
        public static async Task MakeRegister(ENavigationType navType)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appConfig.json")
                        .Build();

            var fileManager = new FileManager(config);
            await fileManager.WriteFile(new ProcessLog() 
            { 
                    ProcessTime = DateTime.Now,
                    Message = navType == ENavigationType.ShotPoint 
                        ? "Realizar batida de ponto" : "Iniciando ajustes na folha de ponto",
                    NavegationType = navType
            });
        }
    }
}