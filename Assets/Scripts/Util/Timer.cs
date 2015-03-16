using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	private static Timer instance;
	public static Timer Instance{
		get{
			if(instance == null){
				GameObject instanceGo = GameObject.Find("Timer");
				if(instanceGo == null){
					instanceGo = new GameObject("Timer");
					DontDestroyOnLoad(instanceGo);
				}
				instance = instanceGo.GetComponent<Timer>();
				if(instance == null){
					instance = instanceGo.AddComponent<Timer>();
				}
			}
			return instance;
		}
	}

	public static void DoAfterSeconds(float seconds, System.Action callback){
		Timer.Instance.StartCoroutine(Timer.Instance.DoAfterSecondsInternal(seconds, callback));
	}

	private IEnumerator DoAfterSecondsInternal(float seconds, System.Action callback){
		yield return new WaitForSeconds(seconds);
		callback();
	}

}
