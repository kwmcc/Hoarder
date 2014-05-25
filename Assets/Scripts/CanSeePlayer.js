#pragma strict
//Andrew Miller (anmcmill@ucsc.edu)
//CMSP 179 S14
//Hoarderlands
//CanSeePlayer.js
//
//This script uses raycasting to detect the player, includes feild of view movdifiers
//As well as distance
//
//Addapted from code found here: http://answers.unity3d.com/questions/15735/field-of-view-using-raycasting.html

var playerObject : GameObject; //player
var fovRange : float; //Feild of View Ranger
var minPlayerDetectDistance : float; //how close can the player get
var rayRange : float; // distance in front.

private var rayDirection = Vector3.zero;

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
}