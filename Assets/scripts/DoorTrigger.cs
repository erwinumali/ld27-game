using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {
	
	public GameObject door;
	public float maxHeight = 5.0f;
	
	public float duration = 10.0f;
	
	public float delayToActivation = 0.0f;
	public float delayToDeactivation = 1.0f;
	
	private AudioSource soundEffect;
	
	private bool isEmpty = true;
	private bool isClosed = true;
	
	private float origHeight;
	private float currentHeight;
	
	private float timeSinceOccupied;
	private float timeSinceEmpty;
	private float tempY;
	// Use this for initialization
	
	void closeDoor(){
			
	}
	
	void Start () {
		timeSinceEmpty = 0.0f;
		timeSinceOccupied = 0.0f;
		
		origHeight = door.transform.position.y;
		
		currentHeight = 0.0f;
		soundEffect = (AudioSource) door.GetComponent(typeof(AudioSource));
	}
	
	// Update is called once per frame
	void Update () {
		if(isEmpty){
			timeSinceEmpty += Time.deltaTime;
			if(timeSinceEmpty < delayToDeactivation){
				soundEffect.Stop ();	
			}
			if(timeSinceEmpty >= delayToDeactivation){
//				Debug.Log ("lowering");
				if(currentHeight >= origHeight){
					currentHeight = door.transform.position.y - ( (maxHeight) / duration ) * Time.deltaTime;
					door.transform.position = 
						new Vector3(door.transform.position.x,
									currentHeight,
									door.transform.position.z);
					if(!soundEffect.isPlaying){
						soundEffect.Play();	
					}
				}
				timeSinceOccupied = 0;
			}
		} else {
			timeSinceOccupied += Time.deltaTime;
			if(timeSinceOccupied < delayToActivation){
				soundEffect.Stop ();	
			}
			if(timeSinceOccupied >= delayToActivation){
//				Debug.Log ("raising");
				if(currentHeight <= maxHeight){
					currentHeight = door.transform.position.y + ( (maxHeight) / duration ) * Time.deltaTime;
					door.transform.position = 
						new Vector3(door.transform.position.x,
									currentHeight,
									door.transform.position.z);
					
					if(!soundEffect.isPlaying){
						soundEffect.Play();	
					}					
				}
				timeSinceEmpty = 0;
			}
		}
	}
	
	void OnTriggerEnter(Collider c){
		if(c.gameObject.tag == "Player"){
			Debug.Log ("player entered trigger");
			isEmpty = false;
		}
	}
	
	void OnTriggerExit(Collider c){
		if(c.gameObject.tag == "Player"){
			isEmpty = true;
		}	
	}
}
