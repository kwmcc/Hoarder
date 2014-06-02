using UnityEngine;
using System.Collections;

public class captured : MonoBehaviour {

	
	void OnTriggerEnter (Collider col)
		{
		if(col.gameObject.tag == "Robot")
		{
			//Destroy(col.gameObject);
			Debug.Log("PLAYER CAPTURED!!");
		}
	}
}

