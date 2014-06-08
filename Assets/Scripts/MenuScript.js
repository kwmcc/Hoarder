#pragma strict
//
//This is a repurposed Main Menu Script from the game Ares, which I worked on previously for this CS 179 Class
//Andrew Miller
//Isabella Lee
//Andrew Neff
//CMPS 179 S14
//Hoarderlands
//

public var xMods:int[];
public var buttonText:String[];
public var loadLevel:String[];
public var buttonTexture:Texture[];
public var menuStyle:GUIStyle;

var sfxObject:GameObject;

function Awake (){
    if(GameObject.Find("SFXManager") == null){
        sfxObject = new GameObject();
        sfxObject.name = "SFXManager";
        sfxObject.AddComponent(SFXManager);
    }else{
        sfxObject = GameObject.Find("SFXManager");
    }
        
}

function OnGUI()
{
	var buttonWidth = 256;
	var buttonHeight = 128;
	
	for (var i = 0; i < loadLevel.Length; i++){


		//
		//Play
		//
		if (
			GUI.Button(
			// Center in X, 2/3 of the height in Y
			new Rect(Screen.width / 2 - (buttonWidth / 2) + xMods[i],(4.25 * Screen.height / 8) - (buttonHeight / 2),buttonWidth,buttonHeight),buttonTexture[0],menuStyle)
			)
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Screen.lockCursor = true;
            sfxObject.GetComponent(SFXManager).PlayConfirm();
			Application.LoadLevel(loadLevel[i]);
		}
		//
		//How to Play
		//
		if (
			GUI.Button(
			// Center in X, 2/3 of the height in Y
			new Rect(Screen.width / 2 - (buttonWidth / 2) + xMods[i],(5.75 * Screen.height / 8) - (buttonHeight / 2),buttonWidth,buttonHeight),buttonTexture[1], menuStyle)
			)
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
            sfxObject.GetComponent(SFXManager).PlayConfirm();
			Application.LoadLevel(loadLevel[i+1]);
		}
		
		//
		//Credits
		//
		if (
			GUI.Button(
			// Center in X, 2/3 of the height in Y
			new Rect(Screen.width / 2 - (buttonWidth / 2) + xMods[i],(7.25 * Screen.height / 8) - (buttonHeight / 2),buttonWidth,buttonHeight),buttonTexture[2],menuStyle)
			)
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
            sfxObject.GetComponent(SFXManager).PlayConfirm();
			Application.LoadLevel(loadLevel[i+2]);
		}
	}
}
