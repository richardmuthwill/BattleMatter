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
		Cursor.visible = pauseMenu.activeSelf;
	}

	public void HidePauseMenu ()
	{
		pauseMenu.SetActive (false);
		PauseMenu.isOn = false;
		Cursor.visible = false;
	}

	public void ShowPauseMenu ()
	{
		pauseMenu.SetActive (true);
		PauseMenu.isOn = true;
		Cursor.visible = true;
	}
}
