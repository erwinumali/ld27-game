using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	
	public Light light;
	
	public float rangeBoost = 1;
	public float intensityBoost = 3.0f;
	public float maxDuration = 10.0f;
	
	static float currentRange;
	
	private float origRange;
	private float origIntensity;
	
	private float elapsedTime = 0;
	// Use this for initialization
	
	public float GetRemainingTime() {
		return maxDuration - elapsedTime;	
	}
	
	void Start () {
		origRange = light.range;
		origIntensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButton(0)){
			elapsedTime += Time.deltaTime;
			currentRange = (origRange + rangeBoost);
			light.range = currentRange;
			light.intensity = (origIntensity + intensityBoost) * ((maxDuration - elapsedTime)/maxDuration);
		}
		else {
			light.range = origRange;
			light.intensity = origIntensity;
			elapsedTime -= Time.deltaTime;
			
			if(elapsedTime <= 0){
				elapsedTime = 0;
			}
		}
	}
}
