using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Actor))]
public abstract class IAction : MonoBehaviour {

	protected Actor actor;

	protected virtual void Awake(){
		actor = GetComponent<Actor>();
	}

	public abstract void Act();
}
