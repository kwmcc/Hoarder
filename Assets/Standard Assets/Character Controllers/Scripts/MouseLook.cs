using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;
	
	public Texture2D crosshairTexture;
	public Texture2D crosshairTextureRed;
	public Texture2D crosshairTextureGreen;
	public float crosshairScale = 1;
    
    private bool mouseLocked = false;
    private Valuable _hoverObjectValuable;
    private GameObject _lastHoverObject;

	void OnGUI()
	{
		//if not paused
		if(Time.timeScale != 0)
		{
			if(crosshairTexture!=null) 
				GUI.DrawTexture(new Rect((Screen.width - crosshairTexture.width) / 2,
				                         (Screen.height - crosshairTexture.height) /2, crosshairTexture.width*crosshairScale, crosshairTexture.height*crosshairScale),crosshairTexture);
			else
				Debug.Log("No crosshair texture set in the Inspector");
		}
	}

	void Update ()
	{
        if(Time.timeScale != 0){
            // Testing to see if objects are within range of throwing
            RaycastHit hit;
            Vector3 target = new Vector3 (Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
            Ray ray = Camera.main.ScreenPointToRay(target);
            Physics.Raycast (ray, out hit, 3);
            // We need to hit a rigidbody that is not kinematic
            if (!hit.rigidbody || hit.rigidbody.isKinematic){
                crosshairTexture = crosshairTextureRed;
                if(_hoverObjectValuable != null){
                    _hoverObjectValuable.highlightItem(false);
                    _hoverObjectValuable = null;
                }
            }else{
                crosshairTexture = crosshairTextureGreen;
                GameObject _hoverObject = hit.collider.gameObject;
                if(_hoverObjectValuable != null && _hoverObject != _lastHoverObject ){
                    _hoverObjectValuable.highlightItem(false);
                    _hoverObjectValuable = null;
                }
                if(_hoverObjectValuable = hit.collider.gameObject.GetComponent<Valuable>()){
                    _lastHoverObject = hit.collider.gameObject;
                    _hoverObjectValuable.highlightItem(true);
                }
            }



            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
                
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
                
                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
                
                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (rigidbody)
			rigidbody.freezeRotation = true;
	}
}