using GCScript.Selenium.ExtensionMethods.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Text;

namespace GCScript.Selenium.ExtensionMethods;

public static class GCScriptExtensionMethods {
	private static IWebDriver GetWebDriver(this IWebElement element) => ((IWrapsDriver)element).WrappedDriver;

	/// <summary>
	/// Returns the first element that matches the specified locator.
	/// </summary>
	/// <param name="driver">The WebDriver instance to use.</param>
	/// <param name="by">The locator to use to find the element.</param>
	/// <returns>The first element that matches the specified locator.</returns>
	public static IWebElement GCS_GetElement(this IWebDriver driver, By by) => driver.FindElement(by);

	/// <summary>
	/// Finds a descendant element of the given element that matches the specified locator.
	/// </summary>
	/// <param name="element">The parent element to search within.</param>
	/// <param name="by">The locator to use to find the element.</param>
	/// <returns>The first element that matches the specified locator.</returns>
	public static IWebElement GCS_GetElement(this IWebElement element, By by) => element.FindElement(by);
	public static ReadOnlyCollection<IWebElement> GCS_GetElements(this IWebDriver driver, By by) => driver.FindElements(by);
	public static ReadOnlyCollection<IWebElement> GCS_GetElements(this IWebElement element, By by) => element.FindElements(by);
	public static void GCS_Click(this IWebElement element) => element.Click();
	public static void GCS_DoubleClick(this IWebElement element) => new Actions(element.GetWebDriver()).DoubleClick(element).Perform();
	public static void GCS_RightClick(this IWebElement element) => new Actions(element.GetWebDriver()).ContextClick(element).Perform();
	public static void GCS_Check(this IWebElement element) { if (!element.Selected) { element.Click(); } }
	public static void GCS_Uncheck(this IWebElement element) { if (element.Selected) { element.Click(); } }

	public static bool GCS_ElementExists(this IWebDriver driver, By by) {
		var el = driver.FindElements(by).FirstOrDefault();
		return el != null;
	}

	public static bool GCS_ElementExists(this IWebElement element, By by) {
		var el = element.FindElements(by).FirstOrDefault();
		return el != null;
	}

	public static bool GCS_ElementIsVisible(this IWebDriver driver, By by) {
		var el = driver.FindElements(by).FirstOrDefault();
		return el != null && el.Displayed;
	}

	public static bool GCS_ElementIsVisible(this IWebElement element, By by) {
		var el = element.FindElements(by).FirstOrDefault();
		return el != null && el.Displayed;
	}

	public static bool GCS_ElementIsSelected(this IWebDriver driver, By by) {
		var el = driver.FindElements(by).FirstOrDefault();
		return el != null && el.Selected;
	}

	public static bool GCS_ElementIsSelected(this IWebElement element, By by) {
		var el = element.FindElements(by).FirstOrDefault();
		return el != null && el.Selected;
	}

	public static bool GCS_ElementIsClickable(this IWebDriver driver, By by) {
		var el = driver.FindElements(by).FirstOrDefault();
		return el != null && el.Displayed && el.Enabled;
	}

	public static bool GCS_ElementIsClickable(this IWebElement element, By by) {
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
	public static async Task GCS_WaitElementIsVisibleAsync(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.GCS_ElementIsVisible(by)) {
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
	public static void GCS_WaitElementIsVisible(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.GCS_ElementIsVisible(by)) {
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
	public static async Task GCS_WaitElementIsNotVisibleAsync(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (!driver.FindElement(by).Displayed) {
					return;
				}
			}
			catch (NoSuchElementException) {
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
	public static void GCS_WaitElementIsNotVisible(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (!driver.FindElement(by).Displayed) {
					return;
				}
			}
			catch (NoSuchElementException) {
				return;
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new Exception($"E931829 - Limit of {seconds} seconds exceeded!");
	}

	public static async Task GCS_WaitElementExistsAsync(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.GCS_ElementExists(by)) {
				return;
			}
			await Task.Delay(1000);
		}
		throw new Exception($"E483672 - Limit of {seconds} seconds exceeded!");
	}

	public static void GCS_WaitElementExists(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.GCS_ElementExists(by)) {
				return;
			}
			Thread.Sleep(1000);
		}
		throw new Exception($"E688996 - Limit of {seconds} seconds exceeded!");
	}

	public static async Task GCS_WaitElementNotExistsAsync(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				driver.FindElement(by);
			}
			catch (NoSuchElementException) {
				return;
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new Exception($"E539720 - Limit of {seconds} seconds exceeded!");
	}

	public static void GCS_WaitElementNotExists(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				driver.FindElement(by);
			}
			catch (NoSuchElementException) {
				return;
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new Exception($"E866254 - Limit of {seconds} seconds exceeded!");
	}

	public static async Task GCS_WaitElementClickableAsync(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.GCS_ElementIsClickable(by)) {
				return;
			}
			await Task.Delay(1000);
		}
		throw new Exception($"E487205 - Limit of {seconds} seconds exceeded!");
	}

	public static void GCS_WaitElementClickable(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.GCS_ElementIsClickable(by)) {
				return;
			}
			Thread.Sleep(1000);
		}
		throw new Exception($"E789332 - Limit of {seconds} seconds exceeded!");
	}

	public static async Task GCS_WaitElementSelectedAsync(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.GCS_ElementIsSelected(by)) {
				return;
			}
			await Task.Delay(1000);
		}
		throw new Exception($"E259225 - Limit of {seconds} seconds exceeded!");
	}

	public static void GCS_WaitElementSelected(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.GCS_ElementIsSelected(by)) {
				return;
			}
			Thread.Sleep(1000);
		}
		throw new Exception($"E430945 - Limit of {seconds} seconds exceeded!");
	}

	public static async Task GCS_WaitElementNotSelectedAsync(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (!driver.FindElement(by).Selected) {
					return;
				}
			}
			catch (NoSuchElementException) {
				return;
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new Exception($"E646159 - Limit of {seconds} seconds exceeded!");
	}

	public static void GCS_WaitElementNotSelected(this IWebDriver driver, By by, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (!driver.FindElement(by).Selected) {
					return;
				}
			}
			catch (NoSuchElementException) {
				return;
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new Exception($"E746493 - Limit of {seconds} seconds exceeded!");
	}

	public static string GCS_GetAlertText(this IWebDriver driver, int seconds = 15) => driver.SwitchTo().Alert().Text;
	public static async Task GCS_WaitAlertAsync(this IWebDriver driver, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				driver.SwitchTo().Alert(); return;
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new Exception($"E810014 - Limit of {seconds} seconds exceeded!");
	}
	public static async Task<string> GCS_WaitAlertAndGetTextAsync(this IWebDriver driver, int seconds = 15) {
		await driver.GCS_WaitAlertAsync(seconds);
		return driver.GCS_GetAlertText();
	}
	public static async Task GCS_WaitAlertContainsTextAsync(this IWebDriver driver, string text, bool accept = true, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCS_GetAlertText().Contains(text, StringComparison.OrdinalIgnoreCase)) {
					return;
				}
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new Exception($"E552777 - Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCS_WaitAlertContainsTextAndAcceptAsync(this IWebDriver driver, string text, int seconds = 15) { await driver.GCS_WaitAlertContainsTextAsync(text); driver.SwitchTo().Alert().Accept(); }
	public static async Task GCS_WaitAlertContainsTextAndDismissAsync(this IWebDriver driver, string text, int seconds = 15) { await driver.GCS_WaitAlertContainsTextAsync(text); driver.SwitchTo().Alert().Dismiss(); }
	public static async Task GCS_WaitAlertAndAcceptAsync(this IWebDriver driver, int seconds = 15) { await driver.GCS_WaitAlertAsync(seconds); driver.SwitchTo().Alert().Accept(); }
	public static async Task GCS_WaitAlertAndDismissAsync(this IWebDriver driver, int seconds = 15) { await driver.GCS_WaitAlertAsync(seconds); driver.SwitchTo().Alert().Dismiss(); }

	public static void GCS_WaitAlert(this IWebDriver driver, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				driver.SwitchTo().Alert(); return;
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new Exception($"E742918 - Limit of {seconds} seconds exceeded!");
	}
	public static string GCS_WaitAlertAndGetText(this IWebDriver driver, int seconds = 15) {
		driver.GCS_WaitAlert(seconds);
		return driver.GCS_GetAlertText();
	}
	public static void GCS_WaitAlertContainsText(this IWebDriver driver, string text, bool accept = true, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCS_GetAlertText().Contains(text, StringComparison.OrdinalIgnoreCase)) {
					return;
				}
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new Exception($"E289656 - Limit of {seconds} seconds exceeded!");
	}
	public static void GCS_WaitAlertContainsTextAndAccept(this IWebDriver driver, string text, int seconds = 15) { driver.GCS_WaitAlertContainsText(text); driver.SwitchTo().Alert().Accept(); }
	public static void GCS_WaitAlertContainsTextAndDismiss(this IWebDriver driver, string text, int seconds = 15) { driver.GCS_WaitAlertContainsText(text); driver.SwitchTo().Alert().Dismiss(); }
	public static void GCS_WaitAlertAndAccept(this IWebDriver driver, int seconds = 15) { driver.GCS_WaitAlert(seconds); driver.SwitchTo().Alert().Accept(); }
	public static void GCS_WaitAlertAndDismiss(this IWebDriver driver, int seconds = 15) { driver.GCS_WaitAlert(seconds); driver.SwitchTo().Alert().Dismiss(); }

	public static bool GCS_PageSourceContainsText(this IWebDriver driver, string text) => driver.PageSource.Contains(text, StringComparison.OrdinalIgnoreCase);
	public static bool GCS_PageSourceNotContainsText(this IWebDriver driver, string text) => !driver.PageSource.Contains(text, StringComparison.OrdinalIgnoreCase);
	public static async Task GCS_WaitPageSourceContainsTextAsync(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) { try { if (driver.GCS_PageSourceContainsText(text)) { return; } } catch { } await Task.Delay(1000); }
		throw new Exception($"E124073 - Limit of {seconds} seconds exceeded!");
	}
	public static void GCS_WaitPageSourceContainsText(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) { try { if (driver.GCS_PageSourceContainsText(text)) { return; } } catch { } Thread.Sleep(1000); ; }
		throw new Exception($"E431408 - Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCS_WaitPageSourceNotContainsTextAsync(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) { try { if (driver.GCS_PageSourceNotContainsText(text)) { return; } } catch { } await Task.Delay(1000); }
		throw new Exception($"E636483 - Limit of {seconds} seconds exceeded!");
	}
	public static void GCS_WaitPageSourceNotContainsText(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) { try { if (driver.GCS_PageSourceNotContainsText(text)) { return; } } catch { } Thread.Sleep(1000); ; }
		throw new Exception($"E466554 - Limit of {seconds} seconds exceeded!");
	}

	public static bool GCS_UrlContainsText(this IWebDriver driver, string text) => driver.Url.Contains(text, StringComparison.OrdinalIgnoreCase);
	public static bool GCS_UrlNotContainsText(this IWebDriver driver, string text) => !driver.Url.Contains(text, StringComparison.OrdinalIgnoreCase);
	public static async Task GCS_WaitUrlContainsTextAsync(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCS_UrlContainsText(text)) {
					return;
				}
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new Exception($"E133345 - Limit of {seconds} seconds exceeded!");
	}
	public static void GCS_WaitUrlContainsText(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCS_UrlContainsText(text)) {
					return;
				}
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new Exception($"E197852 - Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCS_WaitUrlNotContainsTextAsync(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCS_UrlNotContainsText(text)) {
					return;
				}
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new Exception($"E491102 - Limit of {seconds} seconds exceeded!");
	}
	public static void GCS_WaitUrlNotContainsText(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCS_UrlNotContainsText(text)) {
					return;
				}
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new Exception($"E602603 - Limit of {seconds} seconds exceeded!");
	}

	public static string GCS_GetBodyText(this IWebDriver driver, bool full = true) {
		if (full) {
			StringBuilder bodyText = new();
			bodyText.AppendLine(driver.FindElement(By.CssSelector("body")).Text);

			ReadOnlyCollection<IWebElement> iframes = driver.FindElements(By.TagName("iframe"));

			foreach (IWebElement? iframe in iframes) {
				bodyText.AppendLine($"--------------------[IFRAME {iframes.IndexOf(iframe).ToString().PadLeft(2, '0')}]--------------------");
				driver.SwitchTo().Frame(iframe);
				bodyText.AppendLine(driver.FindElement(By.CssSelector("body")).Text);
				driver.SwitchTo().DefaultContent();
			}
			return bodyText.ToString();
		}
		else {
			return driver.FindElement(By.CssSelector("body")).Text;
		}
	}
	public static bool GCS_BodyContainsText(this IWebDriver driver, string text, bool full = true) => driver.GCS_GetBodyText(full).Contains(text, StringComparison.OrdinalIgnoreCase);
	public static bool GCS_BodyNotContainsText(this IWebDriver driver, string text, bool full = true) => !driver.GCS_GetBodyText(full).Contains(text, StringComparison.OrdinalIgnoreCase);
	public static async Task GCS_WaitBodyContainsTextAsync(this IWebDriver driver, string text, int seconds = 15, bool full = true) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCS_BodyContainsText(text, full)) { return; }
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new Exception($"E503762 - Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCS_WaitBodyNotContainsTextAsync(this IWebDriver driver, string text, int seconds = 15, bool full = true) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCS_BodyNotContainsText(text, full)) {
					return;
				}
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new Exception($"E640042 - Limit of {seconds} seconds exceeded!");
	}
	public static void GCS_WaitBodyContainsText(this IWebDriver driver, string text, int seconds = 15, bool full = true) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCS_BodyContainsText(text, full)) { return; }
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new Exception($"E480191 - Limit of {seconds} seconds exceeded!");
	}
	public static void GCS_WaitBodyNotContainsText(this IWebDriver driver, string text, int seconds = 15, bool full = true) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCS_BodyNotContainsText(text, full)) {
					return;
				}
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new Exception($"E970518 - Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCS_WaitForTextToBePresentInElementAsync(this IWebDriver driver, By by, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.FindElement(by).Text.Contains(text, StringComparison.OrdinalIgnoreCase)) { return; }
			await Task.Delay(1000);
		}
		throw new Exception($"E764237 - Limit of {seconds} seconds exceeded!");
	}
	public static void GCS_WaitForTextToBePresentInElement(this IWebDriver driver, By by, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.FindElement(by).Text.Contains(text, StringComparison.OrdinalIgnoreCase)) { return; }
			Thread.Sleep(1000);
		}
		throw new Exception($"E168084 - Limit of {seconds} seconds exceeded!");
	}

	public static string GCS_InnerText(this IWebElement element) => element.GetAttribute("innerText");
	public static string GCS_InnerHTML(this IWebElement element) => element.GetAttribute("innerHTML");
	public static void GCS_Focus(this IWebElement element) {
		element.GetWebDriver().ExecuteJavaScript("arguments[0].focus();", element);
	}
	public static void GCS_RemoveFocus(this IWebElement element) {
		element.GetWebDriver().ExecuteJavaScript("arguments[0].blur();", element);
	}

	public static void GCS_TriggerDomEvent(this IWebElement element, EDomEvent domEvent) {
		element.GetWebDriver().ExecuteJavaScript($"arguments[0].dispatchEvent(new Event('{domEvent.ToString().ToLower()}'))", element);
	}

	public static void GCS_TriggerDomEvent(this IWebElement element, string eventName) {
		element.GetWebDriver().ExecuteJavaScript($"arguments[0].dispatchEvent(new Event('{eventName.ToString().Trim().ToLower()}'))", element);
	}

	public static async Task GCS_WaitForPageToLoadAsync(this IWebDriver driver, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.ExecuteJavaScript<bool>("return document.readyState === 'complete';")) {
				return;
			}
			await Task.Delay(1000);
		}
		throw new Exception($"E961468 - Limit of {seconds} seconds exceeded!");
	}
	public static void GCS_WaitForPageToLoad(this IWebDriver driver, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.ExecuteJavaScript<bool>("return document.readyState === 'complete';")) {
				return;
			}
			Thread.Sleep(1000);
		}
		throw new Exception($"E783565 - Limit of {seconds} seconds exceeded!");
	}
	public static void GCS_OverrideAlertConfirmTrue(this IWebDriver driver) => driver.ExecuteJavaScript("window.confirm = function () { return true; };");

	public static void GCS_FindWindowContainsTextInUrl(this IWebDriver driver, string text) {
		foreach (string window in driver.WindowHandles) {
			driver.SwitchTo().Window(window);
			if (driver.Url.Contains(text, StringComparison.OrdinalIgnoreCase)) {
				return;
			}
		}
		throw new Exception($"Unable to find window containing text [{text}] in URL");
	}
	public static void GCS_FindWindowContainsTextInTitle(this IWebDriver driver, string text) {
		foreach (string window in driver.WindowHandles) {
			driver.SwitchTo().Window(window);
			if (driver.Title.Contains(text, StringComparison.OrdinalIgnoreCase)) {
				return;
			}
		}
		throw new Exception($"Unable to find window containing text [{text}] in Title");
	}
	public static void GCS_FindWindowStartsWithTextInTitle(this IWebDriver driver, string text) {
		foreach (string window in driver.WindowHandles) {
			driver.SwitchTo().Window(window);
			if (driver.Title.StartsWith(text, StringComparison.OrdinalIgnoreCase)) {
				return;
			}
		}
		throw new Exception($"Unable to find window starting with text [{text}] in Title");
	}
	public static void GCS_FindWindowEndsWithTextInTitle(this IWebDriver driver, string text) {
		foreach (string window in driver.WindowHandles) {
			driver.SwitchTo().Window(window);
			if (driver.Title.EndsWith(text, StringComparison.OrdinalIgnoreCase)) {
				return;
			}
		}
		throw new Exception($"Unable to find window ending with text [{text}] in Title");
	}

	public static async Task GCS_WaitWindowHandlesCountAsync(this IWebDriver driver, int count, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.WindowHandles.Count == count) {
				return;
			}
			await Task.Delay(1000);
		}
		throw new Exception($"E889477 - Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCS_WaitWindowHandlesCountMoreThanAsync(this IWebDriver driver, int count, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.WindowHandles.Count > count) {
				return;
			}
			await Task.Delay(1000);
		}
		throw new Exception($"E474528 - Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCS_WaitWindowHandlesCountLessThanAsync(this IWebDriver driver, int count, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.WindowHandles.Count < count) {
				return;
			}
			await Task.Delay(1000);
		}
		throw new Exception($"E367675 - Limit of {seconds} seconds exceeded!");
	}

	public static void GCS_WaitWindowHandlesCount(this IWebDriver driver, int count, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.WindowHandles.Count == count) {
				return;
			}
			Thread.Sleep(1000);
		}
		throw new Exception($"E783379 - Limit of {seconds} seconds exceeded!");
	}
	public static void GCS_WaitWindowHandlesCountMoreThan(this IWebDriver driver, int count, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.WindowHandles.Count > count) {
				return;
			}
			Thread.Sleep(1000);
		}
		throw new Exception($"E474861 - Limit of {seconds} seconds exceeded!");
	}
	public static void GCS_WaitWindowHandlesCountLessThan(this IWebDriver driver, int count, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.WindowHandles.Count < count) {
				return;
			}
			Thread.Sleep(1000);
		}
		throw new Exception($"E461601 - Limit of {seconds} seconds exceeded!");
	}

	public static void GCS_NavigateToUrl(this IWebDriver driver, string url) => driver.Navigate().GoToUrl(url);
	public static void GCS_NavigateToUrlWithJS(this IWebDriver driver, string url) => driver.ExecuteJavaScript($"window.location.href = '{url}';");
	public static void GCS_RefreshPageWithJS(this IWebDriver driver, bool reloadFromServer = true) => driver.ExecuteJavaScript($"window.location.reload({(reloadFromServer ? "true" : "")});");

	public static void GCS_SelectIndexInDropdown(this IWebElement element, int index) => new SelectElement(element).SelectByIndex(index);
	public static void GCS_SelectTextInDropdown(this IWebElement element, string text) => new SelectElement(element).SelectByText(text);
	public static void GCS_SelectValueInDropdown(this IWebElement element, string value) => new SelectElement(element).SelectByValue(value);

	public static string GCS_GetValueWithJS(this IWebElement element) {
		return element.GetWebDriver().ExecuteJavaScript<string>("return arguments[0].value;", element);
	}
	public static void GCS_SetValueWithJS(this IWebElement element, string value) {
		element.GetWebDriver().ExecuteJavaScript("arguments[0].value = arguments[1];", element, value);
	}

	public static void GCS_SetAttributeWithJS(this IWebDriver driver, IWebElement element, string attributeName, string value) => driver.ExecuteJavaScript($"arguments[0].setAttribute('{attributeName}', '{value}');", element);

	public static string GCS_GetUserAgentWithJS(this IWebDriver driver) => driver.ExecuteJavaScript<string>("return navigator.userAgent;");

	public static void GCS_ForceClose(this IWebDriver driver) { try { driver.Close(); } catch { } try { driver.Quit(); } catch { } try { driver.Dispose(); } catch { } }
	public static void GCS_SavePageSource(this IWebDriver driver, string filePath) => File.WriteAllText(filePath, driver.PageSource);

	public static async Task GCS_WaitSAsync(this IWebDriver driver, int seconds) { seconds = Math.Max(1, seconds); await Task.Delay(TimeSpan.FromSeconds(seconds)); }
	public static void GCS_WaitS(this IWebDriver driver, int seconds) { seconds = Math.Max(1, seconds); Thread.Sleep(TimeSpan.FromSeconds(seconds)); }
	public static async Task GCS_WaitMsAsync(this IWebDriver driver, int milliseconds) { milliseconds = Math.Max(1, milliseconds); await Task.Delay(milliseconds); }
	public static void GCS_WaitMs(this IWebDriver driver, int milliseconds) { milliseconds = Math.Max(1, milliseconds); Thread.Sleep(milliseconds); }
	public static async Task GCS_WaitMAsync(this IWebDriver driver, int seconds) { seconds = Math.Max(1, seconds); await Task.Delay(TimeSpan.FromMinutes(seconds)); }
	public static void GCS_WaitM(this IWebDriver driver, int seconds) { seconds = Math.Max(1, seconds); Thread.Sleep(TimeSpan.FromMinutes(seconds)); }

	public static int GCS_GetPageZoom(this IWebDriver driver) => int.Parse(driver.ExecuteJavaScript<string>("return document.body.style.zoom;").Replace("%", ""));
	public static void GCS_SetPageZoom(this IWebDriver driver, int percent = 100) => driver.ExecuteJavaScript($"document.body.style.zoom = '{Math.Clamp(percent, 1, 100)}%';");
	public static void GCS_SetZoom(this IWebElement element, int percent = 100, int originX = 50, int originY = 50) {
		percent = Math.Clamp(percent, 1, 400);
		originX = Math.Clamp(originX, 0, 100);
		originY = Math.Clamp(originY, 0, 100);

		var scale = $"{percent / 100.0:0.00}".Replace(",", ".");
		var origin = $"{originX}% {originY}%";
		element.GetWebDriver().ExecuteJavaScript($"arguments[0].style.transform = 'scale({scale})'; arguments[0].style.transformOrigin = '{origin}';", element);
	}

	public static void GCS_Remove(this IWebElement element) {
		element.GetWebDriver().ExecuteJavaScript("arguments[0].remove();", element);
	}
	public static void GCS_RemoveAttribute(this IWebElement element, string attributeName) {
		element.GetWebDriver().ExecuteJavaScript($"arguments[0].removeAttribute('{attributeName}');", element);
	}

	public static void GCS_DragAndDrop(this IWebDriver driver, IWebElement source, IWebElement target) => new Actions(driver).DragAndDrop(source, target).Perform();
	public static void GCS_ScrollToElement(this IWebElement element, bool alignToTop = true) => element.GetWebDriver().ExecuteJavaScript($"arguments[0].scrollIntoView({(alignToTop ? "true" : "")});", element);
	public static void GCS_WindowScrollTo(this IWebDriver driver, int x, int y) => driver.ExecuteJavaScript($"window.scrollTo({x}, {y});");
	public static void GCS_WindowScrollToTop(this IWebDriver driver) => driver.ExecuteJavaScript("window.scrollTo(0, 0);");
	public static void GCS_WindowScrollToElement(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript($"window.scrollTo({element.Location.X}, {element.Location.Y});");
	public static void GCS_WindowScrollToElementY(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript($"window.scrollTo(0, {element.Location.Y});");
	public static void GCS_WindowScrollToElementX(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript($"window.scrollTo({element.Location.X}, 0);");

	public static Screenshot GCS_GetScreenshot(this IWebDriver driver) => driver.TakeScreenshot();
	public static Screenshot GCS_GetScreenshot(this IWebElement element) => ((ITakesScreenshot)element).GetScreenshot();
	public static void GCS_SaveScreenshot(this IWebDriver driver, string filePath) => driver.GCS_GetScreenshot().SaveAsFile(filePath);
	public static void GCS_SaveScreenshot(this IWebElement element, string filePath) => element.GCS_GetScreenshot().SaveAsFile(filePath);
	public static void GCS_Hide(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript("arguments[0].style.display = 'none';", element);
	public static void GCS_Show(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript("arguments[0].style.display = '';", element);
	public static void GCS_ShowBlock(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript("arguments[0].style.display = 'block';", element);
	public static void GCS_ShowFlex(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript("arguments[0].style.display = 'flex';", element);
	public static void GCS_SetStyle(this IWebElement element, string style) => element.GetWebDriver().ExecuteJavaScript($"arguments[0].style = '{style}';", element);
}
