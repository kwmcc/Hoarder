#pragma strict

public var confirmPing : AudioClip;
public var alertPing : AudioClip;
public var sfxSource : AudioSource;
public var listener:AudioListener;
public var listenerObject:GameObject;
public var alertCount = 0;

function Awake () {
    DontDestroyOnLoad(gameObject);
    sfxSource = gameObject.AddComponent(AudioSource);
    sfxSource.volume = 0.7f;
    sfxSource.priority = 255;
    //sfxSource.loop = true;
    confirmPing = Resources.Load("Audio/Confirm") as AudioClip;
    alertPing = Resources.Load("Audio/EnemyActivated") as AudioClip;
    listener = FindObjectOfType(AudioListener);
    listenerObject = listener.gameObject;
    gameObject.transform.position = listenerObject.transform.position; 
}

function Start () {
    sfxSource.clip = confirmPing;
    //sfxSource.Play();

}

function PlayConfirm(){
    if(!sfxSource.isPlaying){
        sfxSource.clip = confirmPing;
        sfxSource.Play();
    }
}

function PlayAlert(){
    
    if(!sfxSource.isPlaying){
        sfxSource.clip = alertPing;
        sfxSource.Play();
        Debug.Log("alert sound played");
    }
}

function RequestConfirm(){
    PlayConfirm();
}

function OnLevelWasLoaded (level : int) {
    listener = FindObjectOfType(AudioListener);
    listenerObject = listener.gameObject;
    gameObject.transform.position = listenerObject.transform.position;
}