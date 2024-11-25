using GCScript.Common.Exceptions;
using GCScript.Selenium.ExtensionMethods.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace GCScript.Selenium.ExtensionMethods;

public static class GCScriptExtensionMethods {
	private static IWebDriver GetWebDriver(this IWebElement element) => ((IWrapsDriver)element).WrappedDriver;
	private static IWebDriver GetWebDriver(this ISearchContext context) => ((IWrapsDriver)context).WrappedDriver;

	public static IWebElement GCSGetElement(this IWebDriver driver, By by) => driver.FindElement(by);
	public static IWebElement GCSGetElement(this IWebElement element, By by) => element.FindElement(by);
	public static IWebElement GCSGetElement(this ISearchContext context, By by) => context.FindElement(by);

	public static ReadOnlyCollection<IWebElement> GCSGetElements(this IWebDriver driver, By by) => driver.FindElements(by);
	public static ReadOnlyCollection<IWebElement> GCSGetElements(this IWebElement element, By by) => element.FindElements(by);
	public static ReadOnlyCollection<IWebElement> GCSGetElements(this ISearchContext context, By by) => context.FindElements(by);

	public static bool GCSElementExists(this IWebDriver driver, By by) => driver.GCSGetElements(by).Any();
	public static bool GCSElementExists(this IWebElement element, By by) => element.GCSGetElements(by).Any();
	public static bool GCSElementExists(this ISearchContext context, By by) => context.GCSGetElements(by).Any();
	public static bool GCSElementIsVisible(this IWebDriver driver, By by) => driver.GCSGetElements(by).FirstOrDefault()?.Displayed ?? false;
	public static bool GCSElementIsVisible(this IWebElement element, By by) => element.GCSGetElements(by).FirstOrDefault()?.Displayed ?? false;
	public static bool GCSElementIsVisible(this ISearchContext context, By by) => context.GCSGetElements(by).FirstOrDefault()?.Displayed ?? false;
	public static bool GCSElementIsSelected(this IWebDriver driver, By by) => driver.GCSGetElements(by).FirstOrDefault()?.Selected ?? false;
	public static bool GCSElementIsSelected(this IWebElement element, By by) => element.GCSGetElements(by).FirstOrDefault()?.Selected ?? false;
	public static bool GCSElementIsSelected(this ISearchContext context, By by) => context.GCSGetElements(by).FirstOrDefault()?.Selected ?? false;
	public static bool GCSElementIsClickable(this IWebDriver driver, By by) { var elm = driver.GCSGetElements(by).FirstOrDefault(); return elm != null && elm.Displayed && elm.Enabled; }
	public static bool GCSElementIsClickable(this IWebElement element, By by) { var elm = element.GCSGetElements(by).FirstOrDefault(); return elm != null && elm.Displayed && elm.Enabled; }
	public static bool GCSElementIsClickable(this ISearchContext context, By by) { var elm = context.GCSGetElements(by).FirstOrDefault(); return elm != null && elm.Displayed && elm.Enabled; }

	public static void GCSClick(this IWebElement element) => element.Click();
	public static void GCSClickWithActions(this IWebElement element) => new Actions(element.GetWebDriver()).Click(element).Perform();
	public static void GCSDoubleClickWithActions(this IWebElement element) => new Actions(element.GetWebDriver()).DoubleClick(element).Perform();
	public static void GCSRightClickWithActions(this IWebElement element) => new Actions(element.GetWebDriver()).ContextClick(element).Perform();
	public static void GCSCheck(this IWebElement element) { if (!element.Selected) { element.Click(); } }
	public static void GCSUncheck(this IWebElement element) { if (element.Selected) { element.Click(); } }

	public static void GCSWaitElementExists(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (driver.GCSElementExists(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(175908, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static void GCSWaitElementNotExists(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!driver.GCSElementExists(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(720412, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}
	public static void GCSWaitElementExists(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (context.FindElements(by).Count > 0) {
				return;
			}

			Thread.Sleep(500);
		}
		throw new GCScriptException(508488, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static void GCSWaitElementNotExists(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!context.GCSElementExists(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(649047, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementExistsAsync(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (driver.GCSElementExists(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(401561, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementNotExistsAsync(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!driver.GCSElementExists(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(534620, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementExistsAsync(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (context.FindElements(by).Count > 0) {
				return;
			}

			await Task.Delay(500);
		}
		throw new GCScriptException(785089, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementNotExistsAsync(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!context.GCSElementExists(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(299271, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}

	public static void GCSWaitElementIsVisible(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (driver.GCSElementIsVisible(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(200365, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static void GCSWaitElementIsNotVisible(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!driver.GCSElementIsVisible(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(208063, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}
	public static void GCSWaitElementIsVisible(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (context.GCSElementIsVisible(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(756200, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static void GCSWaitElementIsNotVisible(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!context.GCSElementIsVisible(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(194285, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementIsVisibleAsync(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (driver.GCSElementIsVisible(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(655197, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementIsNotVisibleAsync(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!driver.GCSElementIsVisible(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(334241, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementIsVisibleAsync(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (context.GCSElementIsVisible(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(138797, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementIsNotVisibleAsync(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!context.GCSElementIsVisible(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(910390, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}

	public static void GCSWaitElementSelected(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (driver.GCSElementIsSelected(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(766917, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static void GCSWaitElementNotSelected(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!driver.GCSElementIsSelected(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(201214, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}
	public static void GCSWaitElementSelected(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (context.GCSElementIsSelected(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(646266, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static void GCSWaitElementNotSelected(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!context.GCSElementIsSelected(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(563466, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementSelectedAsync(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (driver.GCSElementIsSelected(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(986538, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementNotSelectedAsync(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!driver.GCSElementIsSelected(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(704294, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementSelectedAsync(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (context.GCSElementIsSelected(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(747914, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementNotSelectedAsync(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!context.GCSElementIsSelected(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(156831, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}

	public static void GCSWaitElementClickable(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (driver.GCSElementIsClickable(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(999901, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static void GCSWaitElementNotClickable(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!driver.GCSElementIsClickable(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(714055, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}
	public static void GCSWaitElementClickable(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (context.GCSElementIsClickable(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(857501, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static void GCSWaitElementNotClickable(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!context.GCSElementIsClickable(by)) {
				return;
			}
			Thread.Sleep(500);
		}
		throw new GCScriptException(804463, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementClickableAsync(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (driver.GCSElementIsClickable(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(889017, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementNotClickableAsync(this IWebDriver driver, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!driver.GCSElementIsClickable(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(895554, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementClickableAsync(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (context.GCSElementIsClickable(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(402656, $"The locator '{by}' was not found within {timeout.TotalSeconds} seconds.");
	}
	public static async Task GCSWaitElementNotClickableAsync(this ISearchContext context, By by, int seconds = 15) {
		var timeout = TimeSpan.FromSeconds(Math.Max(1, seconds));
		var stopwatch = Stopwatch.StartNew();
		while (stopwatch.Elapsed < timeout) {
			if (!context.GCSElementIsClickable(by)) {
				return;
			}
			await Task.Delay(500);
		}
		throw new GCScriptException(695998, $"The locator '{by}' was found within {timeout.TotalSeconds} seconds.");
	}

	public static IAlert GCSGetAlert(this IWebDriver driver) => driver.SwitchTo().Alert();
	public static string GCSGetAlertText(this IWebDriver driver) => driver.GCSGetAlert().Text;

	//=================================================[DEPRECATED]=================================================

	public static async Task GCSWaitAlertAsync(this IWebDriver driver, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				driver.SwitchTo().Alert(); return;
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new GCScriptException(835741, $"Limit of {seconds} seconds exceeded!");
	}
	public static async Task<string> GCSWaitAlertAndGetTextAsync(this IWebDriver driver, int seconds = 15) {
		await driver.GCSWaitAlertAsync(seconds);
		return driver.GCSGetAlertText();
	}
	public static async Task GCSWaitAlertContainsTextAsync(this IWebDriver driver, string text, bool accept = true, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCSGetAlertText().Contains(text, StringComparison.OrdinalIgnoreCase)) {
					return;
				}
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new GCScriptException(535001, $"Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCSWaitAlertContainsTextAndAcceptAsync(this IWebDriver driver, string text, int seconds = 15) { await driver.GCSWaitAlertContainsTextAsync(text); driver.SwitchTo().Alert().Accept(); }
	public static async Task GCSWaitAlertContainsTextAndDismissAsync(this IWebDriver driver, string text, int seconds = 15) { await driver.GCSWaitAlertContainsTextAsync(text); driver.SwitchTo().Alert().Dismiss(); }
	public static async Task GCSWaitAlertAndAcceptAsync(this IWebDriver driver, int seconds = 15) { await driver.GCSWaitAlertAsync(seconds); driver.SwitchTo().Alert().Accept(); }
	public static async Task GCSWaitAlertAndDismissAsync(this IWebDriver driver, int seconds = 15) { await driver.GCSWaitAlertAsync(seconds); driver.SwitchTo().Alert().Dismiss(); }

	public static void GCSWaitAlert(this IWebDriver driver, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				driver.SwitchTo().Alert(); return;
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new GCScriptException(884586, $"Limit of {seconds} seconds exceeded!");
	}
	public static string GCSWaitAlertAndGetText(this IWebDriver driver, int seconds = 15) {
		driver.GCSWaitAlert(seconds);
		return driver.GCSGetAlertText();
	}
	public static void GCSWaitAlertContainsText(this IWebDriver driver, string text, bool accept = true, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCSGetAlertText().Contains(text, StringComparison.OrdinalIgnoreCase)) {
					return;
				}
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new GCScriptException(274987, $"Limit of {seconds} seconds exceeded!");
	}
	public static void GCSWaitAlertContainsTextAndAccept(this IWebDriver driver, string text, int seconds = 15) { driver.GCSWaitAlertContainsText(text); driver.SwitchTo().Alert().Accept(); }
	public static void GCSWaitAlertContainsTextAndDismiss(this IWebDriver driver, string text, int seconds = 15) { driver.GCSWaitAlertContainsText(text); driver.SwitchTo().Alert().Dismiss(); }
	public static void GCSWaitAlertAndAccept(this IWebDriver driver, int seconds = 15) { driver.GCSWaitAlert(seconds); driver.SwitchTo().Alert().Accept(); }
	public static void GCSWaitAlertAndDismiss(this IWebDriver driver, int seconds = 15) { driver.GCSWaitAlert(seconds); driver.SwitchTo().Alert().Dismiss(); }

	public static bool GCSPageSourceContainsText(this IWebDriver driver, string text) => driver.PageSource.Contains(text, StringComparison.OrdinalIgnoreCase);
	public static bool GCSPageSourceNotContainsText(this IWebDriver driver, string text) => !driver.PageSource.Contains(text, StringComparison.OrdinalIgnoreCase);
	public static async Task GCSWaitPageSourceContainsTextAsync(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) { try { if (driver.GCSPageSourceContainsText(text)) { return; } } catch { } await Task.Delay(1000); }
		throw new GCScriptException(539414, $"Limit of {seconds} seconds exceeded!");
	}
	public static void GCSWaitPageSourceContainsText(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) { try { if (driver.GCSPageSourceContainsText(text)) { return; } } catch { } Thread.Sleep(1000); ; }
		throw new GCScriptException(467664, $"Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCSWaitPageSourceNotContainsTextAsync(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) { try { if (driver.GCSPageSourceNotContainsText(text)) { return; } } catch { } await Task.Delay(1000); }
		throw new GCScriptException(609644, $"Limit of {seconds} seconds exceeded!");
	}
	public static void GCSWaitPageSourceNotContainsText(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) { try { if (driver.GCSPageSourceNotContainsText(text)) { return; } } catch { } Thread.Sleep(1000); ; }
		throw new GCScriptException(333662, $"Limit of {seconds} seconds exceeded!");
	}

	public static bool GCSUrlContainsText(this IWebDriver driver, string text) => driver.Url.Contains(text, StringComparison.OrdinalIgnoreCase);
	public static bool GCSUrlNotContainsText(this IWebDriver driver, string text) => !driver.Url.Contains(text, StringComparison.OrdinalIgnoreCase);
	public static async Task GCSWaitUrlContainsTextAsync(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCSUrlContainsText(text)) {
					return;
				}
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new GCScriptException(861965, $"Limit of {seconds} seconds exceeded!");
	}
	public static void GCSWaitUrlContainsText(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCSUrlContainsText(text)) {
					return;
				}
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new GCScriptException(738394, $"Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCSWaitUrlNotContainsTextAsync(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCSUrlNotContainsText(text)) {
					return;
				}
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new GCScriptException(187387, $"Limit of {seconds} seconds exceeded!");
	}
	public static void GCSWaitUrlNotContainsText(this IWebDriver driver, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCSUrlNotContainsText(text)) {
					return;
				}
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new GCScriptException(647194, $"Limit of {seconds} seconds exceeded!");
	}

	public static string GCSGetBodyText(this IWebDriver driver, bool full = true) {
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
	public static bool GCSBodyContainsText(this IWebDriver driver, string text, bool full = true) => driver.GCSGetBodyText(full).Contains(text, StringComparison.OrdinalIgnoreCase);
	public static bool GCSBodyNotContainsText(this IWebDriver driver, string text, bool full = true) => !driver.GCSGetBodyText(full).Contains(text, StringComparison.OrdinalIgnoreCase);
	public static async Task GCSWaitBodyContainsTextAsync(this IWebDriver driver, string text, int seconds = 15, bool full = true) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCSBodyContainsText(text, full)) { return; }
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new GCScriptException(663106, $"Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCSWaitBodyNotContainsTextAsync(this IWebDriver driver, string text, int seconds = 15, bool full = true) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCSBodyNotContainsText(text, full)) {
					return;
				}
			}
			catch { }
			await Task.Delay(1000);
		}
		throw new GCScriptException(874231, $"Limit of {seconds} seconds exceeded!");
	}
	public static void GCSWaitBodyContainsText(this IWebDriver driver, string text, int seconds = 15, bool full = true) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCSBodyContainsText(text, full)) { return; }
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new GCScriptException(912405, $"Limit of {seconds} seconds exceeded!");
	}
	public static void GCSWaitBodyNotContainsText(this IWebDriver driver, string text, int seconds = 15, bool full = true) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			try {
				if (driver.GCSBodyNotContainsText(text, full)) {
					return;
				}
			}
			catch { }
			Thread.Sleep(1000);
		}
		throw new GCScriptException(171098, $"Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCSWaitForTextToBePresentInElementAsync(this IWebDriver driver, By by, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.FindElement(by).Text.Contains(text, StringComparison.OrdinalIgnoreCase)) { return; }
			await Task.Delay(1000);
		}
		throw new GCScriptException(468039, $"Limit of {seconds} seconds exceeded!");
	}
	public static void GCSWaitForTextToBePresentInElement(this IWebDriver driver, By by, string text, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.FindElement(by).Text.Contains(text, StringComparison.OrdinalIgnoreCase)) { return; }
			Thread.Sleep(1000);
		}
		throw new GCScriptException(945887, $"Limit of {seconds} seconds exceeded!");
	}

	public static string GCSInnerText(this IWebElement element) => element.GetAttribute("innerText");
	public static string GCSInnerHTML(this IWebElement element) => element.GetAttribute("innerHTML");
	public static void GCSFocus(this IWebElement element) {
		element.GetWebDriver().ExecuteJavaScript("arguments[0].focus();", element);
	}
	public static void GCSRemoveFocus(this IWebElement element) {
		element.GetWebDriver().ExecuteJavaScript("arguments[0].blur();", element);
	}

	public static void GCSTriggerDomEvent(this IWebElement element, EDomEvent domEvent) {
		element.GetWebDriver().ExecuteJavaScript($"arguments[0].dispatchEvent(new Event('{domEvent.ToString().ToLower()}'))", element);
	}

	public static void GCSTriggerDomEvent(this IWebElement element, string eventName) {
		element.GetWebDriver().ExecuteJavaScript($"arguments[0].dispatchEvent(new Event('{eventName.ToString().Trim().ToLower()}'))", element);
	}

	public static async Task GCSWaitForPageToLoadAsync(this IWebDriver driver, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.ExecuteJavaScript<bool>("return document.readyState === 'complete';")) {
				return;
			}
			await Task.Delay(1000);
		}
		throw new GCScriptException(360514, $"Limit of {seconds} seconds exceeded!");
	}
	public static void GCSWaitForPageToLoad(this IWebDriver driver, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.ExecuteJavaScript<bool>("return document.readyState === 'complete';")) {
				return;
			}
			Thread.Sleep(1000);
		}
		throw new GCScriptException(276901, $"Limit of {seconds} seconds exceeded!");
	}
	public static void GCSOverrideAlertConfirmTrue(this IWebDriver driver) => driver.ExecuteJavaScript("window.confirm = function () { return true; };");

	public static void GCSFindWindowContainsTextInUrl(this IWebDriver driver, string text) {
		foreach (string window in driver.WindowHandles) {
			driver.SwitchTo().Window(window);
			if (driver.Url.Contains(text, StringComparison.OrdinalIgnoreCase)) {
				return;
			}
		}
		throw new GCScriptException(110203, $"Unable to find window containing text [{text}] in URL");
	}
	public static void GCSFindWindowContainsTextInTitle(this IWebDriver driver, string text) {
		foreach (string window in driver.WindowHandles) {
			driver.SwitchTo().Window(window);
			if (driver.Title.Contains(text, StringComparison.OrdinalIgnoreCase)) {
				return;
			}
		}
		throw new GCScriptException(872724, $"Unable to find window containing text [{text}] in Title");
	}
	public static void GCSFindWindowStartsWithTextInTitle(this IWebDriver driver, string text) {
		foreach (string window in driver.WindowHandles) {
			driver.SwitchTo().Window(window);
			if (driver.Title.StartsWith(text, StringComparison.OrdinalIgnoreCase)) {
				return;
			}
		}
		throw new GCScriptException(565754, $"Unable to find window starting with text [{text}] in Title");
	}
	public static void GCSFindWindowEndsWithTextInTitle(this IWebDriver driver, string text) {
		foreach (string window in driver.WindowHandles) {
			driver.SwitchTo().Window(window);
			if (driver.Title.EndsWith(text, StringComparison.OrdinalIgnoreCase)) {
				return;
			}
		}
		throw new GCScriptException(686491, $"Unable to find window ending with text [{text}] in Title");
	}

	public static async Task GCSWaitWindowHandlesCountAsync(this IWebDriver driver, int count, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.WindowHandles.Count == count) {
				return;
			}
			await Task.Delay(1000);
		}
		throw new GCScriptException(145377, $"Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCSWaitWindowHandlesCountMoreThanAsync(this IWebDriver driver, int count, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.WindowHandles.Count > count) {
				return;
			}
			await Task.Delay(1000);
		}
		throw new GCScriptException(558551, $"Limit of {seconds} seconds exceeded!");
	}
	public static async Task GCSWaitWindowHandlesCountLessThanAsync(this IWebDriver driver, int count, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.WindowHandles.Count < count) {
				return;
			}
			await Task.Delay(1000);
		}
		throw new GCScriptException(988734, $"Limit of {seconds} seconds exceeded!");
	}

	public static void GCSWaitWindowHandlesCount(this IWebDriver driver, int count, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.WindowHandles.Count == count) {
				return;
			}
			Thread.Sleep(1000);
		}
		throw new GCScriptException(958418, $"Limit of {seconds} seconds exceeded!");
	}
	public static void GCSWaitWindowHandlesCountMoreThan(this IWebDriver driver, int count, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.WindowHandles.Count > count) {
				return;
			}
			Thread.Sleep(1000);
		}
		throw new GCScriptException(916654, $"Limit of {seconds} seconds exceeded!");
	}
	public static void GCSWaitWindowHandlesCountLessThan(this IWebDriver driver, int count, int seconds = 15) {
		seconds = Math.Max(1, seconds);
		for (int i = 0; i < seconds; i++) {
			if (driver.WindowHandles.Count < count) {
				return;
			}
			Thread.Sleep(1000);
		}
		throw new GCScriptException(682433, $"Limit of {seconds} seconds exceeded!");
	}

	public static void GCSNavigateToUrl(this IWebDriver driver, string url) => driver.Navigate().GoToUrl(url);
	public static void GCSNavigateToUrlWithJS(this IWebDriver driver, string url) => driver.ExecuteJavaScript($"window.location.href = '{url}';");
	public static void GCSRefreshPageWithJS(this IWebDriver driver, bool reloadFromServer = true) => driver.ExecuteJavaScript($"window.location.reload({(reloadFromServer ? "true" : "")});");

	public static void GCSSelectIndexInDropdown(this IWebElement element, int index) => new SelectElement(element).SelectByIndex(index);
	public static void GCSSelectTextInDropdown(this IWebElement element, string text) => new SelectElement(element).SelectByText(text);
	public static void GCSSelectValueInDropdown(this IWebElement element, string value) => new SelectElement(element).SelectByValue(value);

	public static string GCSGetValueWithJS(this IWebElement element) {
		return element.GetWebDriver().ExecuteJavaScript<string>("return arguments[0].value;", element);
	}
	public static void GCSSetValueWithJS(this IWebElement element, string value) {
		element.GetWebDriver().ExecuteJavaScript("arguments[0].value = arguments[1];", element, value);
	}

	public static void GCSSetAttributeWithJS(this IWebDriver driver, IWebElement element, string attributeName, string value) => driver.ExecuteJavaScript($"arguments[0].setAttribute('{attributeName}', '{value}');", element);

	public static string GCSGetUserAgentWithJS(this IWebDriver driver) => driver.ExecuteJavaScript<string>("return navigator.userAgent;");

	public static void GCSForceClose(this IWebDriver driver) { try { driver.Close(); } catch { } try { driver.Quit(); } catch { } try { driver.Dispose(); } catch { } }
	public static void GCSSavePageSource(this IWebDriver driver, string filePath) => File.WriteAllText(filePath, driver.PageSource);

	public static async Task GCSWaitSAsync(this IWebDriver driver, int seconds) { seconds = Math.Max(1, seconds); await Task.Delay(TimeSpan.FromSeconds(seconds)); }
	public static void GCSWaitS(this IWebDriver driver, int seconds) { seconds = Math.Max(1, seconds); Thread.Sleep(TimeSpan.FromSeconds(seconds)); }
	public static async Task GCSWaitMsAsync(this IWebDriver driver, int milliseconds) { milliseconds = Math.Max(1, milliseconds); await Task.Delay(milliseconds); }
	public static void GCSWaitMs(this IWebDriver driver, int milliseconds) { milliseconds = Math.Max(1, milliseconds); Thread.Sleep(milliseconds); }
	public static async Task GCSWaitMAsync(this IWebDriver driver, int seconds) { seconds = Math.Max(1, seconds); await Task.Delay(TimeSpan.FromMinutes(seconds)); }
	public static void GCSWaitM(this IWebDriver driver, int seconds) { seconds = Math.Max(1, seconds); Thread.Sleep(TimeSpan.FromMinutes(seconds)); }

	public static int GCSGetPageZoom(this IWebDriver driver) => int.Parse(driver.ExecuteJavaScript<string>("return document.body.style.zoom;").Replace("%", ""));
	public static void GCSSetPageZoom(this IWebDriver driver, int percent = 100) => driver.ExecuteJavaScript($"document.body.style.zoom = '{Math.Clamp(percent, 1, 100)}%';");
	public static void GCSSetZoom(this IWebElement element, int percent = 100, int originX = 50, int originY = 50) {
		percent = Math.Clamp(percent, 1, 400);
		originX = Math.Clamp(originX, 0, 100);
		originY = Math.Clamp(originY, 0, 100);

		var scale = $"{percent / 100.0:0.00}".Replace(",", ".");
		var origin = $"{originX}% {originY}%";
		element.GetWebDriver().ExecuteJavaScript($"arguments[0].style.transform = 'scale({scale})'; arguments[0].style.transformOrigin = '{origin}';", element);
	}

	public static void GCSRemove(this IWebElement element) {
		element.GetWebDriver().ExecuteJavaScript("arguments[0].remove();", element);
	}
	public static void GCSSetAttribute(this IWebElement element, string attributeName, string value, bool escapeSingleQuotes = true) {
		value = escapeSingleQuotes ? value.Replace("'", "\\'") : value;
		element.GetWebDriver().ExecuteJavaScript($"arguments[0].setAttribute('{attributeName}', '{value}');", element);
	}
	public static void GCSRemoveAttribute(this IWebElement element, string attributeName) {
		element.GetWebDriver().ExecuteJavaScript($"arguments[0].removeAttribute('{attributeName}');", element);
	}
	public static void GCSSetProperty (this IWebElement element, string propertyName, string value, bool escapeSingleQuotes = true) {
		value = escapeSingleQuotes ? value.Replace("'", "\\'") : value;
		element.GetWebDriver().ExecuteJavaScript($"arguments[0].{propertyName} = '{value}';", element);
	}

	public static void GCSDragAndDrop(this IWebDriver driver, IWebElement source, IWebElement target) => new Actions(driver).DragAndDrop(source, target).Perform();
	public static void GCSScrollToElement(this IWebElement element, bool alignToTop = true) => element.GetWebDriver().ExecuteJavaScript($"arguments[0].scrollIntoView({(alignToTop ? "true" : "")});", element);
	public static void GCSWindowScrollTo(this IWebDriver driver, int x, int y) => driver.ExecuteJavaScript($"window.scrollTo({x}, {y});");
	public static void GCSWindowScrollToTop(this IWebDriver driver) => driver.ExecuteJavaScript("window.scrollTo(0, 0);");
	public static void GCSWindowScrollToElement(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript($"window.scrollTo({element.Location.X}, {element.Location.Y});");
	public static void GCSWindowScrollToElementY(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript($"window.scrollTo(0, {element.Location.Y});");
	public static void GCSWindowScrollToElementX(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript($"window.scrollTo({element.Location.X}, 0);");

	public static Screenshot GCSGetScreenshot(this IWebDriver driver) => driver.TakeScreenshot();
	public static Screenshot GCSGetScreenshot(this IWebElement element) => ((ITakesScreenshot)element).GetScreenshot();
	public static void GCSSaveScreenshot(this IWebDriver driver, string filePath) => driver.GCSGetScreenshot().SaveAsFile(filePath);
	public static void GCSSaveScreenshot(this IWebElement element, string filePath) => element.GCSGetScreenshot().SaveAsFile(filePath);
	public static void GCSHide(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript("arguments[0].style.display = 'none';", element);
	public static void GCSShow(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript("arguments[0].style.display = '';", element);
	public static void GCSShowBlock(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript("arguments[0].style.display = 'block';", element);
	public static void GCSShowFlex(this IWebElement element) => element.GetWebDriver().ExecuteJavaScript("arguments[0].style.display = 'flex';", element);
	public static void GCSSetStyle(this IWebElement element, string style) => element.GetWebDriver().ExecuteJavaScript($"arguments[0].style = '{style}';", element);
}
