using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CountDown.WebTestingFramework
{
    public static class Browser
    {
        private static IWebDriver _chromePage;

        public static string Title
        {
            get { return _chromePage.Title; }
        }

        public static void Init()
        {
            _chromePage = new ChromeDriver();
        }

        public static void Quit()
        {
            _chromePage.Quit();
        }

        public static void GoToUrl(string url)
        {
            _chromePage.Navigate().GoToUrl(url);
        }

        public static bool Logout()
        {
            bool success = true;
            try
            {
                var anchor = _chromePage.FindElement(By.Id("signout"));
                anchor.Click();
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            return success;
        }

        public static bool HasElement(string cssSelector)
        {
            bool success = true;
            try
            {
                _chromePage.FindElement(By.CssSelector(cssSelector));
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            return success;
        }

        public static bool ClickElement(string cssSelector)
        {
            bool success = true;
            try
            {
                var button = _chromePage.FindElement(By.CssSelector(cssSelector));
                button.Click();
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            return success;
        }

        public static bool ElementHasAttributeWithValue(string cssSelector, string attr, string value)
        {
            try
            {
                var element = _chromePage.FindElement(By.CssSelector(cssSelector));
                return element.GetAttribute(attr).Equals(value);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool FillField(string cssSelector, string value)
        {
            bool success = true;
            try
            {
                var field = _chromePage.FindElement(By.CssSelector(cssSelector));
                field.SendKeys(value);
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            return success;
        }

        public static string GetInputValue(string cssSelector)
        {
            var element = _chromePage.FindElement(By.CssSelector(cssSelector));
            return element.GetAttribute("value");
        }

        public static string GetText(string cssSelector)
        {
            var element = _chromePage.FindElement(By.CssSelector(cssSelector));
            return element.Text;
        }
    }
}