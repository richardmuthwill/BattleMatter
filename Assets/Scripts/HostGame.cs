using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using UnityEngine.UI;

public class HostGame : MonoBehaviour {

	[SerializeField]
	private uint roomSize = 20;

	private string roomName;
	private string roomPass = "";
	private NetworkManager networkManager;

	public int gameType;
	/* 
	 int	Gameplay Type		Name
	 
	 0		Team Deathmatch		Teams
	 1		Free For All		Alone
	 2		Capture The Flag	Intel
	 3		Capture The Bases	Defend
	 4		Bomb				Defuse
	 5		VIP					Target
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

	public void SetRoomSize (string _size)
	{
		uint u = Convert.ToUInt32 (_size);
		roomSize = u;
	}

	public void SetGameType (Int32 _gameType)
	{
		gameType = _gameType;

	}

	public void CreateRoom ()
	{
		if (roomName != "" && roomName != null && roomSize > 0 && roomSize != null) {
			networkManager.matchMaker.CreateMatch (roomName, roomSize, true, roomPass, "", "", 0, 0, networkManager.OnMatchCreate);
		}
	}

}
