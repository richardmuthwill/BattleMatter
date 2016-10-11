using UnityEngine;
using System.Collections;

public class Scoreboard : MonoBehaviour {

	void OnEnable ()
	{
		Player[] players = GameManager.GetAllPlayers ();

		foreach (Player player in players) {
			Debug.Log (player.name + " | " + player.kills + " | " + player.deaths);
			// https://youtu.be/iyO4SdkHDXM?list=PLPV2KyIb3jR5PhGqsO7G4PsbEC_Al-kPZ&t=1031
		}
	}

	void OnDisable ()
	{
		
	}

}
