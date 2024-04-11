using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
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

    private static async Task WaitElementIsVisibleAsync(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.FindElement(by).Displayed)
                {
                    return;
                }
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementIsVisibleAsync]");
    }
    public static async Task WaitElementIsVisibleByIdAsync(this IWebDriver driver, string id, int seconds = 15) => await driver.WaitElementIsVisibleAsync(By.Id(id), seconds);
    public static async Task WaitElementIsVisibleBySelectorAsync(this IWebDriver driver, string selector, int seconds = 15) => await driver.WaitElementIsVisibleAsync(By.CssSelector(selector), seconds);
    public static async Task WaitElementIsVisibleByXPathAsync(this IWebDriver driver, string xpath, int seconds = 15) => await driver.WaitElementIsVisibleAsync(By.XPath(xpath), seconds);
    public static async Task WaitElementIsVisibleByTagNameAsync(this IWebDriver driver, string tagName, int seconds = 15) => await driver.WaitElementIsVisibleAsync(By.TagName(tagName), seconds);
    public static async Task WaitElementIsVisibleByLinkTextAsync(this IWebDriver driver, string linkText, int seconds = 15) => await driver.WaitElementIsVisibleAsync(By.LinkText(linkText), seconds);

    private static void WaitElementIsVisible(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.FindElement(by).Displayed)
                {
                    return;
                }
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementIsVisible]");
    }
    public static void WaitElementIsVisibleById(this IWebDriver driver, string id, int seconds = 15) => driver.WaitElementIsVisible(By.Id(id), seconds);
    public static void WaitElementIsVisibleBySelector(this IWebDriver driver, string selector, int seconds = 15) => driver.WaitElementIsVisible(By.CssSelector(selector), seconds);
    public static void WaitElementIsVisibleByXPath(this IWebDriver driver, string xpath, int seconds = 15) => driver.WaitElementIsVisible(By.XPath(xpath), seconds);
    public static void WaitElementIsVisibleByTagName(this IWebDriver driver, string tagName, int seconds = 15) => driver.WaitElementIsVisible(By.TagName(tagName), seconds);
    public static void WaitElementIsVisibleByLinkText(this IWebDriver driver, string linkText, int seconds = 15) => driver.WaitElementIsVisible(By.LinkText(linkText), seconds);

    private static async Task WaitElementIsNotVisibleAsync(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (!driver.FindElement(by).Displayed)
                {
                    return;
                }
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementIsNotVisibleAsync]");
    }
    public static async Task WaitElementIsNotVisibleByIdAsync(this IWebDriver driver, string id, int seconds = 15) => await driver.WaitElementIsNotVisibleAsync(By.Id(id), seconds);
    public static async Task WaitElementIsNotVisibleBySelectorAsync(this IWebDriver driver, string selector, int seconds = 15) => await driver.WaitElementIsNotVisibleAsync(By.CssSelector(selector), seconds);
    public static async Task WaitElementIsNotVisibleByXPathAsync(this IWebDriver driver, string xpath, int seconds = 15) => await driver.WaitElementIsNotVisibleAsync(By.XPath(xpath), seconds);
    public static async Task WaitElementIsNotVisibleByTagNameAsync(this IWebDriver driver, string tagName, int seconds = 15) => await driver.WaitElementIsNotVisibleAsync(By.TagName(tagName), seconds);
    public static async Task WaitElementIsNotVisibleByLinkTextAsync(this IWebDriver driver, string linkText, int seconds = 15) => await driver.WaitElementIsNotVisibleAsync(By.LinkText(linkText), seconds);

    private static void WaitElementIsNotVisible(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (!driver.FindElement(by).Displayed)
                {
                    return;
                }
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementIsNotVisible]");
    }
    public static void WaitElementIsNotVisibleById(this IWebDriver driver, string id, int seconds = 15) => driver.WaitElementIsNotVisible(By.Id(id), seconds);
    public static void WaitElementIsNotVisibleBySelector(this IWebDriver driver, string selector, int seconds = 15) => driver.WaitElementIsNotVisible(By.CssSelector(selector), seconds);
    public static void WaitElementIsNotVisibleByXPath(this IWebDriver driver, string xpath, int seconds = 15) => driver.WaitElementIsNotVisible(By.XPath(xpath), seconds);
    public static void WaitElementIsNotVisibleByTagName(this IWebDriver driver, string tagName, int seconds = 15) => driver.WaitElementIsNotVisible(By.TagName(tagName), seconds);
    public static void WaitElementIsNotVisibleByLinkText(this IWebDriver driver, string linkText, int seconds = 15) => driver.WaitElementIsNotVisible(By.LinkText(linkText), seconds);

    private static async Task WaitElementExistsAsync(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                driver.FindElement(by);
                return;
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementExistsAsync]");
    }
    public static async Task WaitElementExistsByIdAsync(this IWebDriver driver, string id, int seconds = 15) => await driver.WaitElementExistsAsync(By.Id(id), seconds);
    public static async Task WaitElementExistsBySelectorAsync(this IWebDriver driver, string selector, int seconds = 15) => await driver.WaitElementExistsAsync(By.CssSelector(selector), seconds);
    public static async Task WaitElementExistsByXPathAsync(this IWebDriver driver, string xpath, int seconds = 15) => await driver.WaitElementExistsAsync(By.XPath(xpath), seconds);
    public static async Task WaitElementExistsByTagNameAsync(this IWebDriver driver, string tagName, int seconds = 15) => await driver.WaitElementExistsAsync(By.TagName(tagName), seconds);
    public static async Task WaitElementExistsByLinkTextAsync(this IWebDriver driver, string linkText, int seconds = 15) => await driver.WaitElementExistsAsync(By.LinkText(linkText), seconds);

    private static void WaitElementExists(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                driver.FindElement(by);
                return;
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementExists]");
    }
    public static void WaitElementExistsById(this IWebDriver driver, string id, int seconds = 15) => driver.WaitElementExists(By.Id(id), seconds);
    public static void WaitElementExistsBySelector(this IWebDriver driver, string selector, int seconds = 15) => driver.WaitElementExists(By.CssSelector(selector), seconds);
    public static void WaitElementExistsByXPath(this IWebDriver driver, string xpath, int seconds = 15) => driver.WaitElementExists(By.XPath(xpath), seconds);
    public static void WaitElementExistsByTagName(this IWebDriver driver, string tagName, int seconds = 15) => driver.WaitElementExists(By.TagName(tagName), seconds);
    public static void WaitElementExistsByLinkText(this IWebDriver driver, string linkText, int seconds = 15) => driver.WaitElementExists(By.LinkText(linkText), seconds);

    private static async Task WaitElementNotExistsAsync(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                driver.FindElement(by);
            }
            catch
            {
                return;
            }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementNotExistsAsync]");
    }
    public static async Task WaitElementNotExistsByIdAsync(this IWebDriver driver, string id, int seconds = 15) => await driver.WaitElementNotExistsAsync(By.Id(id), seconds);
    public static async Task WaitElementNotExistsBySelectorAsync(this IWebDriver driver, string selector, int seconds = 15) => await driver.WaitElementNotExistsAsync(By.CssSelector(selector), seconds);
    public static async Task WaitElementNotExistsByXPathAsync(this IWebDriver driver, string xpath, int seconds = 15) => await driver.WaitElementNotExistsAsync(By.XPath(xpath), seconds);
    public static async Task WaitElementNotExistsByTagNameAsync(this IWebDriver driver, string tagName, int seconds = 15) => await driver.WaitElementNotExistsAsync(By.TagName(tagName), seconds);
    public static async Task WaitElementNotExistsByLinkTextAsync(this IWebDriver driver, string linkText, int seconds = 15) => await driver.WaitElementNotExistsAsync(By.LinkText(linkText), seconds);

    private static void WaitElementNotExists(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                driver.FindElement(by);
            }
            catch
            {
                return;
            }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementNotExists]");
    }
    public static void WaitElementNotExistsById(this IWebDriver driver, string id, int seconds = 15) => driver.WaitElementNotExists(By.Id(id), seconds);
    public static void WaitElementNotExistsBySelector(this IWebDriver driver, string selector, int seconds = 15) => driver.WaitElementNotExists(By.CssSelector(selector), seconds);
    public static void WaitElementNotExistsByXPath(this IWebDriver driver, string xpath, int seconds = 15) => driver.WaitElementNotExists(By.XPath(xpath), seconds);
    public static void WaitElementNotExistsByTagName(this IWebDriver driver, string tagName, int seconds = 15) => driver.WaitElementNotExists(By.TagName(tagName), seconds);
    public static void WaitElementNotExistsByLinkText(this IWebDriver driver, string linkText, int seconds = 15) => driver.WaitElementNotExists(By.LinkText(linkText), seconds);

    private static async Task WaitElementClickableAsync(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.FindElement(by).Displayed && driver.FindElement(by).Enabled)
                {
                    return;
                }
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementClickableAsync]");
    }
    public static async Task WaitElementClickableByIdAsync(this IWebDriver driver, string id, int seconds = 15) => await driver.WaitElementClickableAsync(By.Id(id), seconds);
    public static async Task WaitElementClickableBySelectorAsync(this IWebDriver driver, string selector, int seconds = 15) => await driver.WaitElementClickableAsync(By.CssSelector(selector), seconds);
    public static async Task WaitElementClickableByXPathAsync(this IWebDriver driver, string xpath, int seconds = 15) => await driver.WaitElementClickableAsync(By.XPath(xpath), seconds);
    public static async Task WaitElementClickableByTagNameAsync(this IWebDriver driver, string tagName, int seconds = 15) => await driver.WaitElementClickableAsync(By.TagName(tagName), seconds);
    public static async Task WaitElementClickableByLinkTextAsync(this IWebDriver driver, string linkText, int seconds = 15) => await driver.WaitElementClickableAsync(By.LinkText(linkText), seconds);

    private static void WaitElementClickable(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.FindElement(by).Displayed && driver.FindElement(by).Enabled)
                {
                    return;
                }
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementClickable]");
    }
    public static void WaitElementClickableById(this IWebDriver driver, string id, int seconds = 15) => driver.WaitElementClickable(By.Id(id), seconds);
    public static void WaitElementClickableBySelector(this IWebDriver driver, string selector, int seconds = 15) => driver.WaitElementClickable(By.CssSelector(selector), seconds);
    public static void WaitElementClickableByXPath(this IWebDriver driver, string xpath, int seconds = 15) => driver.WaitElementClickable(By.XPath(xpath), seconds);
    public static void WaitElementClickableByTagName(this IWebDriver driver, string tagName, int seconds = 15) => driver.WaitElementClickable(By.TagName(tagName), seconds);
    public static void WaitElementClickableByLinkText(this IWebDriver driver, string linkText, int seconds = 15) => driver.WaitElementClickable(By.LinkText(linkText), seconds);

    private static async Task WaitElementSelectedAsync(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.FindElement(by).Selected)
                {
                    return;
                }
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementSelectedAsync]");
    }
    public static async Task WaitElementSelectedByIdAsync(this IWebDriver driver, string id, int seconds = 15) => await driver.WaitElementSelectedAsync(By.Id(id), seconds);
    public static async Task WaitElementSelectedBySelectorAsync(this IWebDriver driver, string selector, int seconds = 15) => await driver.WaitElementSelectedAsync(By.CssSelector(selector), seconds);
    public static async Task WaitElementSelectedByXPathAsync(this IWebDriver driver, string xpath, int seconds = 15) => await driver.WaitElementSelectedAsync(By.XPath(xpath), seconds);
    public static async Task WaitElementSelectedByTagNameAsync(this IWebDriver driver, string tagName, int seconds = 15) => await driver.WaitElementSelectedAsync(By.TagName(tagName), seconds);
    public static async Task WaitElementSelectedByLinkTextAsync(this IWebDriver driver, string linkText, int seconds = 15) => await driver.WaitElementSelectedAsync(By.LinkText(linkText), seconds);

    private static void WaitElementSelected(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.FindElement(by).Selected)
                {
                    return;
                }
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementSelected]");
    }
    public static void WaitElementSelectedById(this IWebDriver driver, string id, int seconds = 15) => driver.WaitElementSelected(By.Id(id), seconds);
    public static void WaitElementSelectedBySelector(this IWebDriver driver, string selector, int seconds = 15) => driver.WaitElementSelected(By.CssSelector(selector), seconds);
    public static void WaitElementSelectedByXPath(this IWebDriver driver, string xpath, int seconds = 15) => driver.WaitElementSelected(By.XPath(xpath), seconds);
    public static void WaitElementSelectedByTagName(this IWebDriver driver, string tagName, int seconds = 15) => driver.WaitElementSelected(By.TagName(tagName), seconds);
    public static void WaitElementSelectedByLinkText(this IWebDriver driver, string linkText, int seconds = 15) => driver.WaitElementSelected(By.LinkText(linkText), seconds);

    private static async Task WaitElementNotSelectedAsync(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (!driver.FindElement(by).Selected)
                {
                    return;
                }
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementNotSelectedAsync]");
    }
    public static async Task WaitElementNotSelectedByIdAsync(this IWebDriver driver, string id, int seconds = 15) => await driver.WaitElementNotSelectedAsync(By.Id(id), seconds);
    public static async Task WaitElementNotSelectedBySelectorAsync(this IWebDriver driver, string selector, int seconds = 15) => await driver.WaitElementNotSelectedAsync(By.CssSelector(selector), seconds);
    public static async Task WaitElementNotSelectedByXPathAsync(this IWebDriver driver, string xpath, int seconds = 15) => await driver.WaitElementNotSelectedAsync(By.XPath(xpath), seconds);
    public static async Task WaitElementNotSelectedByTagNameAsync(this IWebDriver driver, string tagName, int seconds = 15) => await driver.WaitElementNotSelectedAsync(By.TagName(tagName), seconds);
    public static async Task WaitElementNotSelectedByLinkTextAsync(this IWebDriver driver, string linkText, int seconds = 15) => await driver.WaitElementNotSelectedAsync(By.LinkText(linkText), seconds);

    private static void WaitElementNotSelected(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (!driver.FindElement(by).Selected)
                {
                    return;
                }
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementNotSelected]");
    }
    public static void WaitElementNotSelectedById(this IWebDriver driver, string id, int seconds = 15) => driver.WaitElementNotSelected(By.Id(id), seconds);
    public static void WaitElementNotSelectedBySelector(this IWebDriver driver, string selector, int seconds = 15) => driver.WaitElementNotSelected(By.CssSelector(selector), seconds);
    public static void WaitElementNotSelectedByXPath(this IWebDriver driver, string xpath, int seconds = 15) => driver.WaitElementNotSelected(By.XPath(xpath), seconds);
    public static void WaitElementNotSelectedByTagName(this IWebDriver driver, string tagName, int seconds = 15) => driver.WaitElementNotSelected(By.TagName(tagName), seconds);
    public static void WaitElementNotSelectedByLinkText(this IWebDriver driver, string linkText, int seconds = 15) => driver.WaitElementNotSelected(By.LinkText(linkText), seconds);

    public static string GetAlertText(this IWebDriver driver, int seconds = 15) => driver.SwitchTo().Alert().Text;
    public static async Task WaitAlertAsync(this IWebDriver driver, int seconds = 15)
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
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitAlertAsync]");
    }
    public static async Task<string> WaitAlertAndGetTextAsync(this IWebDriver driver, int seconds = 15)
    {
        await driver.WaitAlertAsync(seconds);
        return driver.GetAlertText();
    }
    public static async Task WaitAlertContainsTextAsync (this IWebDriver driver, string text, bool accept = true, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.GetAlertText().Contains(text, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitAlertContainsTextAsync]");
    }
    public static async Task WaitAlertContainsTextAndAcceptAsync(this IWebDriver driver, string text, int seconds = 15) { await driver.WaitAlertContainsTextAsync(text); driver.SwitchTo().Alert().Accept(); }
    public static async Task WaitAlertContainsTextAndDismissAsync(this IWebDriver driver, string text, int seconds = 15) { await driver.WaitAlertContainsTextAsync(text); driver.SwitchTo().Alert().Dismiss(); }
    public static async Task WaitAlertAndAcceptAsync(this IWebDriver driver, int seconds = 15) { await driver.WaitAlertAsync(seconds); driver.SwitchTo().Alert().Accept(); }
    public static async Task WaitAlertAndDismissAsync(this IWebDriver driver, int seconds = 15) { await driver.WaitAlertAsync(seconds); driver.SwitchTo().Alert().Dismiss(); }

    public static void WaitAlert(this IWebDriver driver, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                driver.SwitchTo().Alert(); return;
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitAlert]");
    }
    public static string WaitAlertAndGetText(this IWebDriver driver, int seconds = 15)
    {
        driver.WaitAlert(seconds);
        return driver.GetAlertText();
    }
    public static void WaitAlertContainsText(this IWebDriver driver, string text, bool accept = true, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.GetAlertText().Contains(text, StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitAlertContainsTextAsync]");
    }
    public static void WaitAlertContainsTextAndAccept(this IWebDriver driver, string text, int seconds = 15) { driver.WaitAlertContainsText(text); driver.SwitchTo().Alert().Accept(); }
    public static void WaitAlertContainsTextAndDismiss(this IWebDriver driver, string text, int seconds = 15) { driver.WaitAlertContainsText(text); driver.SwitchTo().Alert().Dismiss(); }
    public static void WaitAlertAndAccept(this IWebDriver driver, int seconds = 15) { driver.WaitAlert(seconds); driver.SwitchTo().Alert().Accept(); }
    public static void WaitAlertAndDismiss(this IWebDriver driver, int seconds = 15) { driver.WaitAlert(seconds); driver.SwitchTo().Alert().Dismiss(); }

    public static bool PageSourceContainsText(this IWebDriver driver, string text) => driver.PageSource.Contains(text, StringComparison.OrdinalIgnoreCase);
    public static bool PageSourceNotContainsText(this IWebDriver driver, string text) => !driver.PageSource.Contains(text, StringComparison.OrdinalIgnoreCase);
    public static async Task WaitPageSourceContainsTextAsync(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++) { try { if (driver.PageSourceContainsText(text)) { return; } } catch { } await Task.Delay(1000); }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitPageSourceContainText]");
    }
    public static void WaitPageSourceContainsText(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++) { try { if (driver.PageSourceContainsText(text)) { return; } } catch { } Thread.Sleep(1000); ; }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitPageSourceContainText]");
    }
    public static async Task WaitPageSourceNotContainsTextAsync(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++) { try { if (driver.PageSourceNotContainsText(text)) { return; } } catch { } await Task.Delay(1000); }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitPageSourceNotContainText]");
    }
    public static void WaitPageSourceNotContainsText(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++) { try { if (driver.PageSourceNotContainsText(text)) { return; } } catch { } Thread.Sleep(1000); ; }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitPageSourceNotContainText]");
    }

    public static bool UrlContainsText(this IWebDriver driver, string text) => driver.Url.Contains(text, StringComparison.OrdinalIgnoreCase);
    public static bool UrlNotContainsText(this IWebDriver driver, string text) => !driver.Url.Contains(text, StringComparison.OrdinalIgnoreCase);
    public static async Task WaitUrlContainsTextAsync(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.UrlContainsText(text))
                {
                    return;
                }
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitUrlContainsTextAsync]");
    }
    public static void WaitUrlContainsText(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.UrlContainsText(text))
                {
                    return;
                }
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitUrlContainsText]");
    }
    public static async Task WaitUrlNotContainsTextAsync(this IWebDriver driver, string text, int seconds = 15)
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
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitUrlNotContainsTextAsync]");
    }
    public static void WaitUrlNotContainsText(this IWebDriver driver, string text, int seconds = 15)
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
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitUrlNotContainsText]");
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
    public static async Task WaitBodyContainsTextAsync(this IWebDriver driver, string text, int seconds = 15, bool full = true)
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
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitBodyContainsTextAsync]");
    }
    public static async Task WaitBodyNotContainsTextAsync(this IWebDriver driver, string text, int seconds = 15, bool full = true)
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
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitBodyNotContainsTextAsync]");
    }
    public static void WaitBodyContainsText(this IWebDriver driver, string text, int seconds = 15, bool full = true)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                if (driver.BodyContainsText(text, full)) { return; }
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitBodyContainsText]");
    }
    public static void WaitBodyNotContainsText(this IWebDriver driver, string text, int seconds = 15, bool full = true)
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
            Thread.Sleep(1000);
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

    public static async Task WaitForPageToLoadAsync(this IWebDriver driver, int seconds = 15)
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
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitForPageToLoadAsync]");
    }
    public static void WaitForPageToLoad(this IWebDriver driver, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.ExecuteJavaScript<bool>("return document.readyState === 'complete';"))
            {
                return;
            }
            Thread.Sleep(1000);
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

    public static async Task WaitWindowHandlesCountAsync(this IWebDriver driver, int count, int seconds = 15)
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
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitWindowHandlesCountAsync]");
    }
    public static async Task WaitWindowHandlesCountMoreThanAsync(this IWebDriver driver, int count, int seconds = 15)
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
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitWindowHandlesCountMoreThanAsync]");
    }
    public static async Task WaitWindowHandlesCountLessThanAsync(this IWebDriver driver, int count, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.WindowHandles.Count < count)
            {
                return;
            }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitWindowHandlesCountLessThanAsync]");
    }

    public static void WaitWindowHandlesCount(this IWebDriver driver, int count, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.WindowHandles.Count == count)
            {
                return;
            }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitWindowHandlesCount]");
    }
    public static void WaitWindowHandlesCountMoreThan(this IWebDriver driver, int count, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.WindowHandles.Count > count)
            {
                return;
            }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitWindowHandlesCountMoreThan]");
    }
    public static void WaitWindowHandlesCountLessThan(this IWebDriver driver, int count, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.WindowHandles.Count < count)
            {
                return;
            }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitWindowHandlesCountLessThan]");
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
    public static void SaveScreenshot(this IWebDriver driver, string filePath) => driver.TakeScreenshot().SaveAsFile(filePath);
    public static void SavePageSource(this IWebDriver driver, string filePath) => File.WriteAllText(filePath, driver.PageSource);
}
