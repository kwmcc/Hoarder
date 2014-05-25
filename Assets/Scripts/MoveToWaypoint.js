#pragma strict
//Andrew Miller (anmcmill@ucsc.edu)
//CMPS 179 S14
//Hoarderlands
//Waypoint script. 
//using tutorial found here: http://www.attiliocarotenuto.com/83-articles-tutorials/unity/292-unity-3-moving-a-npc-along-a-path

//Waypoint array that can be used in the inspector
var waypoints : Transform[];
private var currentWaypoint : Transform;
private var currentIndex : int;

private var agent: NavMeshAgent;
//NPC Movement
//var moveSpeed : float = 10.0;
var minDistance : float = 2.0;

//initialization
function Start () {
	agent = GetComponent.<NavMeshAgent>();
	currentWaypoint = waypoints[0];
	currentIndex = 0;
}

function Update () {
	MoveTowardsWaypoint();
	if(Vector3.Distance(currentWaypoint.transform.position, transform.position) < minDistance)
	{
		currentIndex++;
		//resests after cycling though all waypoints.
		if(currentIndex > waypoints.Length - 1)
		{
			currentIndex = 0;
		}
		currentWaypoint = waypoints[currentIndex];
	}
}

function MoveTowardsWaypoint() : void
{
	//var direction : Vector3 = currentWaypoint.transform.position - transform.position;
	//var moveVector : Vector3 = direction.normalized * moveSpeed * Time.deltaTime;
	//transform.position += moveVector;
	agent.SetDestination(currentWaypoint.transform.position);
	
	//smoothly rotate towards target
	//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 4 * Time.deltaTime);
}