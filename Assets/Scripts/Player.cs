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

	// https://www.youtube.com/watch?v=makGoauwngQ&list=PLPV2KyIb3jR5PhGqsO7G4PsbEC_Al-kPZ&index=25

	[SerializeField]
	private int maxHealth = 100;

	[SyncVar]
	private int currentHealth;

	[SyncVar]
	public int kills;

	[SyncVar]
	public int deaths;

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

	[SerializeField]
	private GameObject meshObject;

	[SerializeField]
	private GameObject gunsObject;

	private bool firstSetup = true;

	public void PlayerSetup ()
	{
		if (isLocalPlayer) {
			// Switch Cameras
			/*
			// Not working because haven't followed all of Brackeys tutorials
			GameManager.instance.SetSceneCameraActive (false);
			GetComponent<PlayerSetup> ().playerUIInstance.SetActive (true);
			*/
		}

		CmdBroadcastNewPlayerSetup ();
	}

	[Command]
	private void CmdBroadcastNewPlayerSetup ()
	{
		RpcSetupPlayerOnAllClients ();
	}

	[ClientRpc]
	private void RpcSetupPlayerOnAllClients ()
	{
		if (firstSetup) {
			disableWasEnabled = new bool[disableOnDeath.Length];
			for (int i = 0; i < disableWasEnabled.Length; i++) {
				disableWasEnabled [i] = disableOnDeath [i].enabled;
			}

			firstSetup = false;
		}

		SetDefaults ();
	}

	[ClientRpc]
	public void RpcTakeDamage (int _amount, string _sourceID)
	{
		if (isDead)
			return;

		currentHealth -= _amount;

		Debug.Log (transform.name + " now has " + currentHealth + " health");

		if (currentHealth <= 0) {
			Die (_sourceID);
		}
	}

	private void Die (string _sourceID) {
		isDead = true;

		Player sourcePlayer = GameManager.GetPlayer (_sourceID);

		if (sourcePlayer != null) {
			sourcePlayer.kills++;
		}

		deaths++;

		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath [i].enabled = false;
		}

		// Test dropping gun
		// gunsObject.transform.parent = null;

		deadCollider.enabled = true;
		deadRigidBody.isKinematic = false;
		deadRigidBody.useGravity = true;
		networkTransform.enabled = false;
		characterController.enabled = false;
		deadRigidBody.constraints = RigidbodyConstraints.None;
		// 	

		deadRigidBody.velocity = Random.onUnitSphere * 2;

		Debug.Log (transform.name + " Is dead!");

		StartCoroutine (Respawn());
	}

	void Update ()
	{
		if (!isLocalPlayer)
			return;

		if (Input.GetKeyDown (KeyCode.K)) {
			RpcTakeDamage (9999, "suicide");
		}
	}

	private IEnumerator Respawn () {
		yield return new WaitForSeconds (GameManager.instance.matchsettings.respawnTime);

		HideBody ();

		Transform _spawnPoint = NetworkManager.singleton.GetStartPosition ();
		transform.position = _spawnPoint.position;
		transform.rotation = _spawnPoint.rotation;

		yield return new WaitForSeconds (0.1f); // Wait until everyone knows where to spawn particles

		PlayerSetup ();
	}

	private void HideBody ()
	{
		deadCollider.enabled = false;
		deadRigidBody.isKinematic = true;
		deadRigidBody.useGravity = false;
		networkTransform.enabled = false;
		characterController.enabled = false;

		meshObject.SetActive (false);
		// gunsObject.SetActive (false);
		deadRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}

	public void SetDefaults ()
	{
		isDead = false;
		currentHealth = maxHealth;

		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath [i].enabled = disableWasEnabled [i];
		}


		deadCollider.enabled = false;
		deadRigidBody.isKinematic = true;
		deadRigidBody.useGravity = false;
		networkTransform.enabled = true;
		characterController.enabled = true;
		deadRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		meshObject.SetActive (true);
		gunsObject.SetActive (true);


		/*
		Collider _col = GetComponent<Collider> ();
		if (_col != null)
			_col.enabled = true;
		*/
	}


}
