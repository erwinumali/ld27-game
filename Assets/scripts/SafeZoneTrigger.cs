using UnityEngine;
using System.Collections;

public class SafeZoneTrigger : MonoBehaviour {

	
	private bool isInSafeZone = false;
	
	public bool isPlayerSafe(){
		return isInSafeZone;	
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider col){
		if(col.tag == "Player"){
			Debug.Log ("in safe zone, no triggers");
			isInSafeZone = true;	
		}
	}
	
	void OnTriggerExit(Collider col){
		if(col.tag == "Player"){
			Debug.Log ("out of safe zone, trigger!");
			isInSafeZone = false;	
		}
	}
}
