#region - Imports
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
#pragma warning disable CS8602, CS8604
#endregion

namespace PontoTelRpa.Domain.Models
{
    public class DriversList
    {
        public IList<Browser>? BrowserList { get; set; }

        public DriversList(List<Credential> credentialList, string preference)
        {
            BrowserList = new List<Browser>();

            AddToDictionary(credentialList, preference);
        }

        private void AddToDictionary(List<Credential> credentialList, string preference)
        {
            foreach (var credential in credentialList)
                BrowserList.Add(CreateDriverInstance(credential, preference));
        }

        private static Browser CreateDriverInstance(Credential credential, string preference) =>
            new()
            {
                Driver = DefinePreference(preference, credential.Url),
                Credential = credential,
                Url = credential.Url,
                NavigationType = credential.NavigationType
            };

        private static IWebDriver DefinePreference(string preference, string url)
        {
            IWebDriver driver = preference switch
            {
                "Chrome" => new ChromeDriver(),
                "Firefox" => new FirefoxDriver(),
                _ => new ChromeDriver()
            };

            driver.Url = url;
            driver.Manage().Window.Maximize();

            return driver;
        }
    }

    public class Browser
    {
        public IWebDriver? Driver;

        public Credential? Credential;

        public string? Url;

        public ENavigationType? NavigationType;

        public bool Loged = false;
    }
}