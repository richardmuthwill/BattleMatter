using UnityEngine;
using System.Collections;

public class SunScript : MonoBehaviour {

	public float rotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		gameObject.transform.Rotate (rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, 0);

	}
}
