namespace GCScript.Selenium.ExtensionMethods.Exceptions;

public class GCScriptException : Exception {
	public int Code { get; }

	public GCScriptException(int code, string message = "An unexpected error occurred =(")
		: base(message) {
		Code = code;
	}
}
