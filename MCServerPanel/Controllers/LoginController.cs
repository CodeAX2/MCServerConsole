using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MCServerPanel.Controllers {

[Route("[controller]")]
public class LoginController : Controller {
	private readonly ILogger<LoginController> _logger;

	public LoginController(ILogger<LoginController> logger) {
		_logger = logger;
	}

	[HttpGet()]
	public IActionResult Index() {

		string? token = Request.Cookies["session-token"];
		if (token != null) {
			if (Data.AuthCache.IsSessionAuthorized(token)) {
				return RedirectToAction("Index", "Home");
			} else {
				Response.Cookies.Delete("session-token");
				ViewBag.ErrorMessage = TempData["Errors"];
			}
		}

		return View();
	}

	[HttpPost()]
	public IActionResult OnPost(string password) {
		if (password.Equals("Test123")) {
			// lol, lmao

			Random r = new Random();

			// Give cookie
			CookieOptions options = new CookieOptions();
			options.Expires = DateTime.Now.AddMinutes(2);

			string sessionToken = r.Next(10000000).ToString();
			Data.AuthCache.AddAuthorizedSession(sessionToken);

			Response.Cookies.Append("session-token", sessionToken, options);
			return RedirectToAction("Index", "Home");
		} else {
			ViewBag.ErrorMessage = "Incorrect Password";
			return View("Index");
		}
	}
}
}