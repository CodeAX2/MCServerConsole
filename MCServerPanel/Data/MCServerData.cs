namespace MCServerPanel.Data;

public struct MCServerData {

	public bool IsOnline;
	public int CurrentPlayers;
	public int MaximumPlayers;

	public MCServerData(IMCServerProvider fromProvider) {
		IsOnline = fromProvider.IsOnline();
		CurrentPlayers = fromProvider.GetCurrentPlayers();
		MaximumPlayers = fromProvider.GetMaximumPlayers();
	}
}