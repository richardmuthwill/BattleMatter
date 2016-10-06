using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {

	[SerializeField]
	private uint roomSize = 20;

	private string roomName;
	private string roomPass = "";
	private NetworkManager networkManager;

	public int gameType;
	/* 
	 int	Gameplay Type		Name
	 
	 1		Team Deathmatch		Teams
	 2		Free For All		Alone
	 3		Capture The Flag	Intel
	 4		Capture The Bases	Defend
	 5		Bomb				Defuse
	 6		VIP					Target
	*/

	void Start () {
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null) {
			networkManager.StartMatchMaker ();
		}
	}

	public void SetRoomName (string _name)
	{
		roomName = _name;
	}

	public void SetRoomPass (string _pass)
	{
		roomPass = _pass;
	}

	public void SetRoomSize (uint _size)
	{
		roomSize = _size;
	}

	public void SetGameType (string _gameType)
	{
		if (_gameType == "Teams") {
			gameType = 1;
		} else if (_gameType == "Alone") {
			gameType = 2;
		} else if (_gameType == "Intel") {
			gameType = 3;
		} else if (_gameType == "Defend") {
			gameType = 4;
		} else if (_gameType == "Defuse") {
			gameType = 5;
		} else if (_gameType == "Target") {
			gameType = 6;
		}

	}

	public void CreateRoom ()
	{
		if (roomName != "" && roomName != null) {
			Debug.Log ("Creating Room: " + roomName + " with room for " + roomSize + " players and a Gametype of " + gameType);

			// Create room
			networkManager.matchMaker.CreateMatch (roomName, roomSize, true, roomPass, "", "", 0, 0, networkManager.OnMatchCreate);
		}
	}

}
