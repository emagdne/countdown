using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

        public static bool FillInputField(string cssSelector, string value)
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

        public static bool ClearInputField(string cssSelector)
        {
            bool success = true;
            try
            {
                var field = _chromePage.FindElement(By.CssSelector(cssSelector));
                field.Clear();
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            return success;
        } 

        public static string GetSelectedDropdownOption(string cssSelector)
        {
            return new SelectElement(_chromePage.FindElement(By.CssSelector(cssSelector))).SelectedOption.Text;
        }

        public static bool SelectDropdownOption(string cssSelector, string text)
        {
            bool success = true;
            try
            {
                var select = _chromePage.FindElement(By.CssSelector(cssSelector));
                var selectElement = new SelectElement(select);
                selectElement.SelectByText(text);
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            return success;
        }

        public static List<string> GetDropdownOptions(string cssSelector)
        {
            return
                new SelectElement(_chromePage.FindElement(By.CssSelector(cssSelector))).Options.Select(x => x.Text)
                    .ToList();
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