using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Text;

namespace GCScript.Selenium.ExtensionMethods;

public static class GCScriptExtensionMethods
{
    public static IWebElement GetElementById(this IWebDriver driver, string id) => driver.FindElement(By.Id(id));
    public static IWebElement GetElementBySelector(this IWebDriver driver, string selector) => driver.FindElement(By.CssSelector(selector));
    public static IWebElement GetElementByXPath(this IWebDriver driver, string xpath) => driver.FindElement(By.XPath(xpath));
    public static IWebElement GetElementByTagName(this IWebDriver driver, string tagName) => driver.FindElement(By.TagName(tagName));
    public static IWebElement GetElementByLinkText(this IWebDriver driver, string linkText) => driver.FindElement(By.LinkText(linkText));

    public static IWebElement GetElementById(this IWebElement element, string id) => element.FindElement(By.Id(id));
    public static IWebElement GetElementBySelector(this IWebElement element, string selector) => element.FindElement(By.CssSelector(selector));
    public static IWebElement GetElementByXPath(this IWebElement element, string xpath) => element.FindElement(By.XPath(xpath));
    public static IWebElement GetElementByTagName(this IWebElement element, string tagName) => element.FindElement(By.TagName(tagName));
    public static IWebElement GetElementByLinkText(this IWebElement element, string linkText) => element.FindElement(By.LinkText(linkText));

    public static ReadOnlyCollection<IWebElement> GetElementsById(this IWebDriver driver, string id) => driver.FindElements(By.Id(id));
    public static ReadOnlyCollection<IWebElement> GetElementsBySelector(this IWebDriver driver, string selector) => driver.FindElements(By.CssSelector(selector));
    public static ReadOnlyCollection<IWebElement> GetElementsByXPath(this IWebDriver driver, string xpath) => driver.FindElements(By.XPath(xpath));
    public static ReadOnlyCollection<IWebElement> GetElementsByTagName(this IWebDriver driver, string tagName) => driver.FindElements(By.TagName(tagName));
    public static ReadOnlyCollection<IWebElement> GetElementsByLinkText(this IWebDriver driver, string linkText) => driver.FindElements(By.LinkText(linkText));

    public static ReadOnlyCollection<IWebElement> GetElementsById(this IWebElement element, string id) => element.FindElements(By.Id(id));
    public static ReadOnlyCollection<IWebElement> GetElementsBySelector(this IWebElement element, string selector) => element.FindElements(By.CssSelector(selector));
    public static ReadOnlyCollection<IWebElement> GetElementsByXPath(this IWebElement element, string xpath) => element.FindElements(By.XPath(xpath));
    public static ReadOnlyCollection<IWebElement> GetElementsByTagName(this IWebElement element, string tagName) => element.FindElements(By.TagName(tagName));
    public static ReadOnlyCollection<IWebElement> GetElementsByLinkText(this IWebElement element, string linkText) => element.FindElements(By.LinkText(linkText));

    public static async Task WaitElementIsVisibleById(this IWebDriver driver, string id, int seconds = 15)
    {
        await driver.WaitElementIsVisible(x => x.FindElement(By.Id(id)), seconds);
    }
    public static async Task WaitElementIsVisibleBySelector(this IWebDriver driver, string selector, int seconds = 15)
    {
        await driver.WaitElementIsVisible(x => x.FindElement(By.CssSelector(selector)), seconds);
    }
    public static async Task WaitElementIsVisibleByXPath(this IWebDriver driver, string xpath, int seconds = 15)
    {
        await driver.WaitElementIsVisible(x => x.FindElement(By.XPath(xpath)), seconds);
    }
    public static async Task WaitElementIsVisibleByTagName(this IWebDriver driver, string tagName, int seconds = 15)
    {
        await driver.WaitElementIsVisible(x => x.FindElement(By.TagName(tagName)), seconds);
    }
    public static async Task WaitElementIsVisibleByLinkText(this IWebDriver driver, string linkText, int seconds = 15)
    {
        await driver.WaitElementIsVisible(x => x.FindElement(By.LinkText(linkText)), seconds);
    }
    private static async Task WaitElementIsVisible<T>(this IWebDriver driver, Func<IWebDriver, T> getElement, int seconds = 15) where T : IWebElement
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (getElement(driver).Displayed)
                {
                    return;
                }
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementIsVisible]");
    }

    public static async Task WaitElementExistsById(this IWebDriver driver, string id, int seconds = 15)
    {
        await driver.WaitElementExists(x => x.FindElement(By.Id(id)), seconds);
    }
    public static async Task WaitElementExistsBySelector(this IWebDriver driver, string selector, int seconds = 15)
    {
        await driver.WaitElementExists(x => x.FindElement(By.CssSelector(selector)), seconds);
    }
    public static async Task WaitElementExistsByXPath(this IWebDriver driver, string xpath, int seconds = 15)
    {
        await driver.WaitElementExists(x => x.FindElement(By.XPath(xpath)), seconds);
    }
    public static async Task WaitElementExistsByTagName(this IWebDriver driver, string tagName, int seconds = 15)
    {
        await driver.WaitElementExists(x => x.FindElement(By.TagName(tagName)), seconds);
    }
    public static async Task WaitElementExistsByLinkText(this IWebDriver driver, string linkText, int seconds = 15)
    {
        await driver.WaitElementExists(x => x.FindElement(By.LinkText(linkText)), seconds);
    }
    private static async Task WaitElementExists<T>(this IWebDriver driver, Func<IWebDriver, T> getElement, int seconds = 15) where T : IWebElement
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                getElement(driver);
                return;
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementExists]");
    }

    public static string GetAlertText(this IWebDriver driver, int seconds = 15) => driver.SwitchTo().Alert().Text;
    public static async Task WaitAlert(this IWebDriver driver, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                driver.SwitchTo().Alert(); return;
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitAlert]");
    }
    public static async Task WaitAlertAndAccept(this IWebDriver driver, int seconds = 15) { await driver.WaitAlert(seconds); driver.SwitchTo().Alert().Accept(); }
    public static async Task WaitAlertAndDismiss(this IWebDriver driver, int seconds = 15) { await driver.WaitAlert(seconds); driver.SwitchTo().Alert().Dismiss(); }

    public static bool PageSourceContainsText(this IWebDriver driver, string text) => driver.PageSource.Contains(text, StringComparison.OrdinalIgnoreCase);
    public static bool PageSourceNotContainsText(this IWebDriver driver, string text) => !driver.PageSource.Contains(text, StringComparison.OrdinalIgnoreCase);
    public static async Task WaitPageSourceContainsText(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++) { try { if (driver.PageSourceContainsText(text)) { return; } } catch { } await Task.Delay(1000); }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitPageSourceContainText]");
    }
    public static async Task WaitPageSourceNotContainsText(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++) { try { if (driver.PageSourceNotContainsText(text)) { return; } } catch { } await Task.Delay(1000); }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitPageSourceNotContainText]");
    }

    public static bool UrlContainsText(this IWebDriver driver, string text) => driver.Url.Contains(text, StringComparison.OrdinalIgnoreCase);
    public static bool UrlNotContainsText(this IWebDriver driver, string text) => !driver.Url.Contains(text, StringComparison.OrdinalIgnoreCase);
    public static async Task WaitUrlContainsText(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++) { try { if (driver.UrlContainsText(text)) { return; } } catch { } await Task.Delay(1000); }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitUrlContains]");
    }
    public static async Task WaitUrlNotContainsText(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.UrlNotContainsText(text))
                {
                    return;
                }
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitUrlNotContains]");
    }

    public static string GetBodyText(this IWebDriver driver, bool full = true)
    {
        if (full)
        {
            StringBuilder bodyText = new();
            bodyText.AppendLine(driver.GetElementBySelector("body").Text);

            ReadOnlyCollection<IWebElement> iframes = driver.GetElementsByTagName("iframe");

            foreach (IWebElement? iframe in iframes)
            {
                bodyText.AppendLine($"--------------------[IFRAME {iframes.IndexOf(iframe).ToString().PadLeft(2, '0')}]--------------------");
                driver.SwitchTo().Frame(iframe);
                bodyText.AppendLine(driver.GetElementBySelector("body").Text);
                driver.SwitchTo().DefaultContent();
            }
            return bodyText.ToString();
        }
        else
        {
            return driver.GetElementBySelector("body").Text;
        }
    }
    public static bool BodyContainsText(this IWebDriver driver, string text, bool full = true) => driver.GetBodyText(full).Contains(text, StringComparison.OrdinalIgnoreCase);
    public static bool BodyNotContainsText(this IWebDriver driver, string text, bool full = true) => !driver.GetBodyText(full).Contains(text, StringComparison.OrdinalIgnoreCase);
    public static async Task WaitBodyContainsText(this IWebDriver driver, string text, int seconds = 15, bool full = true)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.BodyContainsText(text, full)) { return; }
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitBodyContainsText]");
    }
    public static async Task WaitBodyNotContainsText(this IWebDriver driver, string text, int seconds = 15, bool full = true)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.BodyNotContainsText(text, full))
                {
                    return;
                }
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitBodyNotContainsText]");
    }

    public static string InnerText(this IWebElement element) => element.GetAttribute("innerText");
    public static string InnerHTML(this IWebElement element) => element.GetAttribute("innerHTML");
    public static void Focus(this IWebElement element)
    {
        IWebDriver driver = ((IWrapsDriver)element).WrappedDriver;
        driver.ExecuteJavaScript("arguments[0].focus();", element);
    }
    public static void Unfocus(this IWebElement element)
    {
        IWebDriver driver = ((IWrapsDriver)element).WrappedDriver;
        driver.ExecuteJavaScript("arguments[0].blur();", element);
    }
    public static async Task WaitForPageToLoad(this IWebDriver driver, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.ExecuteJavaScript<bool>("return document.readyState === 'complete';"))
            {
                return;
            }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitForPageToLoad]");
    }
    public static void OverrideAlertConfirmTrue(this IWebDriver driver) => driver.ExecuteJavaScript("window.confirm = function () { return true; };");

    public static void FindWindowContainsTextInUrl(this IWebDriver driver, string text)
    {
        foreach (string window in driver.WindowHandles)
        {
            driver.SwitchTo().Window(window);
            if (driver.Url.Contains(text, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
        }
        throw new Exception($"Unable to find window containing text [{text}] in URL");
    }
    public static void FindWindowContainsTextInTitle(this IWebDriver driver, string text)
    {
        foreach (string window in driver.WindowHandles)
        {
            driver.SwitchTo().Window(window);
            if (driver.Title.Contains(text, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
        }
        throw new Exception($"Unable to find window containing text [{text}] in Title");
    }
    public static void FindWindowStartsWithTextInTitle(this IWebDriver driver, string text)
    {
        foreach (string window in driver.WindowHandles)
        {
            driver.SwitchTo().Window(window);
            if (driver.Title.StartsWith(text, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
        }
        throw new Exception($"Unable to find window starting with text [{text}] in Title");
    }
    public static void FindWindowEndsWithTextInTitle(this IWebDriver driver, string text)
    {
        foreach (string window in driver.WindowHandles)
        {
            driver.SwitchTo().Window(window);
            if (driver.Title.EndsWith(text, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
        }
        throw new Exception($"Unable to find window ending with text [{text}] in Title");
    }

    public static async Task WaitWindowHandlesCount(this IWebDriver driver, int count, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.WindowHandles.Count == count)
            {
                return;
            }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitWindowHandlesCount]");
    }
    public static async Task WaitWindowHandlesCountMoreThan(this IWebDriver driver, int count, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.WindowHandles.Count > count)
            {
                return;
            }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitWindowHandlesCountMoreThan]");
    }

    public static void NavigateToUrl(this IWebDriver driver, string url) => driver.Navigate().GoToUrl(url);
    public static void NavigateToUrlWithJS(this IWebDriver driver, string url) => driver.ExecuteJavaScript($"window.location.href = '{url}';");

    public static void SelectIndexInDropdown(this IWebElement element, int index) => new SelectElement(element).SelectByIndex(index);
    public static void SelectTextInDropdown(this IWebElement element, string text) => new SelectElement(element).SelectByText(text);
    public static void SelectValueInDropdown(this IWebElement element, string value) => new SelectElement(element).SelectByValue(value);

    public static void SetValueWithJS(this IWebElement element, string value)
    {
        IWebDriver driver = ((IWrapsDriver)element).WrappedDriver;
        driver.ExecuteJavaScript("arguments[0].value = arguments[1];", element, value);
    }
    public static string GetValueWithJS(this IWebElement element)
    {
        IWebDriver driver = ((IWrapsDriver)element).WrappedDriver;
        return driver.ExecuteJavaScript<string>("return arguments[0].value;", element);
    }


    public static void ForceClose(this IWebDriver driver) { try { driver.Close(); } catch { } try { driver.Quit(); } catch { } try { driver.Dispose(); } catch { } }
    public static void SaveScreenshot(this IWebDriver driver, string filePath) { try { driver.TakeScreenshot().SaveAsFile(filePath); } catch { } }
    public static void SavePageSource(this IWebDriver driver, string filePath) { try { File.WriteAllText(filePath, driver.PageSource); } catch { } }
}
