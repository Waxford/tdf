using UnityEngine;
using System.Collections;

public static class Extensions {

	#region Vectors
	public static Vector2 xy (this Vector3 vec3){
		return new Vector2(vec3.x, vec3.y);
	}

	public static Vector3 xyz (this Vector2 vec2){
		return new Vector3(vec2.x, vec2.y, 0f);
	}
	#endregion

}
