using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
// https://docs.unity3d.com/Manual/UNetSetup.html
public class EnergyBallDie : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";
	private SphereCollider collider;

	public int damage;
	
	void Start ()
	{
        StartCoroutine(DestroyFizz());
		collider = gameObject.GetComponent<SphereCollider> ();
	}

	IEnumerator DestroyFizz ()
	{
		yield return new WaitForSeconds(3);
		this.transform.GetChild (0).GetComponent<ParticleSystem> ().loop = false;
		yield return new WaitForSeconds(2);
		collider.enabled = false;
		CmdDestroyProjectile ();
	}

	IEnumerator DestroyCollision ()
	{
		this.transform.GetChild (0).GetComponent<ParticleSystem> ().Stop ();
		this.transform.GetChild (0).gameObject.active = false;
		this.transform.GetChild (1).gameObject.active = true;
		this.transform.GetChild (1).GetComponent<ParticleSystem> ().Play ();
		collider.enabled = false;
		yield return new WaitForSeconds(2);
		CmdDestroyProjectile ();
	}
	
	private void OnCollisionEnter (Collision collision)
	{
		this.GetComponent<AudioSource> ().Play ();
		this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		this.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
		LooseHealth (collision);
		StopCoroutine(DestroyFizz());
		StartCoroutine (DestroyCollision());
	}

	[Client]
	void LooseHealth (Collision col)
	{
		if (col.collider.tag == PLAYER_TAG) {
			CmdLooseHealth (col.collider.name, damage);
		}
	}

	[Command]
	void CmdLooseHealth (string _playerID, int _damage)
	{
		Debug.Log (_playerID + " has been shot");

		Player _player = GameManager.GetPlayer(_playerID);
		_player.RpcTakeDamage (_damage);
	}
	
	[Command]
	void CmdDestroyProjectile ()
	{
		Destroy (gameObject);
	}
}