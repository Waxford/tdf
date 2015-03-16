using UnityEngine;
using System.Collections;

public class Strike : IAction {

	[SerializeField]
	private float strikeRadius = 0.5f;

	public override void Act(){
		foreach(Collider2D col in Physics2D.OverlapCircleAll(this.transform.position.xy() + this.transform.up.xy() * this.actor.circleCollider.radius, strikeRadius)){
			Strikable s = col.GetComponent<Strikable>();
			if(s != null && s.gameObject != this.gameObject){
				GameObject.Destroy(s.gameObject);
			}
		}
		GameObject strikeDebugGo = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		strikeDebugGo.transform.position = this.transform.position + this.transform.up * this.actor.circleCollider.radius;
		strikeDebugGo.transform.localScale = Vector3.one * strikeRadius;
		Timer.DoAfterSeconds(0.3f,()=>{
			GameObject.Destroy(strikeDebugGo);
		});
	}

}
