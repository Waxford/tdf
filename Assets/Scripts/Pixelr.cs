using UnityEngine;
using System.Collections;

public class Pixelr : MonoBehaviour {

	private Vector3 internalPosition;
	private Vector3 internalScale;
	private Vector3 lastSnappedPosition;
	private Vector3 lastSnappedScale;

	void Awake(){
		internalPosition = this.transform.position;
		internalScale = this.transform.localScale;
		lastSnappedPosition = this.transform.position;
		lastSnappedScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
