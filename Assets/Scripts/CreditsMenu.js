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
		//Credits
		//
		if (
			GUI.Button(
			// Center in X, 2/3 of the height in Y
			new Rect(0 + xMods[i],0,buttonWidth,buttonHeight),buttonTexture[0],menuStyle)
			)
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
            sfxObject.GetComponent(SFXManager).PlayConfirm();
			Application.LoadLevel(loadLevel[i]);
		}

	}
}
