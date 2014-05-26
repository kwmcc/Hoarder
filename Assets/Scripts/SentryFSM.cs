using UnityEngine;
using System.Collections;

public class SentryFSM : MonoBehaviour {
	Animator anim;
	
	void Start ()
	{
		anim = GetComponent<Animator>();
	}
		
	void Update ()
	{

		Debug.DrawLine(transform.position, Vector3.forward * Time.deltaTime, Color.red);
		//Debug.DrawLine (Transform.position, Vector3.forward , Color.cyan);
		if (Input.GetKey ("f")) {

			transform.Translate (Vector3.forward * Time.deltaTime);
			anim.SetBool("Walking", true);
		}else{
			anim.SetBool("Walking", false);
		}
	}
}
