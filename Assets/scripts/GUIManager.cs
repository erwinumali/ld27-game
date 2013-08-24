using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public Texture reticule;
	private GUIStyle textStyle;
	
	void Start() {
		textStyle = GUIStyle.none;
		textStyle.border = null;
		textStyle.normal.textColor = Color.white;
	}	
	
	void OnGUI() {
		GUI.DrawTexture(
			new Rect((Screen.width/2), (Screen.height/2), 10, 10), 
			reticule);
		
		GUI.TextArea(new Rect((Screen.width - 20), (Screen.height - 20), 20, 20), "Hello");
	}
}
