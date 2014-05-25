using UnityEngine;
using System.Collections;


public class GUICrosshair : MonoBehaviour {


	public Texture2D crosshairTexture;
	public Texture2D crosshairTextureRed;
	public Texture2D crosshairTextureGreen;
	public float crosshairScale = 1;
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

	void onRed(){
		crosshairTexture = crosshairTextureRed;
	}

	void onGreen(){
		crosshairTexture = crosshairTextureGreen;
	}

	int getColor(){
		if(crosshairTexture == crosshairTextureRed)
			return 0;
		else{
			return 1;
		}
	}

}
