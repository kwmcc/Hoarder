using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {
    [Range (1, 10)]
    public float timeLimit = 5;
    [Range (1, 5)]
    public float endingThreshold = 1;
    private float gameTime;
    string minutes;
    string seconds;
    bool colorSet = false;
    public bool timePaused = false;
    public bool displayTime = true;
    public bool pauseGame = false;
    public GUIStyle timeStyle;
    
    private static GameState _sInstance;
    public static GameState sInstance {
        get {
            if (_sInstance == null) {
                _sInstance = (GameState)FindObjectOfType(typeof(GameState));
                //timeStyle = new GUIStyle("HoarderlandsFont");
                //timeStyle.font = Resources.Load("FontNameOfSomeKind", typeof(Font)) as Font;
            }
            return _sInstance;
        }
    }

    public void Awake() {
        if (GameState.sInstance.GetInstanceID() != this.GetInstanceID()) {
            Debug.LogWarning("There were multiple instances of " + GetType() + ", please fix that.");
            Destroy(this);
        }
        gameTime = timeLimit * 60.0f;
    }
    
    public void OnGUI(){
        if(gameTime < endingThreshold*60 && !colorSet){
            timeStyle.normal.textColor = Color.red;
            colorSet = true;
        }
 	
        //if (displayTime)
        int minutes = Mathf.FloorToInt(gameTime/60);
        string minutesString = minutes.ToString("00");
        seconds = Mathf.FloorToInt(gameTime - minutes*60).ToString("00");;
            GUI.Label (new Rect (10, 10, 200, 40), "Time: " + minutesString + ":"+ seconds, timeStyle);
            
        if (pauseGame){
            //pauseMenu();
        }
    }
    
    void Update(){
 
        //if (!timePaused){
            gameTime -= Time.deltaTime;
        //}
            
        //if (pauseGame)
            //pauseMenu();
            
        // Input to open pause menu
        if (Input.GetKeyDown(KeyCode.Escape) && Application.loadedLevelName != "mainMenu") {
            pauseGame = !pauseGame;
            timePaused = !timePaused;
            Time.timeScale = (Time.timeScale + 1) % 2;
            Debug.Log("escape called");
        }
    }
}
