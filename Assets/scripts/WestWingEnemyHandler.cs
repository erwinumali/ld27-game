using UnityEngine;
using System.Collections;

public class WestWingEnemyHandler : MonoBehaviour {
	// to be attached to trigger that starts it
	
	public GameObject player;
	public Light playerFlashLight;
	
	public GameObject safeZone1;
	public GameObject safeZone2;
	public GameObject safeZone3;
	
	public float pollTime = 4.0f;
	public float maxEnemyPresenceTime = 10.0f;
	
	public AudioSource startCueSound;
	public AudioSource warningCueSound;
	public AudioSource dangerCueSound;
	
	public AudioSource warningAmbienceLayer;
	
	// -----------
	private CharacterMotor speedChecker;
	
	private bool isStarted = false;
	private bool isEnemyTriggered = false;
	
	private float elapsedTime = 0;
	
	private float enemyPresentTime = 0;
	
	private SafeZoneTrigger sft1;
	private SafeZoneTrigger sft2;
	private SafeZoneTrigger sft3;
	
	public bool IsWestWingTriggered(){
		return isStarted;	
	}
	
	public bool IsEnemyTrigger(){
		return isEnemyTriggered;
	}
	
	public float GetEnemyTime(){
		return enemyPresentTime;
	}
	
	// Use this for initialization
	void Start () {
		Random.seed = (int)Time.time;
		speedChecker = (CharacterMotor) player.GetComponent(typeof(CharacterMotor));
		
		sft1 = (SafeZoneTrigger) safeZone1.GetComponent(typeof(SafeZoneTrigger));
		sft2 = (SafeZoneTrigger) safeZone2.GetComponent(typeof(SafeZoneTrigger));
		sft3 = (SafeZoneTrigger) safeZone3.GetComponent(typeof(SafeZoneTrigger));

	}
	
	// Update is called once per frame
	void Update () {
		if(isStarted){
			elapsedTime += Time.deltaTime;
			if(	elapsedTime >= pollTime && 
				speedChecker.inputMoveDirection == Vector3.zero &&
				(!sft1.isPlayerSafe() &&
				 !sft2.isPlayerSafe() &&
				 !sft3.isPlayerSafe() )){
				Debug.Log ("poll time passed & player zero velocity!");
				
				warningCueSound.transform.position = player.transform.position;
				
				if(!warningAmbienceLayer.isPlaying){
					warningAmbienceLayer.Play ();	
				}
				
				warningCueSound.Play ();
				
				if(Random.Range(0,2) == 2){ // 33% chance
					isEnemyTriggered = true;
				} else {
	
				}
				
				elapsedTime = 0;
			}
			if( sft1.isPlayerSafe() &&
				sft2.isPlayerSafe() &&
				sft3.isPlayerSafe()){
				
				warningAmbienceLayer.Stop();	
			}
			
			if(sft3.isPlayerSafe()){
				isStarted = false;	
			}
		}
	}
	
	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			isStarted = true;
			startCueSound.Play ();
			this.audio.Play ();
			Debug.Log("West wing sequence begin");
		}
	}
}
