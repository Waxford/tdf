using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class Wanderer : Mover {

	[SerializeField]
	protected float wanderRange = 10f;
	[SerializeField]
	protected float waypointAwarenessRange = 1f;

	[SerializeField]
	protected Transform waypoint = null;
	[SerializeField]
	protected bool consumeWaypoint = true;
	
	protected float sqrWaypointAwarenessRange;

	private Vector3 initialPosition;

	protected virtual void Update(){
		if(waypoint == null || (waypoint.transform.position - this.transform.position).sqrMagnitude < sqrWaypointAwarenessRange){
			if(consumeWaypoint){
				CreateNewWaypoint();
			}
			return;
		}
	}

	protected override void Awake(){
		base.Awake();
		this.initialPosition = this.transform.position;
		this.sqrWaypointAwarenessRange = waypointAwarenessRange * waypointAwarenessRange;
	}

	private void CreateNewWaypoint(){
		if(waypoint != null){
			GameObject.Destroy(waypoint.gameObject);
		}
		GameObject newWaypoint = new GameObject("waypoint");
		newWaypoint.transform.position = initialPosition + Random.insideUnitCircle.xyz() * wanderRange;
		waypoint = newWaypoint.transform;
	}

}
