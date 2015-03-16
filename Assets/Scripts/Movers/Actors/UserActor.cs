using UnityEngine;
using System.Collections;

public class UserActor : Actor {

	void Update(){
		this.MoveFrame(Input.GetAxisRaw("Forward"));
		this.TurnFrame(Input.GetAxisRaw("Right"));
		if(Input.GetButton("Action1")){
			this.TryDoAction1();
		}
		if(Input.GetButton("Action2")){
			this.TryDoAction2();
		}
		this.running = Input.GetButton("Run");
	}

}
