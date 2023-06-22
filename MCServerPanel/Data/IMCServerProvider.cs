namespace MCServerPanel.Data;

public interface IMCServerProvider {

	void RefreshData();
	bool IsOnline();
	int GetCurrentPlayers();
	int GetMaximumPlayers();
	void SetAddressAndPort(string address, ushort port);
}