using UnityEngine;
using System.Collections;

public class PathGenerator : MonoBehaviour {
	
	public GameObject hallSection;
	public GameObject wallSection;
	public GameObject floor; 
	 
	public GameObject exitDoor;
	
	// need to be set properly
	public int floorSize = 4;
	public int wallLength = 4;
	 
	public int floorLevel = -1;  
	// means startFloorSpace length on each side
	public int startFloorSpace = 4;
	  
	private Transform startPosition;
	 
	private int xOffset;
	private int zOffset;
	private Vector3 posVec = Vector3.up;
	
	private float yOffset = 0.0f;
	
	private Quaternion yRotation = Quaternion.identity;
	
	void Start () {
		startPosition = gameObject.transform;  
		 
		posVec = new Vector3(					
					startPosition.position.x, 
					floorLevel, 
					startPosition.position.z);
		GameObject.Instantiate (floor, posVec, Quaternion.identity);
		// fill out initial area in cross pattern
		for(int i = 0; i < 4; i++){
			for(int j = 0; j < startFloorSpace; j++){ 
				// clockwise rotation from positive x axis
				if(i == 0){ // right
					xOffset = floorSize;
					zOffset = 0;
					yOffset = 0.0f;
					
					yRotation = Quaternion.Euler(0, 90.0f, 0);
				} else if(i == 1){ // down
					xOffset = 0;
					zOffset = -floorSize;
					yOffset =0.0f;
					
					yRotation = Quaternion.identity;
				} else if(i == 2){ // left
					xOffset = -floorSize;
					zOffset = 0;
					yOffset = 0.0f;
					
					yRotation = Quaternion.Euler (0, 90.0f, 0);
				} else if(i == 3){ // up
					xOffset = 0;
					zOffset = floorSize;
					yOffset = 0.3f;
					
					yRotation = Quaternion.identity;
					
				}
				
				posVec = new Vector3(
					startPosition.position.x + + xOffset + (xOffset*j), 
					floorLevel + (yOffset*j), 
					startPosition.position.z + zOffset + (zOffset*j)
					);
				
				GameObject.Instantiate(hallSection, posVec, yRotation);
				
				if(i == 3 && j == startFloorSpace-1){
					posVec = new Vector3(
						startPosition.position.x + + xOffset + (xOffset*j), 
						floorLevel + (yOffset*j) + 2, 
						startPosition.position.z + zOffset + (zOffset*j)
						);					
					GameObject.Instantiate (exitDoor, posVec, Quaternion.identity);	
				}

				
					
			}
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
