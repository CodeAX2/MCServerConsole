using Microsoft.AspNetCore.Mvc;
using MCServerPanel.Data;
using MinecraftServerRCON;

namespace MCServerPanel.Controllers {
[ApiController]
[Route("api/[controller]")]
public class ServerConsoleController : ControllerBase {

	[HttpGet()]
	public IActionResult OnGet(string serverPath, int lineStart) {

		if (AuthCache.IsSessionAuthorized(Request.Cookies)) {
			try {
				string fullLog =
					System.IO.File.ReadAllText(serverPath + "/logs/latest.log");

				string[] lines = fullLog.Split(
					new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None
				);
				string[] shortened = lines.Skip(lineStart - 1).ToArray();

				object returnedObj = new {
					log = string.Join("\n", shortened),
					newLineStart = lines.Length
				};
				return StatusCode(200, returnedObj);
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

	[HttpPost()]
	public IActionResult OnPost([FromBody] string command) {
		if (AuthCache.IsSessionAuthorized(Request.Cookies)) {
			using RCONClient rcon = RCONClient.INSTANCE;
			rcon.setupStream("localhost", 25575);
			rcon.sendMessage(RCONMessageType.Command, command);
			return StatusCode(200, "Maybe success");
		}

		return StatusCode(401, "Unauthorized Session");
	}
}
}