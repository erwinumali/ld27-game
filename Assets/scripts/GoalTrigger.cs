using UnityEngine;
using System.Collections;

public class GoalTrigger : MonoBehaviour {
	
	
	private bool isFinished = false;
	
	public bool isFinishedGame(){
		return isFinished;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col){
		if(col.tag == "Player"){
			isFinished = true;
		}
	}
}
