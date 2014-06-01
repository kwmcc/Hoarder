#pragma strict
//Andrew Miller (anmcmill@ucsc.edu) 
//CMPS 179 S14
//Hoarderlands
//
//Aibehavior.js this script will intigrate the CanSeePlayer.js, MoveToWaypoint, and ChasePlayer
//AI scripts to make NPC bahavior.

//
//this decales the MoveToWAypoint script	
//
//var patrol : MoveToWaypoint;
//patrol = gameObject.GetComponent(MoveToWaypoint);
//var search : CanSeePlayer;
//search = gameObject.GetComponent(CanSeePlayer);	

//
//for raycasting and player detection
//
var playerObject : GameObject; //player
var patrolPause : float; 
var fovRange : float; //Feild of View Ranger
var minPlayerDetectDistance : float; //how close can the player get
var rayRange : float; // distance in front.
private var rayDirection = Vector3.zero;

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
		
	//initializes the waypoints w/ navmesh stuff in that function
	//patrol.Start();
	
	agent = GetComponent.<NavMeshAgent>();
	currentWaypoint = waypoints[0];
	currentIndex = 0;
}

function Update () {
	//if the player is not seen the NPC will move to waypoints.
	if(!CanSeePlayer()){
		patrolWaypoints();
	}else{
		chasePlayer();
	}
}
 
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

function chasePlayer() {
	//commented out reset so it will continue its previous path
	//agent.ResetPath();
	var lastLocation;
	agent.SetDestination(playerObject.transform.position);
	
	//if the player goes out of sight then the NPC will move to the players last known location.
	if(CanSeePlayer() == false)
	{
		Debug.Log("Out of Sight, moving to last location");
		lastLocation = playerObject.transform.position;
		agent.SetDestination(lastLocation);	
	}
	//Debug.Log("Reseting path, moving to player");


}

function MoveToWaypoint() : void
{
	//var direction : Vector3 = currentWaypoint.transform.position - transform.position;
	//var moveVector : Vector3 = direction.normalized * moveSpeed * Time.deltaTime;
	//transform.position += moveVector;
	
	agent.SetDestination(currentWaypoint.transform.position);
	
	
	//smoothly rotate towards target
	//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 4 * Time.deltaTime);
}
 
 
 function CanSeePlayer() : boolean
{
	var hit : RaycastHit;
	rayDirection = playerObject.transform.position - transform.position; //aims ray int direction of the player
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

function OnDrawGizmosSelected ()
{
// Draws a line in front of the player and one behind this is used to visually illustrate the detection ranges in front and behind the enemy
Gizmos.color = Color.magenta; // the color used to detect the player in front
Gizmos.DrawRay (transform.position, transform.forward * rayRange);
Gizmos.color = Color.yellow; // the color used to detect the player from behind
Gizmos.DrawRay (transform.position, transform.forward * -minPlayerDetectDistance);
}