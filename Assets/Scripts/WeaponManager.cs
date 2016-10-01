using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

	[SerializeField]
	private string weaponLayerName = "Weapon";

	[SerializeField]
	private Transform weaponHolder;

	[SerializeField]
	private PlayerWeapon primaryWeapon;

	private PlayerWeapon currentWeapon;
	private GameObject currentGunbarrel;
	private bool gunbarrelPositionFix;
	private AudioClip currentShootSound;

	void Start ()
	{
		EquipWeapon (primaryWeapon);
	}

	public PlayerWeapon GetCurrentWeapon () 
	{
		return currentWeapon;
	}

	public GameObject GetCurrentGunbarrel () 
	{
		return currentGunbarrel;
	}

	public bool GetCurrentGunbarrelPositionFix () 
	{
		return gunbarrelPositionFix;
	}

	public AudioClip GetCurrentShootSound () 
	{
		return currentShootSound;
	}

	void Update ()
	{
		
	}

	void EquipWeapon (PlayerWeapon _weapon)
	{
		currentWeapon = _weapon;

		GameObject _weaponIns = (GameObject)Instantiate (_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
		_weaponIns.transform.SetParent(weaponHolder);
		if(isLocalPlayer)
			SetLayerRecursively(_weaponIns, LayerMask.NameToLayer (weaponLayerName));

		currentGunbarrel = _weaponIns.transform.FindChild ("GunBarrel").gameObject;
		gunbarrelPositionFix = _weapon.gunbarrelPositionFix;
		currentShootSound = _weapon.shootSound;
	}

	void SetLayerRecursively (GameObject obj, int newLayer)
	{
		obj.layer = newLayer;

		foreach (Transform child in obj.transform) {
			SetLayerRecursively (child.gameObject, newLayer);
		}
	}
}