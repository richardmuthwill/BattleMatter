using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour {
	[SerializeField]
	GameObject pauseMenu;

	void Start ()
	{
		PauseMenu.isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			TogglePauseMenu ();
		}
	}

	void TogglePauseMenu ()
	{
		pauseMenu.SetActive (!pauseMenu.activeSelf);
		PauseMenu.isOn = pauseMenu.activeSelf;
		if (PauseMenu.isOn) {
			Cursor.visible = true;
		} else {
			Cursor.visible = false;
		}
	}
}
