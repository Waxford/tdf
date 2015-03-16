using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class CarWanderer : Wanderer {

	[SerializeField]
	private float minPivotPeriod = 0.5f;

	private float lastPivot = 0f;
	private float forwardWeight = 0f;
	
	protected override void Update(){
		base.Update();
		if(waypoint == null || (waypoint.transform.position - this.transform.position).sqrMagnitude < sqrWaypointAwarenessRange){
			return;
		}
		float newForwardWeight = Vector3.Dot(this.transform.up, (waypoint.transform.position - this.transform.position)) > 0 ? 1f : -1f;
		if(newForwardWeight != forwardWeight && lastPivot + minPivotPeriod < Time.time){
			lastPivot = Time.time;
			forwardWeight = newForwardWeight;
		}
		MoveFrame(forwardWeight);
		TurnFrame(Vector3.Dot(this.transform.right, (waypoint.transform.position - this.transform.position)) > 0 ? 1f : -1f);
	}

	protected override void Awake(){
		base.Awake();
	}

}
