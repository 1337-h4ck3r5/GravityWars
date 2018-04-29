using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	private float currSpeed = 0f;
	public GameObject shot;

	private int shotCooldown = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		doMove ();
	}

	void doMove() {
		var world = transform.TransformDirection(Vector3.up);
		var rot = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;
		var v = Input.GetAxis("Vertical") * Time.deltaTime;

		var fire = Input.GetKey (KeyCode.Space);
		if (fire && shotCooldown == 0) {
			var instance = Instantiate (shot, transform.position, Quaternion.identity);
			var shotController = instance.GetComponent<ShotController> ();
			shotController.direction = world;
			shotController.speed = currSpeed + 1f;

			NetworkServer.Spawn (instance);
			shotCooldown = 10;
		}

		if (shotCooldown > 0)
			shotCooldown--;

		transform.Rotate(0,0, -rot);
		transform.Translate (world * getNewSpeed(v), Space.World);
		var fixedPos = new Vector3 (Mathf.Clamp (transform.position.x, -1000, 1000),
			               Mathf.Clamp (transform.position.y, -1000, 1000),
			               0);
		transform.position = fixedPos;
		print (transform.position.x);
		print (transform.position.y);
		print (transform.position.z);
	}
		
	float getNewSpeed(float v) {

		var maxSpeed = 2;
		/*if (v > 0)
			currSpeed += 0.02f * (maxSpeed - currSpeed);
		else if (v < 0)
			currSpeed -= 0.02f * (currSpeed);*/
		if (v > 0) {
			if (currSpeed > 0.01)
				currSpeed *= 1.1f;
			else if (currSpeed < -0.01)
				currSpeed /= 1.1f;
			else
				currSpeed += 0.01f;
		} else if (v < 0) {
			if (currSpeed > 0.01)
				currSpeed /= 1.1f;
			else if (currSpeed < -0.01)
				currSpeed *= 1.1f;
			else
				currSpeed -= 0.01f;
		} else {
			if (currSpeed > -0.05 && currSpeed < 0.05)
				currSpeed = 0;
			else currSpeed /= 1.1f;
		}

		if (currSpeed > maxSpeed) {
			currSpeed = maxSpeed;
		} else if (currSpeed < -maxSpeed) {
			currSpeed = -maxSpeed;
		}
		return currSpeed;
	}

	public override void OnStartLocalPlayer() {
		var m = Camera.main;
		m.GetComponent<CameraFollow>().setTarget(gameObject.transform);
	}
}

/*
 * if (v > 0) {
			if (currSpeed > 0.01)
				currSpeed *= 1.1f;
			else if (currSpeed < -0.01)
				currSpeed /= 1.1f;
			else
				currSpeed += 0.01f;
		} else if (v < 0) {
			if (currSpeed > 0.01)
				currSpeed /= 1.1f;
			else if (currSpeed < -0.01)
				currSpeed *= 1.1f;
			else
				currSpeed -= 0.01f;
		} else {
			if (currSpeed > -0.05 && currSpeed < 0.05)
				currSpeed = 0;
			else currSpeed /= 1.1f;
		}
*/