using MineStatLib;

namespace MCServerPanel.Data;

public class MineStatServerProvider : IMCServerProvider {

	private MineStat ms;
	private string address;
	private ushort port;

	public MineStatServerProvider() {
		address = "localhost";
		port = 25565;
		ms = new MineStat(address, port);
	}

	public void RefreshData() {
		ms = new MineStat(address, port);
	}

	public int GetCurrentPlayers() {
		return ms.CurrentPlayersInt;
	}

	public int GetMaximumPlayers() {
		return ms.MaximumPlayersInt;
	}

	public bool IsOnline() {
		return ms.ServerUp;
	}

	public void SetAddressAndPort(string address, ushort port) {
		this.address = address;
		this.port = port;
	}
}