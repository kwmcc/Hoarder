using UnityEngine;
using System.Collections;

/*
 * Optimal functionality for this script:
 * 1. Place on a GUI element
 * 2. Link the OnMouseDown function to a start/resume button so that mouse is locked
 * 		when that button is pressed.
 * 3. Escape will unlock the cursor and should always bring up at least a resume button
 * 4. Remove the Start function so that everything else works normally.
 */

public class CursorLock : MonoBehaviour {
	void DidLockCursor() {
	//	Debug.Log("Locking cursor");
		//guiTexture.enabled = false;
	}
	void DidUnlockCursor() {
	//	Debug.Log("Unlocking cursor");
		//guiTexture.enabled = true;
	}
	void OnMouseDown() {
		Screen.lockCursor = true;
	}
	private bool wasLocked = false;

	void Start() {
		Screen.lockCursor = true;
	}

	void Update() {
		if (Input.GetKeyDown("escape"))
			Screen.lockCursor = false;
		
		if (!Screen.lockCursor && wasLocked) {
			wasLocked = false;
			DidUnlockCursor();
		} else
		if (Screen.lockCursor && !wasLocked) {
			wasLocked = true;
			DidLockCursor();
		}
	}
}