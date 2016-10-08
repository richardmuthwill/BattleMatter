﻿using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;

	[SerializeField]
	string remoteLayerName = "RemoteLayer";

	[SerializeField]
	string dontDrawLayerName = "DontDraw";

	[SerializeField]
	GameObject playerGraphics;

	[SerializeField]
	GameObject playerUIPrefab;
	private GameObject playerUIInstance;

	LayerMask mask;

	Camera sceneCamera;

	private LocalPlayer localPlayer;

	void Start ()
	{
		mask = LayerMask.NameToLayer ("RemoteLayer");
		localPlayer = GameObject.FindGameObjectWithTag ("NetworkManager").GetComponent<LocalPlayer> ();
		if (!isLocalPlayer) {
			DisableComponents ();
			AssignRemoteLayer ();
		} else {
			sceneCamera = GameObject.FindGameObjectWithTag("MenuCamera").GetComponent<Camera>();
			if (sceneCamera != null) {
				sceneCamera.gameObject.SetActive (false);
			}

			SetLayerRecursively (playerGraphics, LayerMask.NameToLayer (dontDrawLayerName));
		
			playerUIInstance = Instantiate (playerUIPrefab);
			playerUIInstance.name = playerUIPrefab.name;

			GetComponent<Player> ().PlayerSetup ();
		}
	}

	void SetLayerRecursively (GameObject obj, int newLayer)
	{
		obj.layer = newLayer;

		foreach (Transform child in obj.transform) {
			SetLayerRecursively (child.gameObject, newLayer);
		}
	}

	public override void OnStartClient () 
	{
		base.OnStartClient ();

		string _netID = GetComponent<NetworkIdentity> ().netId.ToString();
		Player _player = GetComponent<Player> ();

		GameManager.RegisterPlayer (_netID, _player);

		// localPlayer.local = _player.gameObject;
	}

	void AssignRemoteLayer () 
	{
		gameObject.layer = mask;
	}

	void DisableComponents () {
		for (int i = 0; i < componentsToDisable.Length; i++) {
			componentsToDisable [i].enabled = false;
		}
	}

	void OnDisable()
	{
		Destroy (playerUIInstance);

		if (isLocalPlayer) {
			if (sceneCamera != null) {
				sceneCamera.gameObject.SetActive (true);
			}
		}

		GameManager.UnRegisterPlayer (transform.name);
	}
}