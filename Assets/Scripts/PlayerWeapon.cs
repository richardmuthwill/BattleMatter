using UnityEngine;

[System.Serializable]
public class PlayerWeapon {

	public string name = "Bubblegun";

	public int damage = 10;

	public float range = 100f;
	public float fireRate = 0f;
	public float projectileSpeed = 200f;

	public bool hasProjectile = true;
	public bool gunbarrelPositionFix = true;

	public GameObject graphics;
	public GameObject projectile;

	public AudioClip shootSound;
}
