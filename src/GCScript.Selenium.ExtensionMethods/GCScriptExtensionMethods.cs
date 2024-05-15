using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Text;

namespace GCScript.Selenium.ExtensionMethods;

public static class GCScriptExtensionMethods
{
    public static IWebElement GetElement(this IWebDriver driver, By by) => driver.FindElement(by);
    public static IWebElement GetElement(this IWebElement element, By by) => element.FindElement(by);
    public static ReadOnlyCollection<IWebElement> GetElements(this IWebDriver driver, By by) => driver.FindElements(by);
    public static ReadOnlyCollection<IWebElement> GetElements(this IWebElement element, By by) => element.FindElements(by);

    public static bool ElementIsVisible(this IWebDriver driver, By by)
    {
        try
        {
            return driver.GetElement(by).Displayed;
        }
        catch
        {
            return false;
        }
    }

    public static bool ElementIsVisible(this IWebElement element, By by)
    {
        try
        {
            return element.GetElement(by).Displayed;
        }
        catch
        {
            return false;
        }
    }

    public static bool ElementExists(this IWebDriver driver, By by)
    {
        try
        {
            driver.GetElement(by);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool ElementExists(this IWebElement element, By by)
    {
        try
        {
            element.GetElement(by);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool ElementIsSelected(this IWebDriver driver, By by)
    {
        try
        {
            return driver.GetElement(by).Selected;
        }
        catch
        {
            return false;
        }
    }

    public static bool ElementIsSelected(this IWebElement element, By by)
    {
        try
        {
            return element.GetElement(by).Selected;
        }
        catch
        {
            return false;
        }
    }

    public static bool ElementIsClickable(this IWebDriver driver, By by)
    {
        try
        {
            IWebElement element = driver.GetElement(by);
            return element.Displayed && element.Enabled;
        }
        catch
        {
            return false;
        }
    }

    public static bool ElementIsClickable(this IWebElement element, By by)
    {
        try
        {
            IWebElement element2 = element.GetElement(by);
            return element2.Displayed && element2.Enabled;
        }
        catch
        {
            return false;
        }
    }

    public static async Task WaitElementIsVisibleAsync(this IWebDriver driver, By by, int seconds = 15)
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

    public static void WaitElementIsVisible(this IWebDriver driver, By by, int seconds = 15)
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

    public static async Task WaitElementIsNotVisibleAsync(this IWebDriver driver, By by, int seconds = 15)
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
            catch (NoSuchElementException)
            {
                return;
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementIsNotVisibleAsync]");
    }

    public static void WaitElementIsNotVisible(this IWebDriver driver, By by, int seconds = 15)
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
            catch (NoSuchElementException)
            {
                return;
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementIsNotVisible]");
    }

    public static async Task WaitElementExistsAsync(this IWebDriver driver, By by, int seconds = 15)
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
    public static void WaitElementExists(this IWebDriver driver, By by, int seconds = 15)
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

    public static async Task WaitElementNotExistsAsync(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return;
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementNotExistsAsync]");
    }

    public static void WaitElementNotExists(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            try
            {
                driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                return;
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"Limit of {seconds} seconds exceeded in [WaitElementNotExists]");
    }

    public static async Task WaitElementClickableAsync(this IWebDriver driver, By by, int seconds = 15)
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

    public static void WaitElementClickable(this IWebDriver driver, By by, int seconds = 15)
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

    public static async Task WaitElementSelectedAsync(this IWebDriver driver, By by, int seconds = 15)
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

    public static void WaitElementSelected(this IWebDriver driver, By by, int seconds = 15)
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

    public static async Task WaitElementNotSelectedAsync(this IWebDriver driver, By by, int seconds = 15)
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

    public static void WaitElementNotSelected(this IWebDriver driver, By by, int seconds = 15)
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
    public static async Task WaitAlertContainsTextAsync(this IWebDriver driver, string text, bool accept = true, int seconds = 15)
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
            bodyText.AppendLine(driver.FindElement(By.CssSelector("body")).Text);

            ReadOnlyCollection<IWebElement> iframes = driver.FindElements(By.TagName("iframe"));

            foreach (IWebElement? iframe in iframes)
            {
                bodyText.AppendLine($"--------------------[IFRAME {iframes.IndexOf(iframe).ToString().PadLeft(2, '0')}]--------------------");
                driver.SwitchTo().Frame(iframe);
                bodyText.AppendLine(driver.FindElement(By.CssSelector("body")).Text);
                driver.SwitchTo().DefaultContent();
            }
            return bodyText.ToString();
        }
        else
        {
            return driver.FindElement(By.CssSelector("body")).Text;
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
    public static void Blur(this IWebElement element)
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
