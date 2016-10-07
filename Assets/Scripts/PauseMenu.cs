using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PauseMenu : MonoBehaviour {

	public static bool isOn = false;

	public void LeaveRoom ()
	{
		Debug.Log ("Tried to leave room");
		MatchInfo matchInfo = NetworkManager.singleton.matchInfo;
		NetworkManager.singleton.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, NetworkManager.singleton.OnDropConnection);
		NetworkManager.singleton.StopHost ();
	}

}
