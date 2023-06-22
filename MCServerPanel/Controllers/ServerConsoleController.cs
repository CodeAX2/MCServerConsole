using Microsoft.AspNetCore.Mvc;
using MCServerPanel.Data;

namespace MCServerPanel.Controllers {
[ApiController]
[Route("api/[controller]")]
public class ServerConsoleController : ControllerBase {

	[HttpGet()]
	public IActionResult OnGet(string serverPath) {

		if (AuthCache.IsSessionAuthorized(Request.Cookies)) {
			try {
				string logOutput =
					System.IO.File.ReadAllText(serverPath + "/logs/latest.log");
				return StatusCode(200, "{log:\"logOutput\"");
			} catch (UnauthorizedAccessException e) {
				return StatusCode(
					405,
					"Not allowed to read log file: " + serverPath +
						"/logs/latest.log"
				);
			} catch (Exception e) {
				return StatusCode(
					404,
					"Could not find log file: " + serverPath + "/logs/latest.log"
				);
			}
		}

		return StatusCode(401, "Unauthorized Session");
	}
}
}