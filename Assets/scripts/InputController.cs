using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	
	public Light light;
	
	public float lightRechargeRate = 0.9f;
	public float delayBeforeLightRecharge = 0.2f;
	
	// for left click light
	public float intensityBoost = 1.0f;
	// for right click light
	public float rangeBoost = 30.0f;
	public float intensityMultiplier = 4.0f;
	public float spotAngleModifier = 8.0f;
	
	public float maxDuration = 15.0f;
	
	public AudioSource flashlightClickSound;
	
	static float currentRange;
	
	private float origRange;
	private float origIntensity;
	private float origAngle;
	private Vector3 origPosition;
	
	private float elapsedTime = 0;
	private float lightIdleTime = 0;
	// Use this for initialization
	
	public float GetRemainingTime() {
		return maxDuration - elapsedTime;	
	}
	
	void Start () {
		origRange = light.range;
		origIntensity = light.intensity;
		origAngle = light.spotAngle;
		origPosition = light.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButton(0) && elapsedTime < maxDuration){
			elapsedTime += Time.deltaTime;
			light.intensity = (origIntensity + intensityBoost) * ((maxDuration - elapsedTime)/maxDuration);
			lightIdleTime = 0;
			
			
		} else if(Input.GetMouseButton(1) && elapsedTime < maxDuration){
			light.transform.localPosition = Vector3.zero;
			elapsedTime += Time.deltaTime;
			currentRange = (origRange + rangeBoost);
			light.range = currentRange;
			light.spotAngle = spotAngleModifier;
			light.intensity = (origIntensity + intensityBoost*intensityMultiplier) * ((maxDuration - elapsedTime)/maxDuration);			
			lightIdleTime = 0;
			
		}
		else {
			lightIdleTime += Time.deltaTime;
			
			light.transform.localPosition = origPosition;
			light.range = origRange;
			light.intensity = origIntensity;
			light.spotAngle = origAngle;
			
			if(lightIdleTime >= delayBeforeLightRecharge){
				elapsedTime -= Time.deltaTime * lightRechargeRate;
			}
			
			if(elapsedTime <= 0){
				elapsedTime = 0;
			}
		}
		
		
		//flashlight sound
		if( Input.GetMouseButtonDown(0) || 
			Input.GetMouseButtonDown(1) ||
			Input.GetMouseButtonUp(0)	||
			Input.GetMouseButtonUp(1)	  ){
			
			flashlightClickSound.Play();	
		}
		
		
	}
}
