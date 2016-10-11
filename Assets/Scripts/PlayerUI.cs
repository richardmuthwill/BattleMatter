using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour {
	[SerializeField]
	GameObject pauseMenu;

	[SerializeField]
	GameObject scoreboard;

	void Start ()
	{
		PauseMenu.isOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			TogglePauseMenu ();
		}
		if (Input.GetKeyDown (KeyCode.Tab)) {
			scoreboard.SetActive (true);
		} else if (Input.GetKeyUp (KeyCode.Tab)) {
			scoreboard.SetActive (false);
		}
	}

	public void TogglePauseMenu ()
	{
		pauseMenu.SetActive (!pauseMenu.activeSelf);
		PauseMenu.isOn = pauseMenu.activeSelf;
		Cursor.visible = pauseMenu.activeSelf;
	}
}
