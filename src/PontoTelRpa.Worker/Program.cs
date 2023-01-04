#region - Imports
using System.Reflection;
using PontoTelRpa.Worker;
using PontoTelRpa.Domain.Models;
using PontoTelRpa.Navigation.PontoTel;
using PontoTelRpa.Navigation.Utilities;
using PontoTelRpa.Navigation.PontoTelGestao;
#pragma warning disable CS8604
#endregion

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var configJson = new ConfigurationBuilder()
            .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .AddJsonFile("appsettings.json")
            .AddJsonFile("navigationConfig.json")
            .AddJsonFile("appConfig.json")
            .Build();

        services.AddHostedService<Worker>();
        services.AddSingleton<IConfiguration>(configJson);
        services.AddTransient<ShotPointNavigator>();
        //pages and controllers here

        services.AddTransient<ManagerNavigator>();
        //pages and controllers here

        services.AddSingleton<FileManager>();

        services.AddSingleton<DriversList>(d =>
        {
            return new(configJson.GetSection("Credentials").Get<List<Credential>>()
                , configJson["PreferenceDriver"]);
        });
    })
    .Build();

await host.RunAsync();