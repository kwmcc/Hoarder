using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {
    [Range (1, 10)]
    public float timeLimit = 5;
    [Range (1, 5)]
    public float endingThreshold = 1.0f;
    public bool devMode = false;
    public GUIStyle timeStyle;
    public GUIStyle menuStyle;
    public GUIStyle scoreStyle;
    public GUIStyle scoreTallyStyle;
    public GUIStyle itemStyle;
    public float buttonHeight = 60;
    public float buttonWidth = 84;
    
    public bool menuUp = false;
    
    
    private float gameTime;
    private string minutes;
    private string seconds;
    private string scoreText;
    
    private bool colorSet = false;
    private bool timePaused = false;
    private bool pauseGame = false;
    private bool gameOver = false;
    

    private static CharacterController _characterControllor;
    private static HoardLair _hoardLair;
    private Dictionary<string,Valuable> _hoardedItems;
    
    public static GameState _sInstance;
    public static GameState sInstance {
        get {
            if (_sInstance == null) {
                _sInstance = (GameState)FindObjectOfType(typeof(GameState));
                _characterControllor = (CharacterController)FindObjectOfType(typeof(CharacterController));
                _hoardLair = (HoardLair) FindObjectOfType(typeof(HoardLair));
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
 	
        int minutes = Mathf.FloorToInt(gameTime/60);
        string minutesString = minutes.ToString("00");
        seconds = Mathf.FloorToInt(gameTime - minutes*60).ToString("00");
        GUI.Label (new Rect (Screen.width, 10, 200, 40), "Time: " + minutesString + ":"+ seconds, timeStyle);
        scoreText = _hoardLair.getTotal().ToString();
        GUI.Label (new Rect (10, 10, 200, 40), "Hoard Value: $ " + string.Format("{0:#,###0}", _hoardLair.getTotal()), scoreStyle);    
        if (pauseGame){
            pauseMenu();
        }
        if (gameOver){
            endMenu();
        }
    }
    
    void Update(){
        if(gameTime > 0 || devMode){
            gameTime -= Time.deltaTime;
            
            if (pauseGame)
                pauseMenu();
                
            // Input to open pause menu
            if (Input.GetKeyDown(KeyCode.Escape) && Application.loadedLevelName != "mainMenu") {
                togglePause();
            }
        }else{
            gameTime = 0.0f;
            if(!gameOver){
                gameOver = true;
                Screen.lockCursor = false;
                _characterControllor.enabled = !_characterControllor.enabled;
                Time.timeScale = (Time.timeScale + 1) % 2;
                Debug.Log("game over called");
                _hoardedItems = _hoardLair.getValuables();
                endMenu();
            }
        }
    }
    
    public void pauseMenu(){
 
        if (GUI.Button(
            // Center in X, 2/3 of the height in Y
            new Rect(Screen.width / 2 - (buttonWidth / 2) + 200,(2 * Screen.height / 3) - (buttonHeight / 2),buttonWidth,buttonHeight),
            "Resume", menuStyle))
        {
            // On Click, resume the game
            togglePause();
        } else if ( GUI.Button(
            // Center in X, 2/3 of the height in Y
            new Rect(Screen.width / 2 - (buttonWidth / 2) + - 200,(2 * Screen.height / 3) - (buttonHeight / 2),buttonWidth,buttonHeight),
            "Main Menu", menuStyle))
        {
            // On Click, load the main menu.
            timePaused = false;
            pauseGame = false;
            //Application.LoadLevel("mainMenu");
            Time.timeScale = 1;
        }
 	
    }
    public void endMenu(){
        
        GUI.Label(
            // Center in X, 2/3 of the height in Y
            new Rect(Screen.width / 2 - (buttonWidth / 2),(Screen.height - 7 * (Screen.height / 8) ) - (buttonHeight / 2),buttonWidth,buttonHeight),
            "Time's Up!", menuStyle);
        GUI.Label(
            // Center in X, 2/3 of the height in Y
            new Rect(Screen.width - Screen.width / 2,(Screen.height - 4 * (Screen.height / 8) ) - (buttonHeight / 2),buttonWidth,buttonHeight),
            "Hoard Value: $ " + string.Format("{0:#,###0}", _hoardLair.getTotal()), scoreTallyStyle);
            
            
        //for (int index = 0; index < _hoardedItems.Count; index++) {
        int index = 1;
        foreach ( string key in _hoardedItems.Keys) {
          //var item = dictionary.ElementAt(index);
          //var itemKey = item.Key;
          Valuable item = _hoardedItems[key];
          GUI.Label(
            // Center in X, 2/3 of the height in Y
            new Rect(Screen.width - Screen.width / 2,(Screen.height - 4 * (Screen.height / 8) + 50 * index) - (buttonHeight / 2),buttonWidth,buttonHeight),
            item.getName() +" - $ " + string.Format("{0:#,###0}",  item.getValue()), itemStyle);
          Debug.Log("item");
          index++;
        }
        
        if (GUI.Button(
            // Center in X, 2/3 of the height in Y
            new Rect(Screen.width / 2 - (buttonWidth / 2) + 200, Screen.height - 6 * (Screen.height / 8) - (buttonHeight / 2),buttonWidth,buttonHeight),
            "Try Again", menuStyle))
        {
            pauseGame = false;
            gameOver = false;
            gameOver = true;
            _characterControllor.enabled = !_characterControllor.enabled;
            timePaused = !timePaused;
            Time.timeScale = (Time.timeScale + 1) % 2;
            // On Click, restart the game
            Application.LoadLevel(Application.loadedLevel);
        } else if ( GUI.Button(
            // Center in X, 2/3 of the height in Y
            new Rect(Screen.width / 2 - (buttonWidth / 2) + - 200,Screen.height - 6 * (Screen.height / 8) - (buttonHeight / 2),buttonWidth,buttonHeight),
            "Main Menu", menuStyle))
        {
            // On Click, load the main menu.
            timePaused = false;
            pauseGame = false;
            Application.LoadLevel("mainMenu");
            Time.timeScale = 1;
        }
 	
    }    
    public void togglePause(){
        _characterControllor.enabled = !_characterControllor.enabled;
        pauseGame = !pauseGame;
        timePaused = !timePaused;
        Time.timeScale = (Time.timeScale + 1) % 2;
        
    }
}
