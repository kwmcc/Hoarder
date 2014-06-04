using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {
    public AudioClip confirmPing;
    public AudioClip alertPing;
    public AudioSource sfxSource;
    public AudioListener listener;
    public GameObject listenerObject;
    public int alertCount = 0;
    
	void Start () {
        sfxSource.clip = confirmPing;
	}
	
	void Update () {
	
	}
    
    void Awake () {
        DontDestroyOnLoad(gameObject);
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.volume = 0.7f;
        sfxSource.priority = 255;
        confirmPing = (AudioClip) Resources.Load("Audio/Confirm", typeof(AudioClip));
        alertPing = (AudioClip) Resources.Load("Audio/EnemyActivated", typeof(AudioClip));
        listener = FindObjectOfType<AudioListener>();
        listenerObject = listener.gameObject;
        gameObject.transform.position = listenerObject.transform.position; 
    }

    public void PlayConfirm(){
        if(!sfxSource.isPlaying){
            sfxSource.clip = confirmPing;
            sfxSource.Play();
        }
    }

    public void PlayAlert(){
        if(!sfxSource.isPlaying){
            sfxSource.clip = alertPing;
            sfxSource.Play();
            Debug.Log("alert sound played");
        }
    }

    public void OnLevelWasLoaded (int level) {
        listener = FindObjectOfType<AudioListener>();
        listenerObject = listener.gameObject;
        gameObject.transform.position = listenerObject.transform.position;
    }
}
