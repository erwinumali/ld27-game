using UnityEngine;
using System.Collections;

// from http://docs.unity3d.com/Documentation/ScriptReference/Screen-lockCursor.html
public class GameFocusHandler : MonoBehaviour {
	
	public GameObject player;
	public GameObject camera;
	
	private float originalTimeScale;
	private MouseLook lookController1;
	private MouseLook lookController2;

	private bool wasLocked = false;
	
	public bool CursorIsLocked(){
		return wasLocked;	
	}
	
    void DidLockCursor() {
        Debug.Log("Locking cursor");
		Time.timeScale = 1;	
		lookController1.enabled = true;
		lookController2.enabled = true;
    }
    void DidUnlockCursor() {
        Debug.Log("Unlocking cursor");
		Time.timeScale = 0;
		lookController1.enabled = false;
		lookController2.enabled = false;
    }
	
	void Start() {
		// inits game by locking cursor
		Screen.lockCursor = true;	
		originalTimeScale = Time.timeScale;
		lookController1 = (MouseLook) player.GetComponent(typeof(MouseLook));
		lookController2 = (MouseLook) camera.GetComponent(typeof(MouseLook));
	}
	
    void Update() {
		

		
		if (Input.GetKey(KeyCode.Backspace) && !wasLocked) {
			Debug.Log ("restarting");
			Application.LoadLevel(0);
		}
		
		if(((WestWingEnemyHandler)GameObject.Find("__WestWingEnemyHandler").GetComponent(typeof(WestWingEnemyHandler))).IsEnemyTrigger()){
			Screen.lockCursor = false;
		}

        if (Input.GetKeyDown("escape"))
            Screen.lockCursor = false;		
		
		if (Input.GetMouseButtonDown(0) ||
			Input.GetMouseButtonDown(1) ||
			Input.GetMouseButtonDown(2)){
			Screen.lockCursor = true;
		}
        
        if (!Screen.lockCursor && wasLocked) {
            wasLocked = false;
            DidUnlockCursor();
        } else
            if (Screen.lockCursor && !wasLocked) {
                wasLocked = true;
                DidLockCursor();
            }
		
		}
}