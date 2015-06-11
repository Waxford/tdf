using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Light))]
public class Flicker : MonoBehaviour {

	[SerializeField]
	private Vector2 intensityRange;
	[SerializeField]
	private Vector2 flickerPeriodRange;

	private Light flickerLight;
	private float lastFlickerTime = 0f;
	private float targetFlickerTime = 0f;
	private float lastIntensity = 0f;
	private float targetIntensity = 0f;

	void Awake () {
		flickerLight = GetComponent<Light>();
		flickerLight.intensity = intensityRange.x + Random.value * (intensityRange.y - intensityRange.x);
		SetNewFlicker();
	}

	void Update () {
		float dt = Time.time - lastFlickerTime;
		float pct = dt / (targetFlickerTime - lastFlickerTime);
		flickerLight.intensity = Mathf.Lerp(lastIntensity, targetIntensity, pct);
		if(pct >= 1f){
			SetNewFlicker();
		}
	}

	void SetNewFlicker() {
		lastIntensity = flickerLight.intensity;
		targetIntensity = intensityRange.x + Random.value * (intensityRange.y - intensityRange.x);
		lastFlickerTime = Time.time;
		targetFlickerTime = lastFlickerTime + flickerPeriodRange.x + Random.value * (flickerPeriodRange.y - flickerPeriodRange.x);
	}
}
