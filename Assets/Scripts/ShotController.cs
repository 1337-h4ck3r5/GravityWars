using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {
	public Vector3 direction;
	public float speed;
	public GameObject localPlayer;

	int lifeFrames = 100;

	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3 (50, 50, 1);
	}

	void FixedUpdate() {
		if (lifeFrames < 1) {
			Destroy (this.gameObject);
			return;
		}
		lifeFrames--;

		this.transform.Translate (direction * speed, Space.World);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject != localPlayer && col.gameObject.tag != "shot"){
			Destroy(col.gameObject);
		}
	}
}
