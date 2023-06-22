namespace MCServerPanel.Data;

public class AuthCache {

	public static readonly string AUTH_COOKIE = "session-token";

	private static readonly object locker = new object();
	private static Dictionary<string, DateTime> authorizedSessionTokens;

	static AuthCache() {
		authorizedSessionTokens = new Dictionary<string, DateTime>();
		StartInvalidator();
	}

	public static void AddAuthorizedSession(string sessionToken) {
		lock (locker) {
			authorizedSessionTokens[sessionToken] = DateTime.Now;
		}
	}

	public static void RemoveSession(string sessionToken) {
		lock (locker) {
			authorizedSessionTokens.Remove(sessionToken);
		}
	}

	public static bool IsSessionAuthorized(IRequestCookieCollection cookies) {
		string? sessionToken = cookies[AUTH_COOKIE];
		if (sessionToken != null)
			return IsSessionAuthorized(sessionToken);
		return false;
	}

	public static bool HasSession(IRequestCookieCollection cookies) {
		string? sessionToken = cookies[AUTH_COOKIE];
		return sessionToken != null;
	}

	public static bool IsSessionAuthorized(string sessionToken) {
		lock (locker) {
			if (authorizedSessionTokens.ContainsKey(sessionToken)) {
				authorizedSessionTokens[sessionToken] = DateTime.Now;
				return true;
			}
		}
		return false;
	}

	private static void StartInvalidator() {
		new Timer(
			InvalidateSessions, null, TimeSpan.Zero, TimeSpan.FromSeconds(5)
		);
	}

	private static void InvalidateSessions(object? state) {
		lock (locker) {
			var invalidSessions =
				authorizedSessionTokens.Where(kv => IsTimeInvalid(kv.Value));
			foreach (var session in invalidSessions) {
				System.Console.WriteLine("Removed session: " + session);
				authorizedSessionTokens.Remove(session.Key);
			}
		}
	}

	private static bool IsTimeInvalid(DateTime lastActivityTime) {
		DateTime invalidTime = lastActivityTime.AddSeconds(20);
		return DateTime.Now > invalidTime;
	}
}