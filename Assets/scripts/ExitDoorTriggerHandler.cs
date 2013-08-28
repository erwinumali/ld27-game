using UnityEngine;
using System.Collections;

public class ExitDoorTriggerHandler : MonoBehaviour {
	
	public GameObject platform1;
	public GameObject platform2;
	public GameObject platform3;
	
	public GameObject westBeacon;
	
	public GameObject exitDoor;
	
	public AudioSource raiseSound;
	
	private float triggeredPlatforms = 0;
	
	private ExitDoorPlatformTrigger p1;
	private ExitDoorPlatformTrigger p2;
	private ExitDoorPlatformTrigger p3;
	
	private bool trig1 = false;
	private bool trig2 = false;
	private bool trig3 = false;
	
	
	public float getNumberOfFinishedPlatforms() {
		return triggeredPlatforms;	
	}
	// Use this for initialization
	void Start () {
		p1 = (ExitDoorPlatformTrigger) platform1.GetComponent(typeof(ExitDoorPlatformTrigger));
		p2 = (ExitDoorPlatformTrigger) platform2.GetComponent(typeof(ExitDoorPlatformTrigger));
		p3 = (ExitDoorPlatformTrigger) platform3.GetComponent(typeof(ExitDoorPlatformTrigger));		
	}
	
	// Update is called once per frame
	void Update () {
		if(p1.IsTriggered() && !trig1){
			triggeredPlatforms += 1;
			trig1 = true;
			Debug.Log ("first platform triggered!");
		}
		if(p2.IsTriggered() && !trig2){
			triggeredPlatforms += 1;
			trig2 = true;
			Debug.Log ("2nd platform triggered!");
		}
		if(p3.IsTriggered() && !trig3){
			triggeredPlatforms += 1;
			trig3 = true;
			Debug.Log ("3rd platform triggered!");
		}	
		
		if(triggeredPlatforms >= 3){
			westBeacon.SetActive (false);
			raiseSound.Play ();
			
			//GameObject.Find ("__WestWingEnemyHandler").SetActive(false);
			
			exitDoor.transform.position = new Vector3(
				exitDoor.transform.position.x,
				exitDoor.transform.position.y + 5,
				exitDoor.transform.position.z);
			
		}
	}
}
