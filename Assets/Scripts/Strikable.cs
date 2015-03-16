using UnityEngine;
using System.Collections;

public class Strikable : MonoBehaviour {

	public void Struck(){
		GameObject.Destroy(this.gameObject);
	}

}
