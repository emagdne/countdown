using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CountDown.WebTestingFramework
{
    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
    public enum QueryMethod
    {
        CssSelector,
        Xpath
    }

    /// <summary>
    /// <para>Author: Jordan Brown</para>
    /// <para>Version: 5/1/14</para>
    /// </summary>
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

        public static bool HasElement(string query, QueryMethod method = QueryMethod.CssSelector)
        {
            bool success = true;
            try
            {
                QuerySingle(query, method);
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            return success;
        }

        public static bool ClickElement(string query, QueryMethod method = QueryMethod.CssSelector)
        {
            bool success = true;
            try
            {
                var element = QuerySingle(query, method);
                if (element != null)
                {
                    element.Click();
                }
                else
                {
                    success = false;
                }
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            return success;
        }

        public static bool ElementHasAttributeWithValue(string query, string attr, string value,
            QueryMethod method = QueryMethod.CssSelector)
        {
            try
            {
                var element = QuerySingle(query, method);
                var attribute = element != null ? element.GetAttribute(attr) : null;
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

        public static bool FillInputField(string query, string value, QueryMethod method = QueryMethod.CssSelector)
        {
            bool success = true;
            try
            {
                var field = QuerySingle(query, method);

                if (field != null)
                    field.SendKeys(value);
                else
                    success = false;
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            return success;
        }

        public static bool ClearInputField(string query, QueryMethod method = QueryMethod.CssSelector)
        {
            bool success = true;
            try
            {
                var element = QuerySingle(query, method);
                if (element != null)
                {
                    element.Clear();
                }
                else
                {
                    success = false;
                }
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            return success;
        }

        public static string GetSelectedDropdownOption(string query, QueryMethod method = QueryMethod.CssSelector)
        {
            var select = QuerySingle(query, method);
            return select != null
                ? new SelectElement(select).SelectedOption.Text
                : null;
        }

        public static bool SelectDropdownOption(string query, string text, QueryMethod method = QueryMethod.CssSelector)
        {
            bool success = true;
            try
            {
                var select = QuerySingle(query, method);

                if (select != null)
                {
                    var selectElement = new SelectElement(select);
                    selectElement.SelectByText(text);
                }
                else
                {
                    success = false;
                }
            }
            catch (NoSuchElementException)
            {
                success = false;
            }
            return success;
        }

        public static List<string> GetDropdownOptions(string query, QueryMethod method = QueryMethod.CssSelector)
        {
            var element = QuerySingle(query, method);
            return element != null ? new SelectElement(element).Options.Select(x => x.Text).ToList() : null;
        }

        public static string GetInputValue(string query, QueryMethod method = QueryMethod.CssSelector)
        {
            var element = QuerySingle(query, method);
            return element != null ? element.GetAttribute("value") : null;
        }

        public static string GetText(string query, QueryMethod method = QueryMethod.CssSelector)
        {
            var element = QuerySingle(query, method);
            return element != null ? element.Text : null;
        }

        public static int ElementCount(string query, QueryMethod method = QueryMethod.CssSelector)
        {
            var elements = QueryMultiple(query, method);
            return elements != null ? elements.Count : 0;
        }

        public static void CheckInputCheckbox(string query, QueryMethod method = QueryMethod.CssSelector)
        {
            if (!ElementHasAttributeWithValue(query, "checked", "true", method))
            {
                ClickElement(query, method);
            }
        }

        public static void UnCheckInputCheckbox(string query, QueryMethod method = QueryMethod.CssSelector)
        {
            if (ElementHasAttributeWithValue(query, "checked", "true", method))
            {
                ClickElement(query, method);
            }
        }

        public static bool ElementsHaveCss(string query, string cssPropertyName, string cssPropertyValue,
            QueryMethod method = QueryMethod.CssSelector)
        {
            var elements = QueryMultiple(query, method);

            if (elements != null)
            {
                foreach (var element in elements)
                {
                    if (!element.GetCssValue(cssPropertyName).Equals(cssPropertyValue))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public static bool ElementsHaveText(string query, string text, QueryMethod method = QueryMethod.CssSelector)
        {
            bool result = true;
            var elements = QueryMultiple(query, method);

            if (elements != null)
            {
                foreach (var element in elements)
                {
                    if (!element.Text.Equals(text))
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public static bool ElementsHaveAttributeWithValue(string query, string attr, string value,
            QueryMethod method = QueryMethod.CssSelector)
        {
            bool result = true;
            var elements = QueryMultiple(query, method);

            if (elements != null)
            {
                foreach (var element in elements)
                {
                    var attribute = element.GetAttribute(attr);
                    if (attribute == null || !attribute.Equals(value))
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        public static bool ElementsDoNotHaveAttribute(string query, string attr,
            QueryMethod method = QueryMethod.CssSelector)
        {
            bool result = true;
            var elements = QueryMultiple(query, method);

            if (elements != null)
            {
                foreach (var element in elements)
                {
                    var attribute = element.GetAttribute(attr);
                    if (attribute != null)
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
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

        public static void WaitForElementText(string query, string text, TimeSpan timeout,
            QueryMethod method = QueryMethod.CssSelector)
        {
            var wait = new WebDriverWait(_chromePage, timeout);
            wait.Until(x =>
            {
                try
                {
                    var element = QuerySingle(query, method);

                    if (element != null)
                        return element.Text.Equals(text);
                }
                catch (NoSuchElementException)
                {
                }
                return false;
            });
        }

        private static IWebElement QuerySingle(string query, QueryMethod method)
        {
            switch (method)
            {
                case QueryMethod.CssSelector:
                    return _chromePage.FindElement(By.CssSelector(query));
                case QueryMethod.Xpath:
                    return _chromePage.FindElement(By.XPath(query));
                default:
                    return null;
            }
        }

        private static IReadOnlyCollection<IWebElement> QueryMultiple(string query, QueryMethod method)
        {
            switch (method)
            {
                case QueryMethod.CssSelector:
                    return _chromePage.FindElements(By.CssSelector(query));
                case QueryMethod.Xpath:
                    return _chromePage.FindElements(By.XPath(query));
                default:
                    return null;
            }
        }
    }
}