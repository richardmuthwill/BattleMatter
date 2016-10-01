using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour {

	private const string PLAYER_TAG = "Player";

	[SerializeField]
	private Camera cam; // Where the raycast starts

	[SerializeField]
	private LayerMask mask;

	private PlayerWeapon currentWeapon;
	private WeaponManager weaponManager;
	private GameObject currentGunbarrel;
	private Vector3 gunbarrelForward;
	private AudioClip shootSound;
	private AudioSource audioSource;
	public GameObject bulletPrefab;

	void Start () 
	{
		if (cam == null) {
			Debug.LogError ("PlayerShoot: No camera!");
			this.enabled = false;
		}

		weaponManager = GetComponent<WeaponManager> ();
		audioSource = this.GetComponent<AudioSource> ();

	}
		
	void Update () 
	{
		currentWeapon = weaponManager.GetCurrentWeapon ();
		currentGunbarrel = weaponManager.GetCurrentGunbarrel ();
		shootSound = weaponManager.GetCurrentShootSound ();

		if (currentWeapon.fireRate <= 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot ();
			}
		} else {
			if (Input.GetButtonDown ("Fire1")) {
				InvokeRepeating ("Shoot", 0f, 1f / currentWeapon.fireRate);
			} else if (Input.GetButtonUp ("Fire1")) {
				CancelInvoke ("Shoot");
			}
		}


	}

	/*
	[Command]
	void CmdShoot ()
	{
		// This [Command] code is run on the server!

		// create the bullet object locally
		var EnergyBullet = (GameObject)Instantiate(
			bulletPrefab,
			currentGunbarrel.transform.position,
			Quaternion.identity);

		EnergyBullet.GetComponent<Rigidbody>().velocity = -transform.forward*4;

		// spawn the bullet on the clients
		NetworkServer.Spawn(EnergyBullet);

		// when the bullet is destroyed on the server it will automaticaly be destroyed on clients
		Destroy(EnergyBullet, 2.0f);
	}
	*/

	void Shoot ()
	{
		audioSource.clip = shootSound;
		audioSource.Play ();

		if (currentWeapon.hasProjectile) {

			if (weaponManager.GetCurrentGunbarrelPositionFix ()) {
				gunbarrelForward = currentGunbarrel.transform.up;
			} else {
				gunbarrelForward = currentGunbarrel.transform.forward;
			}

			CmdFireProjectile (currentWeapon.projectile, currentGunbarrel.transform.position, gunbarrelForward, currentWeapon.projectileSpeed, currentWeapon.damage);
			
			/*
			
				if (weaponManager.GetCurrentGunbarrelPositionFix ()) {
					gunbarrelForward = currentGunbarrel.transform.up;
				} else {
					gunbarrelForward = currentGunbarrel.transform.forward;
				}
			
				GameObject projectile = Instantiate(currentWeapon.projectile, currentGunbarrel.transform.position, Quaternion.identity) as GameObject;                
				projectile.GetComponent<Rigidbody>().AddForce(gunbarrelForward * currentWeapon.projectileSpeed); 
				
				
				
			*/
	
		} else {
			RaycastHit _hit;
			if (Physics.Raycast (cam.transform.position, cam.transform.forward, out _hit, currentWeapon.range, mask)) {
				if (_hit.collider.tag == PLAYER_TAG) {
					CmdPlayerShot (_hit.collider.name, currentWeapon.damage);
				}
			}
		}
	}


	[Command]
	void CmdFireProjectile(GameObject projectile, Vector3 position, Vector3 forward, float speed, int damage)
    {
       // This [Command] code is run on the server!

		// create the bullet object locally
		var bullet = (GameObject)Instantiate(bulletPrefab, position, Quaternion.identity);

		bullet.GetComponent<Rigidbody> ().velocity = forward * speed;
		EnergyBallDie bulletScript = bullet.GetComponent<EnergyBallDie> (); 
		bulletScript.damage = damage;

		// spawn the bullet on the clients
		NetworkServer.Spawn(bullet);

		// when the bullet is destroyed on the server it will automaticaly be destroyed on clients
		// Destroy(bullet);
    }

	[Command]
	void CmdPlayerShot (string _playerID, int _damage)
	{
		Debug.Log (_playerID + " has been shot");

		Player _player = GameManager.GetPlayer(_playerID);
		_player.RpcTakeDamage (_damage);
	}

}	
