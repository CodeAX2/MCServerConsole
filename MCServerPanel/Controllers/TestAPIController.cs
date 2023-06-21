using Microsoft.AspNetCore.Mvc;

namespace MCServerPanel.Controllers {
[ApiController]
[Route("api/[controller]")]
public class TestAPIController : ControllerBase {

	[HttpGet()]
	public IActionResult TestGet(string authToken) {
		CookieOptions options = new CookieOptions();
		options.Expires = DateTime.Now.AddMinutes(2);

		Response.Cookies.Append("Test-Cookie-JH", "REEE", options);
		return StatusCode(200, "Got token: " + authToken);
	}
}
}