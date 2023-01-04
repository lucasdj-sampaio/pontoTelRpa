#region - Imports
using Newtonsoft.Json;
using PontoTelRpa.Domain.Models;
using Microsoft.Extensions.Configuration;
#pragma warning disable CS8618, CS8601
#endregion

namespace PontoTelRpa.Navigation.Utilities
{
    public class FileManager
    {
        private static string FilePath { get; set; }

        public FileManager(IConfiguration config) 
        { 
            FilePath = config["LogFilePath"];

            if (!VerifyIfFileExist())
                throw new Exception("Log file dos not exists, please look setting file and change repository");
        }

        private static bool VerifyIfFileExist() =>
            File.Exists(FilePath);
        

        public async Task<IList<ProcessLog>> ReadFile()
        {
            var fileData = await File.ReadAllTextAsync(FilePath);

            if (!String.IsNullOrEmpty(fileData))
                return JsonConvert.DeserializeObject<IList<ProcessLog>>(fileData);

            return new List<ProcessLog> { };
        }

        public async Task WriteFile(ProcessLog data) 
        { 
            var logGroup = await ReadFile();
            logGroup?.Add(data);

            await File.WriteAllTextAsync(FilePath
                , JsonConvert.SerializeObject(logGroup));
        }
    }
}