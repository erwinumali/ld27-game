using UnityEngine;
using System.Collections;

public class ExitDoorPlatformTrigger : MonoBehaviour {
	
	private bool isTriggered = false; 
	
	public bool IsTriggered(){
		return isTriggered;	
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnTriggerStay(Collider col){
		if(col.tag == "Player" && isTriggered == false){
			
			isTriggered = true;
			renderer.enabled = false;
			
			// NEED TO PUT AUDIOSOURCE ON PLATFORM
			audio.Play ();
			
			Debug.Log ("platform triggered!");
		}
	}
}
