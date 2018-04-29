using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform playerTransform;
	public int depth = -100;

	// Update is called once per frame
	void Update()
	{
		if (playerTransform == null)
			return;
		transform.position = playerTransform.position + new Vector3(0,0,-100);
	}

	public void setTarget(Transform target)
	{
		playerTransform = target;
	}
}