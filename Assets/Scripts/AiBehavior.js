#pragma strict
//Andrew Miller (anmcmill@ucsc.edu) 
//CMPS 179 S14
//Hoarderlands
//
//Aibehavior.js this script will intigrate the CanSeePlayer.js, MoveToWaypoint, and ChasePlayer
//AI scripts to make NPC bahavior.

//
//for raycasting and player detection
//
var playerObject : GameObject; //player
var patrolPause : float; //how long the NPC will wait when it reaches a waypoint
//var persuitModivation : float; //this is the amount of time that the NPC will chase the player.
var persuitRange: float; //how far away before the NPC will give up the search
var fovRange : float; //Feild of View Ranger
var minPlayerDetectDistance : float; //how close can the player get
var rayRange : float; // distance in front.
private var rayDirection = Vector3.zero;
private var timer : float = 0;
private var playerSpotted : boolean = false;

//
//Waypoint array that can be used in the inspector
//
var waypoints : Transform[];
var priorityWaypoint : Transform;
private var currentWaypoint : Transform;
private var currentIndex : int;
private var agent: NavMeshAgent;
//NPC Movement
//var moveSpeed : float = 10.0;
var minDistance : float = 2.0;


function Start () {
	var curTransform : Transform;		
	curTransform = gameObject.GetComponent(Transform);
	agent = GetComponent.<NavMeshAgent>();
	currentWaypoint = waypoints[0];
	currentIndex = 0;
}

function Update () {
	var distToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);
	//if the player is not seen the NPC will move to waypoints.
	if(!CanSeePlayer() && playerSpotted == false)
	{
		patrolWaypoints();
		
	}
	else if(CanSeePlayer())
	{ //triggered when the player is spotted
		
		playerSpotted = true;
		//while the player is within a distance of the NPC
		chasePlayer();
		Debug.Log("Chasing the Player");
	}
	else if(distToPlayer <= persuitRange && playerSpotted == true)
	{
			Debug.Log("Searching the Player");
			chasePlayer();
	}
	else
	{
		Debug.Log("Player lost.");
		playerSpotted = false;
	}
}
 
//
//This is what the NPC will be doing most of its time. The NPC will move between waypoints
//wait an amount of time, and then move to the next one.
//
function patrolWaypoints () {

	MoveToWaypoint();
	
	
	if(Vector3.Distance(currentWaypoint.transform.position, transform.position) < minDistance)
	{
		yield WaitForSeconds(patrolPause);
		Debug.Log("Moving to next Waypoint");
		currentIndex++;
		//resests after cycling though all waypoints.
		if(currentIndex > waypoints.Length - 1)
		{
			currentIndex = 0;
		}
		currentWaypoint = waypoints[currentIndex];
	}
}

//
//Will follow the player by moving to their position.
//
function chasePlayer() {
	//commented out reset so it will continue its previous path
	//agent.ResetPath();
	//var lastLocation;
	
	agent.SetDestination(playerObject.transform.position);
	
	//if the player goes out of sight then the NPC will move to the players last known location.
	//if(CanSeePlayer() == false)
	//{
	//	Debug.Log("Out of Sight, moving to last location");
	//	lastLocation = playerObject.transform.position;
	//	agent.SetDestination(lastLocation);	
	//}
	//Debug.Log("Reseting path, moving to player");


}


//
//moves the NPC to the current waypoint
//
function MoveToWaypoint() : void{ agent.SetDestination(currentWaypoint.transform.position);}
	
	
 
//
//Returns true of the player is caught in the NPC's raycast
//Returns false if no player is detected
// 
function CanSeePlayer() : boolean
{
	var hit : RaycastHit;
	rayDirection = playerObject.transform.position - transform.position; //aims ray in direction of the player
	var distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);
	
	//for general proximity to the NPC
	if(Physics.Raycast (transform.position, rayDirection, hit))
	{
		if((hit.transform.tag == "Player") && (distanceToPlayer <= minPlayerDetectDistance))
		{
			Debug.Log("Player within intimate proximity...");
			return true;
		}	
		
	}
	
	//for the specified FOV of the NPC
	if((Vector3.Angle(rayDirection, transform.forward)) < fovRange)
	{
		if(Physics.Raycast(transform.position, rayDirection, hit, rayRange))
		{
			if(hit.transform.tag == "Player")
			{
				Debug.Log("Player Spotted!");
				return true;
			}else{
				return false;
			}
		}
	}
	//if nothing is found
	return false;
}

//
//Some Debuging stuff that lets you see the extent of the raycasting
//
function OnDrawGizmosSelected ()
{
// Draws a line in front of the player and one behind this is used to visually illustrate the detection ranges in front and behind the enemy
Gizmos.color = Color.magenta; // the color used to detect the player in front
Gizmos.DrawRay (transform.position, transform.forward * rayRange);
Gizmos.color = Color.yellow; // the color used to detect the player from behind
Gizmos.DrawRay (transform.position, transform.forward * -minPlayerDetectDistance);
}