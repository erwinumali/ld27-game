using UnityEngine;
using System.Collections;

public class WorldStatsHandler : MonoBehaviour {
	
	
	private int numberOfDeaths;
	private float elapsedTime;
	
	public float getElapsedTime(){
		return elapsedTime;	
	}
	
	// Use this for initialization
	void Start () {
		elapsedTime = 0;
		numberOfDeaths = 0;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
	}
}
