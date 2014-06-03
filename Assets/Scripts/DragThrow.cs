// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class DragThrow : MonoBehaviour {
	public int normalCollisionCount = 1;
	public int range= 1;
	public float spring= 50.0f;
	public float damper= 5.0f;
	public float drag= 10.0f;
	public float angularDrag= 5.0f;
	public float distance= 0.2f;
	public int throwForce= 500;
	public int throwRange= 1000;
	public bool attachToCenterOfMass= false;
	
	private SpringJoint springJoint;
	private bool  holding;
    private Valuable _hoverObjectValuable;
	private GameObject player;
	
	void Start () {
		player = GameObject.Find("First Person Controller");
	}

	void  Update (){
		//toggle between holding and not
		
		// Make sure the user pressed the mouse down
		if (!Input.GetMouseButtonDown (0))
			return;
		
		Camera mainCamera = FindCamera();
		
		// We need to actually hit an object
		RaycastHit hit;
		Ray ray= mainCamera.ScreenPointToRay(new Vector3(mainCamera.pixelWidth/2, mainCamera.pixelHeight/2, 0));
		if (!Physics.Raycast(ray, out hit, range))
			return;
		// We need to hit a rigidbody that is not kinematic
		if (!hit.rigidbody || hit.rigidbody.isKinematic)
			return;

		if (!springJoint)
		{
			GameObject go = new GameObject("Rigidbody dragger");
			Rigidbody body = go.AddComponent ("Rigidbody") as Rigidbody;
			springJoint = (SpringJoint) go.AddComponent ("SpringJoint");
			body.isKinematic = true;
		}
		
        if(_hoverObjectValuable = hit.collider.gameObject.GetComponent<Valuable>()){
            _hoverObjectValuable.setGrab(true);
        }
        
		springJoint.transform.position = hit.point;
		if (attachToCenterOfMass)
		{
			Vector3 anchor= transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
			anchor = springJoint.transform.InverseTransformPoint(anchor);
			springJoint.anchor = anchor;
		}
		else
		{
			springJoint.anchor = Vector3.zero;
		}
		
		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.maxDistance = distance;
		springJoint.connectedBody = hit.rigidbody;
		
		StartCoroutine ("DragObject", hit.distance);
	}
	
	IEnumerator DragObject ( float distance  ){
		float oldDrag = springJoint.connectedBody.drag;
		float oldAngularDrag = springJoint.connectedBody.angularDrag;
		springJoint.connectedBody.drag = drag;
		springJoint.connectedBody.angularDrag = angularDrag;
		Camera mainCamera = FindCamera();
		while (Input.GetMouseButton (0))
		{
			Ray ray= mainCamera.ScreenPointToRay (new Vector3(mainCamera.pixelWidth/2, mainCamera.pixelHeight/2, 0));
			springJoint.transform.position = ray.GetPoint(distance);
			springJoint.transform.rotation = player.transform.rotation;
			yield return 0;
			
			if (Input.GetMouseButton (1)){
				springJoint.connectedBody.AddExplosionForce(throwForce,mainCamera.transform.position,throwRange);
				springJoint.connectedBody.drag = oldDrag;
				springJoint.connectedBody.angularDrag = oldAngularDrag;
				springJoint.connectedBody = null;
			}
		}
		if (springJoint.connectedBody)
		{
			springJoint.connectedBody.drag = oldDrag;
			springJoint.connectedBody.angularDrag = oldAngularDrag;
			springJoint.connectedBody = null;
            _hoverObjectValuable.setGrab(false);
            _hoverObjectValuable = null;
		}
	}
	
	Camera FindCamera (){
		if (camera)
			return camera;
		else
			return Camera.main;
	}
}