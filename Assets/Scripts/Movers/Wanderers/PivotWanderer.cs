using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class PivotWanderer : Wanderer {
	
	private float walkBlindspotRadius;
	private float runBlindspotRadius;
	
	private bool WaypointInBlindspot{
		get {
			if(waypoint == null){
				return true;
			}
			Vector3 leftBlindspot = this.transform.position + this.transform.right * walkBlindspotRadius;
			Vector3 rightBlindspot = this.transform.position - this.transform.right * walkBlindspotRadius;
			if((waypoint.position - leftBlindspot).magnitude < walkBlindspotRadius){
				return true;
			}
			if((waypoint.position - rightBlindspot).magnitude < walkBlindspotRadius){
				return true;
			}
			return false;
		}
	}

	protected override void Update(){
		base.Update();
		if(waypoint == null || (waypoint.transform.position - this.transform.position).sqrMagnitude < sqrWaypointAwarenessRange){
			return;
		}
		if(!WaypointInBlindspot){
			MoveFrame(Vector3.Dot(this.transform.up, (waypoint.transform.position - this.transform.position)) > 0 ? 1f : 0f);
		}
		TurnFrame(Vector3.Dot(this.transform.right, (waypoint.transform.position - this.transform.position)) > 0 ? 1f : -1f);
	}

	protected override void Awake(){
		base.Awake();
		this.walkBlindspotRadius = Mathf.Sqrt((walkSpeed * walkSpeed) / (slowTurnSpeed * slowTurnSpeed));
		this.runBlindspotRadius = Mathf.Sqrt((runSpeed * runSpeed) / (fastTurnSpeed * fastTurnSpeed));
	}

}
