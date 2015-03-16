using UnityEngine;
using System.Collections;

public class Actor : Mover {

	[SerializeField]
	private IAction action1;
	[SerializeField]
	private IAction action2;

	[SerializeField]
	private float action1Period;
	[SerializeField]
	private float action2Period;

	private float lastAction1;
	private float lastAction2;

	protected override void Awake(){
		base.Awake();
		lastAction1 = 0f;
		lastAction2 = 0f;
	}

	protected bool CanDoAction1{
		get {
			return action1 != null && lastAction1 + action1Period < Time.time;
		}
	}

	protected bool CanDoAction2{
		get {
			return action2 != null && lastAction2 + action2Period < Time.time;
		}
	}

	protected void TryDoAction1(){
		if(CanDoAction1){
			action1.Act();
			lastAction1 = Time.time;
		}
	}

	protected void TryDoAction2(){
		if(CanDoAction2){
			action2.Act();
			lastAction2 = Time.time;
		}
	}

}
