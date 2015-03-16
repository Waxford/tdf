using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class Mover : MonoBehaviour {

	[SerializeField]
	protected float walkSpeed;
	[SerializeField]
	protected float runSpeed;
	[SerializeField]
	protected float slowTurnSpeed;
	[SerializeField]
	protected float fastTurnSpeed;

	protected bool running = false;

	[HideInInspector]
	public CircleCollider2D circleCollider;

	protected virtual void Awake(){
		circleCollider = this.GetComponent<CircleCollider2D>();
	}

	protected void MoveFrame(float magnitude){
		float moveSpeed = walkSpeed;
		if(running){
			moveSpeed = runSpeed;
		}
		this.transform.position = this.transform.position + this.transform.up * magnitude * moveSpeed * Time.deltaTime;
	}
	protected void TurnFrame(float magnitude){
		Debug.LogWarning("Mag: "+magnitude);
		float turnSpeed = slowTurnSpeed;
		if(running){
			turnSpeed = fastTurnSpeed;
		}
		this.transform.RotateAround(this.transform.position, Vector3.back, turnSpeed * (180f/Mathf.PI) * magnitude * Time.deltaTime);
	}

}
