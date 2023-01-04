#region - Imports
using OpenQA.Selenium;
using static System.TimeSpan;
using OpenQA.Selenium.Support.UI;
#pragma warning disable CS8603
#endregion

namespace PontoTelRpa.Navigation.Utilities
{
    public static class Util
    {
        public static IWebElement WaitElement(this IWebDriver driver, By by, int seconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, FromSeconds(seconds));
                var element = wait.Until(d => d.FindElement(by));
                return element;
            }
            catch
            {
                return null;
            }
        }

        public static bool JavascriptExecutor(this IWebDriver driver, string script)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool FrameSearch(this IWebDriver driver, string element)
        {
            IList<IWebElement> elements = driver.FindElements(By.TagName("iframe")).ToList();
            foreach (var frame in elements)
            {
                //Trade by father frame by elemente
                driver.SwitchTo().Frame(frame);

                try
                {
                    var elemento = driver.FindElement(By.XPath(element));

                    if (elemento != null)
                        return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        public static void CloseAllHandles(this IWebDriver driver, string currentWindowHandle)
        {
            foreach (var item in driver.WindowHandles)
                if (currentWindowHandle != item)
                {
                    driver.SwitchTo().Window(item);
                    driver.Close();
                }

            driver.SwitchTo().Window(currentWindowHandle);
        }

        public static void TypeSlow(this IWebElement element, string text) 
        {
            foreach (var word in text)
            {
                element.SendKeys(word.ToString());
                Thread.Sleep(200);
            }
        }
    }
}