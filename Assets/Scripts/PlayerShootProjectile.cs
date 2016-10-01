using UnityEngine;
using UnityEngine.Networking;

public class PlayerShootProjectile : NetworkBehaviour
{
	public GameObject bulletPrefab;

	[Command]
	void CmdFire()
	{
		// This [Command] code is run on the server!

		// create the bullet object locally
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			transform.position - transform.forward,
			Quaternion.identity);

		bullet.GetComponent<Rigidbody>().velocity = transform.forward*4;

		// spawn the bullet on the clients
		NetworkServer.Spawn(bullet);

		// when the bullet is destroyed on the server it will automaticaly be destroyed on clients
		Destroy(bullet, 2.0f);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// Command function is called from the client, but invoked on the server
			CmdFire();
		}
	}
}