#pragma strict

// from http://answers.unity3d.com/questions/164638/how-to-make-the-fps-character-controller-run-and-c.html
var walkSpeed: float = 7; // regular speed
var crchSpeed: float = 3; // crouching speed
var runSpeed: float = 20; // run speed

var runRecoveryFactor = 0.7;
var maxRunDuration: float = 10; //run duration

private var chMotor: CharacterMotor;
private var ch: CharacterController;
private var tr: Transform;
private var height: float; // initial height
 
private var currRunTime: float;

private var isTired: boolean;    
    
function GetRemainingRunTime(){
	return maxRunDuration - currRunTime;
}
    
function Start(){
    chMotor = GetComponent(CharacterMotor);
    tr = transform;
    ch = GetComponent(CharacterController);
	height = ch.height;
}
 
function Update(){
 
    var h = height;
    var speed = walkSpeed;
    
    if(currRunTime >= maxRunDuration){
//    	Debug.Log("is tired!");
    	isTired = true;
   	}
   	
   	if(isTired){
//   	Debug.Log("tired! recharging!");
   		currRunTime -= Time.deltaTime * runRecoveryFactor;
   		if(currRunTime <= 0){
//   		Debug.Log("finished recharging! go!");
   			isTired = false;
   			currRunTime = 0;
   		}
   	}
    
    if ((ch.isGrounded && Input.GetKey("left shift") || 
    	Input.GetKey("right shift")) &&
    	!isTired){
//    	Debug.Log("running!");
    	currRunTime += Time.deltaTime;
        speed = runSpeed;
        
    } else if(!isTired) {
//    	Debug.Log("not tired, but recharging!");
    	currRunTime -= Time.deltaTime;
    	if(currRunTime <= 0){
//    		Debug.Log("Fully charged!");
    		currRunTime = 0;
    	}
    }
    
    if (Input.GetKey("c")){ // press C to crouch
        h = 0.5 * height;
        speed = crchSpeed; // slow down when crouching
    }
    chMotor.movement.maxForwardSpeed = speed; // set max speed
    var lastHeight = ch.height; // crouch/stand up smoothly 
    ch.height = Mathf.Lerp(ch.height, h, 5*Time.deltaTime);
    tr.position.y += (ch.height-lastHeight)/2; // fix vertical position
}
