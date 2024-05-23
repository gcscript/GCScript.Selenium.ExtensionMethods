using GCScript.Selenium.ExtensionMethods.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Text;

namespace GCScript.Selenium.ExtensionMethods;

public static class GCScriptExtensionMethods
{
    /// <summary>
    /// Returns the first element that matches the specified locator.
    /// </summary>
    /// <param name="driver">The WebDriver instance to use.</param>
    /// <param name="by">The locator to use to find the element.</param>
    /// <returns>The first element that matches the specified locator.</returns>
    public static IWebElement GetElement(this IWebDriver driver, By by) => driver.FindElement(by);

    /// <summary>
    /// Finds a descendant element of the given element that matches the specified locator.
    /// </summary>
    /// <param name="element">The parent element to search within.</param>
    /// <param name="by">The locator to use to find the element.</param>
    /// <returns>The first element that matches the specified locator.</returns>
    public static IWebElement GetElement(this IWebElement element, By by) => element.FindElement(by);
    public static ReadOnlyCollection<IWebElement> GetElements(this IWebDriver driver, By by) => driver.FindElements(by);
    public static ReadOnlyCollection<IWebElement> GetElements(this IWebElement element, By by) => element.FindElements(by);

    public static bool ElementExists(this IWebDriver driver, By by)
    {
        var el = driver.FindElements(by).FirstOrDefault();
        return el != null;
    }

    public static bool ElementExists(this IWebElement element, By by)
    {
        var el = element.FindElements(by).FirstOrDefault();
        return el != null;
    }

    public static bool ElementIsVisible(this IWebDriver driver, By by)
    {
        var el = driver.FindElements(by).FirstOrDefault();
        return el != null && el.Displayed;
    }

    public static bool ElementIsVisible(this IWebElement element, By by)
    {
        var el = element.FindElements(by).FirstOrDefault();
        return el != null && el.Displayed;
    }

    public static bool ElementIsSelected(this IWebDriver driver, By by)
    {
        var el = driver.FindElements(by).FirstOrDefault();
        return el != null && el.Selected;
    }

    public static bool ElementIsSelected(this IWebElement element, By by)
    {
        var el = element.FindElements(by).FirstOrDefault();
        return el != null && el.Selected;
    }

    public static bool ElementIsClickable(this IWebDriver driver, By by)
    {
        var el = driver.FindElements(by).FirstOrDefault();
        return el != null && el.Displayed && el.Enabled;
    }

    public static bool ElementIsClickable(this IWebElement element, By by)
    {
        var el = element.FindElements(by).FirstOrDefault();
        return el != null && el.Displayed && el.Enabled;
    }

    /// <summary>
    /// Waits for an element to become visible on the page.
    /// </summary>
    /// <param name="driver">The WebDriver instance to use.</param>
    /// <param name="by">The By locator to use to find the element.</param>
    /// <param name="seconds">The maximum number of seconds to wait. Default is 15.</param>
    /// <exception cref="Exception">Thrown if the element is not visible within the specified time limit.</exception>
    public static async Task WaitElementIsVisibleAsync(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.ElementIsVisible(by))
            {
                return;
            }
            await Task.Delay(1000);
        }
        throw new Exception($"E427422 - Limit of {seconds} seconds exceeded!");
    }

    /// <summary>
    /// Waits for an element to become visible on the page.
    /// </summary>
    /// <param name="driver">The WebDriver instance to use.</param>
    /// <param name="by">The By locator to use to find the element.</param>
    /// <param name="seconds">The maximum number of seconds to wait. Default is 15.</param>
    /// <exception cref="Exception">Thrown if the element is not visible within the specified time limit.</exception>
    public static void WaitElementIsVisible(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.ElementIsVisible(by))
            {
                return;
            }
            Thread.Sleep(1000);
        }
        throw new Exception($"E315708 - Limit of {seconds} seconds exceeded!");
    }

    /// <summary>
    /// Waits for an element to become not visible on the page.
    /// </summary>
    /// <param name="driver">The WebDriver instance to use.</param>
    /// <param name="by">The By locator to use to find the element.</param>
    /// <param name="seconds">The maximum number of seconds to wait. Default is 15.</param>
    /// <exception cref="Exception">Thrown if the element is still visible within the specified time limit.</exception>
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
        throw new Exception($"E783772 - Limit of {seconds} seconds exceeded!");
    }

    /// <summary>
    /// Waits for an element to become not visible on the page.
    /// </summary>
    /// <param name="driver">The WebDriver instance to use.</param>
    /// <param name="by">The By locator to use to find the element.</param>
    /// <param name="seconds">The maximum number of seconds to wait. Default is 15.</param>
    /// <exception cref="Exception">Thrown if the element is still visible within the specified time limit.</exception>
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
        throw new Exception($"E931829 - Limit of {seconds} seconds exceeded!");
    }

    public static async Task WaitElementExistsAsync(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.ElementExists(by))
            {
                return;
            }
            await Task.Delay(1000);
        }
        throw new Exception($"E483672 - Limit of {seconds} seconds exceeded!");
    }

    public static void WaitElementExists(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.ElementExists(by))
            {
                return;
            }
            Thread.Sleep(1000);
        }
        throw new Exception($"E688996 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E539720 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E866254 - Limit of {seconds} seconds exceeded!");
    }

    public static async Task WaitElementClickableAsync(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.ElementIsClickable(by))
            {
                return;
            }
            await Task.Delay(1000);
        }
        throw new Exception($"E487205 - Limit of {seconds} seconds exceeded!");
    }

    public static void WaitElementClickable(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.ElementIsClickable(by))
            {
                return;
            }
            Thread.Sleep(1000);
        }
        throw new Exception($"E789332 - Limit of {seconds} seconds exceeded!");
    }

    public static async Task WaitElementSelectedAsync(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.ElementIsSelected(by))
            {
                return;
            }
            await Task.Delay(1000);
        }
        throw new Exception($"E259225 - Limit of {seconds} seconds exceeded!");
    }

    public static void WaitElementSelected(this IWebDriver driver, By by, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++)
        {
            if (driver.ElementIsSelected(by))
            {
                return;
            }
            Thread.Sleep(1000);
        }
        throw new Exception($"E430945 - Limit of {seconds} seconds exceeded!");
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
            catch (NoSuchElementException)
            {
                return;
            }
            catch { }
            await Task.Delay(1000);
        }
        throw new Exception($"E646159 - Limit of {seconds} seconds exceeded!");
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
            catch (NoSuchElementException)
            {
                return;
            }
            catch { }
            Thread.Sleep(1000);
        }
        throw new Exception($"E746493 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E810014 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E552777 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E742918 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E289656 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E124073 - Limit of {seconds} seconds exceeded!");
    }
    public static void WaitPageSourceContainsText(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++) { try { if (driver.PageSourceContainsText(text)) { return; } } catch { } Thread.Sleep(1000); ; }
        throw new Exception($"E431408 - Limit of {seconds} seconds exceeded!");
    }
    public static async Task WaitPageSourceNotContainsTextAsync(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++) { try { if (driver.PageSourceNotContainsText(text)) { return; } } catch { } await Task.Delay(1000); }
        throw new Exception($"E636483 - Limit of {seconds} seconds exceeded!");
    }
    public static void WaitPageSourceNotContainsText(this IWebDriver driver, string text, int seconds = 15)
    {
        seconds = Math.Max(1, seconds);
        for (int i = 0; i < seconds; i++) { try { if (driver.PageSourceNotContainsText(text)) { return; } } catch { } Thread.Sleep(1000); ; }
        throw new Exception($"E466554 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception("Limit of {seconds} seconds exceeded in [WaitUrlContainsTextAsync]");
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
        throw new Exception($"E197852 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E491102 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E602603 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E503762 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E640042 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E480191 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E970518 - Limit of {seconds} seconds exceeded!");
    }

    public static string InnerText(this IWebElement element) => element.GetAttribute("innerText");
    public static string InnerHTML(this IWebElement element) => element.GetAttribute("innerHTML");
    public static void Focus(this IWebElement element)
    {
        IWebDriver driver = ((IWrapsDriver)element).WrappedDriver;
        driver.ExecuteJavaScript("arguments[0].focus();", element);
    }
    public static void RemoveFocus(this IWebElement element)
    {
        IWebDriver driver = ((IWrapsDriver)element).WrappedDriver;
        driver.ExecuteJavaScript("arguments[0].blur();", element);
    }

    public static void TriggerDomEvent(this IWebElement element, EDomEvent domEvent)
    {
        IWebDriver driver = ((IWrapsDriver)element).WrappedDriver;
        driver.ExecuteJavaScript($"arguments[0].dispatchEvent(new Event('{domEvent.ToString().ToLower()}'))", element);
    }

    public static void TriggerDomEvent(this IWebElement element, string eventName)
    {
        IWebDriver driver = ((IWrapsDriver)element).WrappedDriver;
        driver.ExecuteJavaScript($"arguments[0].dispatchEvent(new Event('{eventName.ToString().Trim().ToLower()}'))", element);
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
        throw new Exception($"E961468 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E783565 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E889477 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E474528 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E367675 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E783379 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E474861 - Limit of {seconds} seconds exceeded!");
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
        throw new Exception($"E461601 - Limit of {seconds} seconds exceeded!");
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
