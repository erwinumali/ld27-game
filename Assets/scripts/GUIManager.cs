using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public GameObject player;
	public GameObject worldHandler;
	public GameObject westEnemyHandler;
	public GameObject goalTrigger;
	
	public Texture reticule;
	public Texture lightIcon;
	public Texture sprintIcon;
	public Texture pauseOverlay;
	public Texture gameOverOverlay;
	
	public GUISkin guiSkin;
	
	public Color flashlightColor = Color.white;
	public Color runBarColor = Color.white;
	
	public GUIStyle textStyle;
	
	private string textStatus;
	private string textPlatform;
	
	
	private InputController lightTimer;
	private CharacterMotorModifier runTimer;
	private GameFocusHandler focusChecker;
	private WestWingEnemyHandler wweh;
	private ExitDoorTriggerHandler edth;
	private GoalTrigger gt;
	
	private WorldStatsHandler wsh;
	
	private float minutes;
	private float seconds;
	
	private float rawTime;
	
	void Start() {
		lightTimer = (InputController) player.GetComponent(typeof(InputController));
		runTimer = (CharacterMotorModifier) player.GetComponent(typeof(CharacterMotorModifier));
		focusChecker = (GameFocusHandler) worldHandler.GetComponent (typeof(GameFocusHandler));
		wweh = (WestWingEnemyHandler) westEnemyHandler.GetComponent (typeof(WestWingEnemyHandler));
		edth = (ExitDoorTriggerHandler) worldHandler.GetComponent (typeof(ExitDoorTriggerHandler));
		wsh = (WorldStatsHandler) worldHandler.GetComponent (typeof(WorldStatsHandler));
		gt = (GoalTrigger) goalTrigger.GetComponent(typeof(GoalTrigger));
		
		guiSkin.horizontalScrollbarThumb.fixedHeight = 6.0f;
	}	
	
	void Update() {
			
	}
	
	void OnGUI() {
		GUI.skin = guiSkin;
		
		// if not paused
		if(focusChecker.CursorIsLocked() && !wweh.IsEnemyTrigger() && !gt.isFinishedGame()){
			GUI.DrawTexture(
				new Rect((Screen.width/2), (Screen.height/2), 10, 10), 
				reticule);
			
			// flashlight gauge
			GUI.color = flashlightColor;
			GUI.HorizontalScrollbar(new Rect(Screen.height/32, (Screen.height/2 + Screen.height/4 + Screen.height/5), 
				Screen.width*0.96f, 5), 0, 
				lightTimer.GetRemainingTime() ,0, lightTimer.maxDuration);
			
			// sprint gauge
			GUI.color = runBarColor;
			GUI.HorizontalScrollbar(new Rect(Screen.height/32, (Screen.height/2 + Screen.height/4 + Screen.height/5 + 10), 
				Screen.width*0.96f, 5), 0, 
				runTimer.GetRemainingRunTime() ,0, runTimer.maxRunDuration);
			
			textStatus = (Mathf.Round(lightTimer.GetRemainingTime())).ToString() + " | " + (Mathf.Round(runTimer.GetRemainingRunTime())).ToString();
			
			// text labels
			GUI.color = Color.white;
			textStyle.alignment = TextAnchor.LowerLeft;
			textStyle.fontSize = 32;
			GUI.Label(
				new Rect((Screen.width/48), (Screen.height/2 + Screen.height/4 + Screen.height/5 + 10), 50, 20), textStatus, textStyle);
			
			if(wweh.IsWestWingTriggered()){
				//GUI.Label(
				//	new Rect((Screen.width/2), (Screen.height/32), 50, 20), "RUN.", textStyle);
				
				textPlatform = edth.getNumberOfFinishedPlatforms().ToString() + " / 3";
			
				GUI.Label(
					new Rect((Screen.width/2 - 10), (Screen.height/16), 50, 20), textPlatform, textStyle);
				
				if(edth.getNumberOfFinishedPlatforms() >= 3){
					GUI.Label(
						new Rect((Screen.width/2 - 30), (Screen.height/16 + 32), 50, 20), "Door opened", textStyle);						
				}
			}
			

		// if dead or done
		} else if(wweh.IsEnemyTrigger() || gt.isFinishedGame()){
			Time.timeScale = 0;
			
			GUI.DrawTexture(
				new Rect(0, 0, Screen.width, Screen.height), 
				gameOverOverlay);
			
			textStyle.fontSize = 32;
			if(wweh.IsEnemyTrigger()){
				GUI.Label(
					new Rect((Screen.width/3 - 30), (Screen.height/3 + 32), 50, 20), "You were too slow.", textStyle);
			} else {
				GUI.Label(
					new Rect((Screen.width/3 - 30), (Screen.height/3 + 32), 50, 20), "You've escaped!", textStyle);				
			}
			
			rawTime = wsh.getElapsedTime();

			
			GUI.Label(
				new Rect((Screen.width/3 - 30), (Screen.height/2), 50, 20), "Your time: " + Mathf.Floor(rawTime / 60).ToString("00") + ":" + (rawTime % 60).ToString("00"), textStyle);
			
			textStyle.fontSize = 16;
			GUI.Label(
				new Rect((Screen.width/3 - 30), (Screen.height/2 + 48), 50, 20), "Press Backspace to restart.", textStyle);
			
			
		// if paused
		} else {
			GUI.DrawTexture(
				new Rect(0, 0, Screen.width, Screen.height), 
				pauseOverlay);
			
			textStyle.alignment = TextAnchor.MiddleCenter;
			textStyle.fontSize = 32;			
			GUI.Label(new Rect((Screen.width/2), (Screen.height/2 - 30), 60, 60), "Paused", textStyle);
			textStyle.fontSize = 16;
			GUI.Label(new Rect((Screen.width/2), (Screen.height/2), 60, 60), "Click anywhere to continue", textStyle);
			
			GUI.Label(new Rect((Screen.width/2), (Screen.height/2 + 30), 60, 60), "Press Backspace to restart", textStyle);
				
		}
		
		
	}
}
