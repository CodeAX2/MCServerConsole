using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MCServerPanel.Models;

namespace MCServerPanel.Controllers;

public class HomeController : Controller {
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger) {
		_logger = logger;
	}

	private IActionResult? RequireValidSession() {
		string? token = Request.Cookies["session-token"];
		if (token == null || !Data.AuthCache.IsSessionAuthorized(token)) {
			if (token != null)
				TempData["Errors"] = "Invalid Session";
			return RedirectToAction("Index", "Login");
		}
		return null;
	}

	public IActionResult Index() {

		IActionResult? result = RequireValidSession();
		if (result != null)
			return result;

		return View();
	}

	public IActionResult Console() {
		IActionResult? result = RequireValidSession();
		if (result != null)
			return result;
		return View();
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
