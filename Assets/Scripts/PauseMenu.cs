using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PauseMenu : MonoBehaviour {

	public static bool isOn = false;

	/*
	private Player player;
	private GameObject playerGO;
	private PlayerUI playerUI;

	void Start ()
	{
		playerGO = GameObject.FindGameObjectWithTag ("NetworkManager").GetComponent<LocalPlayer> ().local;
		player = playerGO.GetComponent<Player> ();
		playerUI = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI> ();
	}
	*/

	public void LeaveRoom ()
	{
		Debug.Log ("Tried to leave room");
		MatchInfo matchInfo = NetworkManager.singleton.matchInfo;
		NetworkManager.singleton.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, NetworkManager.singleton.OnDropConnection);
		NetworkManager.singleton.StopHost ();
	}

	/*
	// Also fix in "PlayerSetup"
	public void Respawn ()
	{
		player.RpcTakeDamage (9999);
		playerUI.HidePauseMenu ();
	}
	*/
}
