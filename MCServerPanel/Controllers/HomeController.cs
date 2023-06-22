using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MCServerPanel.Models;
using MCServerPanel.Data;

namespace MCServerPanel.Controllers;

public class HomeController : Controller {
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger) {
		_logger = logger;
	}

	private IActionResult? RequireValidSession(IRequestCookieCollection cookies) {
		if (!AuthCache.IsSessionAuthorized(cookies)) {
			if (AuthCache.HasSession(cookies))
				TempData["Errors"] = "Invalid Session";
			return RedirectToAction("Index", "Login");
		}
		return null;
	}

	public IActionResult Index() {

		IActionResult? result = RequireValidSession(Request.Cookies);
		if (result != null)
			return result;

		return View();
	}

	public IActionResult LocalConsole() {
		IActionResult? result = RequireValidSession(Request.Cookies);
		if (result != null)
			return result;
		return View();
	}

	public IActionResult Logout() {
		IActionResult? result = RequireValidSession(Request.Cookies);
		if (result != null)
			return result;
		AuthCache.RemoveSession(Request.Cookies[AuthCache.AUTH_COOKIE]);
		return RedirectToAction("Index", "Login");
	}

	[ResponseCache(
		Duration = 0, Location = ResponseCacheLocation.None, NoStore = true
	)]
	public IActionResult Error() {
		return View(new ErrorViewModel {
			RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
		});
	}
}
