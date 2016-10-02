using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {

	[SyncVar]
	private bool _isDead = false;
	public bool isDead
	{
		get { return _isDead; }
		protected set { _isDead = value; }
	}



	[SerializeField]
	private int maxHealth = 100;

	[SyncVar]
	private int currentHealth;

	[SerializeField]
	private Behaviour[] disableOnDeath;
	private bool[] disableWasEnabled;

	[SerializeField]
	private Collider deadCollider;

	[SerializeField]
	private Rigidbody deadRigidBody;

	[SerializeField]
	private NetworkTransform networkTransform;

	[SerializeField]
	private CharacterController characterController;

	public void Setup ()
	{

		disableWasEnabled = new bool[disableOnDeath.Length];
		for (int i = 0; i < disableWasEnabled.Length; i++) {
			disableWasEnabled [i] = disableOnDeath [i].enabled;
		}

		SetDefaults ();
	}

	[ClientRpc]
	public void RpcTakeDamage (int _amount)
	{
		if (isDead)
			return;

		currentHealth -= _amount;

		Debug.Log (transform.name + " now has " + currentHealth + " health");

		if (currentHealth <= 0) {
			Die ();
		}
	}

	private void Die () {
		isDead = true;

		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath [i].enabled = false;
		}

		deadCollider.enabled = true;
		deadRigidBody.isKinematic = false;
		deadRigidBody.useGravity = true;
		networkTransform.enabled = false;
		characterController.enabled = false;
		// 	deadRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;


		Debug.Log (transform.name + " Is dead!");

		// StartCoroutine (Respawn());
	}

	private IEnumerator Respawn () {
		yield return new WaitForSeconds (GameManager.instance.matchsettings.respawnTime);

		SetDefaults ();
		Transform _spawnPoint = NetworkManager.singleton.GetStartPosition ();
		transform.position = _spawnPoint.position;
		transform.rotation = _spawnPoint.rotation;
	}

	public void SetDefaults ()
	{
		isDead = false;
		currentHealth = maxHealth;

		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath [i].enabled = disableWasEnabled [i];
		}

		deadRigidBody.isKinematic = true;
		deadRigidBody.useGravity = false;
		deadRigidBody.constraints = RigidbodyConstraints.None;
		deadCollider.enabled = false;

		/*
		Collider _col = GetComponent<Collider> ();
		if (_col != null)
			_col.enabled = true;
			*/
	}


}
