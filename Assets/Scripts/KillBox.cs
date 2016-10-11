using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
// https://docs.unity3d.com/Manual/UNetSetup.html
public class KillBox : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";
	private BoxCollider collider;

	public int damage = 999999;

	void Start ()
	{
		collider = gameObject.GetComponent<BoxCollider> ();
	}

	void OnTriggerEnter (Collider collision)
	{
		LooseHealth (collision);
	}

	[Client]
	void LooseHealth (Collider col)
	{
		if (col.GetComponent<Collider>().tag == PLAYER_TAG) {
			CmdLooseHealth (col.GetComponent<Collider>().name, damage);
		}
	}

	[Command]
	void CmdLooseHealth (string _playerID, int _damage)
	{
		Debug.Log (_playerID + " fell to their death");

		Player _player = GameManager.GetPlayer(_playerID);
		_player.RpcTakeDamage (_damage, "felltodeath");
	}
}