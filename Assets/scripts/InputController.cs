using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	
	public Light light;
	
	public float rangeBoost = 20;
	public float intensityBoost = 0.8f;
	public float maxDuration = 10.0f;
	
	public float currentRange;
	
	private float origRange;
	private float origIntensity;
	
	private float elapsedTime = 0;
	// Use this for initialization
	void Start () {
		origRange = light.range;
		origIntensity = light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButton(0)){
			elapsedTime += Time.deltaTime;
			currentRange = (origRange + rangeBoost) * ((maxDuration - elapsedTime)/maxDuration);
			light.range = currentRange;
			light.intensity = (origIntensity + intensityBoost);
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
