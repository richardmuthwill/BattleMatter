using UnityEngine;
using System.Collections;

public class LocalPlayer : MonoBehaviour {

	private GameObject localPlayer;

	public GameObject local
	{
		get
		{
			return localPlayer;
		}

		set
		{
			localPlayer = value;
		}
	}
}
