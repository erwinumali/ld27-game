using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public GameObject player;
	
	public Texture reticule;
	
	public GUISkin guiSkin;
	public GUIStyle textStyle;
	
	private string intensity;
	
	private InputController lightTimer;
	void Start() {
		lightTimer = (InputController) player.GetComponent(typeof(InputController));
	}	
	
	void Update() {
			
	}
	
	void OnGUI() {
		GUI.skin = guiSkin;
		
		GUI.DrawTexture(
			new Rect((Screen.width/2), (Screen.height/2), 10, 10), 
			reticule);

		GUI.HorizontalScrollbar(new Rect(0, (Screen.height - 15), 
			Screen.width, 20), 0, 
			lightTimer.GetRemainingTime (),0, lightTimer.maxDuration);
		
		GUI.Label(
			new Rect((Screen.width), (Screen.height - 40), 60, 20), 
			((Mathf.Round(lightTimer.GetRemainingTime()))).ToString ());
		
	}
}
