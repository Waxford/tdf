using UnityEngine;
using System.Collections;

public class SunRotation : MonoBehaviour {

	public float rotationSpeed = 1f;

	void Update() {
		transform.RotateAround(transform.position, transform.up, rotationSpeed * 180f/Mathf.PI * Time.deltaTime);
	}
}
