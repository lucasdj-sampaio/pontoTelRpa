#region - Imports
using PontoTelRpa.Domain.Models;
using PontoTelRpa.Navigation.PontoTel;
using PontoTelRpa.Navigation.PontoTelGestao;
#pragma warning disable IDE0044, CS8602, CS8629
#endregion

namespace PontoTelRpa.Worker
{
    public class Worker : BackgroundService
    {
        private DriversList _driversList;
        private ShotPointNavigator _shotPointNav;
        private ManagerNavigator _managerNav;

        public Worker(DriversList list
            , ShotPointNavigator shotPointNav
            , ManagerNavigator managerNav)
        {
            _driversList = list;
            _shotPointNav = shotPointNav;
            _managerNav = managerNav;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var lstTsk = new List<Task>();
                
                    foreach (Browser driverSetting in _driversList.BrowserList)
                        switch ((int)driverSetting?.NavigationType) 
                        {
                            case 1:
                                lstTsk.Add(Task.Run(()
                                    => shotPointNav.StartNavegation(driverSetting), stoppingToken));
                                break;
                            case 2:
                                lstTsk.Add(Task.Run(() 
                                    => managerNav.StartNavegation(driverSetting), stoppingToken));
                                break;
                            default:
                                break;
                        }

                    Task.WaitAll(lstTsk.ToArray(), stoppingToken);
                    lstTsk.RemoveAll(t => t.IsCompleted);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Thread.Sleep(1000);
            }
        }
    }
}