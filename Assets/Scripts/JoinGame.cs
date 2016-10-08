using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class JoinGame : MonoBehaviour {

	List<GameObject> roomList = new List<GameObject> ();

	[SerializeField]
	private Text status;

	[SerializeField]
	private GameObject roomListItemPrefab;

	[SerializeField]
	private Transform roomListParent;

	void Start ()
	{
		NetworkManager.singleton.StartMatchMaker();

		RefreshRoomList ();
	}

	public void RefreshRoomList ()
	{
		ClearRoomList ();
		NetworkManager.singleton.matchMaker.ListMatches (0, 20, "", true, 0, 0, OnMatchList);
		status.text = "Loading Rooms...";
	}
	/*
	public void OnMatchList (MatchInfoSnapshot matchList)
	{
		status.text = "";

		if (matchList == null) {
			status.text = "Couldn't find any rooms.";
			return;
		}

		ClearRoomList ();

		foreach (MatchInfoSnapshot match in matchList) {
			GameObject _roomListItemGO = Instantiate (roomListItemPrefab);
			_roomListItemGO.transform.SetParent (roomListParent);

			roomList.Add (_roomListItemGO);

		}

	}
	*/

	private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		status.text = "";

		if (success)
		{
			if (matches.Count != 0)
			{

				foreach (MatchInfoSnapshot match in matches) {
					GameObject _roomListItemGO = Instantiate (roomListItemPrefab);
					_roomListItemGO.transform.SetParent (roomListParent, false);

					RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem> ();
					if (_roomListItem != null) {
						_roomListItem.Setup (match, JoinRoom);
					}


					roomList.Add (_roomListItemGO);

				}

				/*
				//Debug.Log("A list of matches was returned");

				//join the last server (just in case there are two...)
				NetworkManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);

				*/
			}
			else
			{
				status.text = "No Rooms Found...";
				return;
			}
		}
		else
		{
			status.text = "Couldn't find anything...";
			return;
		}
	}

	void ClearRoomList ()
	{
		for (int i = 0; i < roomList.Count; i++) {
			Destroy (roomList[i]);
		}

		roomList.Clear ();
	}

	public void JoinRoom (MatchInfoSnapshot _match)
	{
		NetworkManager.singleton.matchMaker.JoinMatch (_match.networkId, "", "", "", 0, 0, NetworkManager.singleton.OnMatchJoined);
		ClearRoomList ();
		status.text = "Joining Room...";
	}

}






















