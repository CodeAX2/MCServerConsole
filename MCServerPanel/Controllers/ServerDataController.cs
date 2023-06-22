using Microsoft.AspNetCore.Mvc;
using MCServerPanel.Data;

namespace MCServerPanel.Controllers {
[ApiController]
[Route("api/[controller]")]
public class ServerDataController : ControllerBase {

	private IMCServerProvider _mcServer;

	public ServerDataController(IMCServerProvider mcServer) {
		_mcServer = mcServer;
	}

	[HttpGet()]
	public IActionResult OnGet(string? address, ushort? port) {

		address ??= "localhost";
		port ??= 25565;

		if (AuthCache.IsSessionAuthorized(Request.Cookies)) {
			_mcServer.SetAddressAndPort(address, port.Value);
			_mcServer.RefreshData();
			return StatusCode(200, new MCServerData(_mcServer));
		}

		return StatusCode(401, "Unauthorized Session");
	}
}
}