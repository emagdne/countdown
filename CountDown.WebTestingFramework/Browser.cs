using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
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
                var attribute = element.GetAttribute(attr);
                if (attribute != null)
                {
                    return attribute.Equals(value);
                }
            }
            catch (NoSuchElementException)
            {
            }
            return false;
        }

        public static bool ElementHasAttributeWithValueByXpath(string xpath, string attr, string value)
        {
            try
            {
                var element = _chromePage.FindElement(By.XPath(xpath));
                var attribute = element.GetAttribute(attr);
                if (attribute != null)
                {
                    return attribute.Equals(value);
                }
            }
            catch (NoSuchElementException)
            {
            }
            return false;
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

        public static int ElementCount(string cssSelector)
        {
            return _chromePage.FindElements(By.CssSelector(cssSelector)).Count;
        }

        public static void CheckInputCheckbox(string cssSelector)
        {
            if (!ElementHasAttributeWithValue(cssSelector, "checked", "true"))
            {
                ClickElement(cssSelector);
            }
        }

        public static void UnCheckInputCheckbox(string cssSelector)
        {
            if (ElementHasAttributeWithValue(cssSelector, "checked", "true"))
            {
                ClickElement(cssSelector);
            }
        }

        public static bool ElementsHaveCssByXpath(string xpath, string cssPropertyName, string cssPropertyValue)
        {
            var elements = _chromePage.FindElements(By.XPath(xpath));
            foreach (var element in elements)
            {
                if (!element.GetCssValue(cssPropertyName).Equals(cssPropertyValue))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ElementsHaveText(string cssSelector, string text)
        {
            bool result = true;

            var elements = _chromePage.FindElements(By.CssSelector(cssSelector));
            foreach (var element in elements)
            {
                if (!element.Text.Equals(text))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static bool ElementsHaveAttributeWithValueByXpath(string xpath, string attr, string value)
        {
            bool result = true;

            var elements = _chromePage.FindElements(By.XPath(xpath));
            foreach (var element in elements)
            {
                var attribute = element.GetAttribute(attr);
                if (attribute == null || !attribute.Equals(value))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static bool ElementsDoNotHaveAttributeByXpath(string xpath, string attr)
        {
            bool result = true;

            var elements = _chromePage.FindElements(By.XPath(xpath));
            foreach (var element in elements)
            {
                var attribute = element.GetAttribute(attr);
                if (attribute != null)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static bool ClickElementByXpath(string xpath)
        {
            bool success = true;
            try
            {
                var button = _chromePage.FindElement(By.XPath(xpath));
                button.Click();
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            return success;
        }

        public static bool HasAlert()
        {
            var alert = _chromePage.SwitchTo().Alert();
            return alert != null;
        }

        public static void ClickOkInAlert()
        {
            var alert = _chromePage.SwitchTo().Alert();
            if (alert != null)
            {
                alert.Accept();
            }
        }

        public static void ClickCancelInAlert()
        {
            var alert = _chromePage.SwitchTo().Alert();
            if (alert != null)
            {
                alert.Dismiss();
            }
        }

        public static void WaitForElementText(string cssSelector, string text, TimeSpan timeout)
        {
            var wait = new WebDriverWait(_chromePage, timeout);
            wait.Until(x =>
            {
                try
                {
                    var element = x.FindElement(By.CssSelector(cssSelector));
                    return element.Text.Equals(text);
                } catch (NoSuchElementException) { }
                return false;
            });
        }
    }
}