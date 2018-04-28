using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		print ("hello my dear");

		var rot = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
		var v = Input.GetAxis("Vertical") * Time.deltaTime * 150.0f;

		print(rot);
		transform.Rotate(0,0,rot);
		transform.position += new Vector3(v,0,0);
		
	}
}
