using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HoardLair : MonoBehaviour {
    // list of hoarded items
    private Dictionary<string,Valuable> _hoardedItems;
    // current value total
    private float _totalValue = 0.0f;
    private AudioClip clip;

	// Use this for initialization
	void Start () {
        _hoardedItems = new Dictionary<string,Valuable>();
        gameObject.AddComponent("AudioSource");
        clip = (AudioClip) Resources.Load("Audio/cashMoney", typeof(AudioClip));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Valuable>() != null){
            Valuable valuableComponent = other.GetComponent<Valuable>();
            if(!_hoardedItems.ContainsKey(valuableComponent.getName())){
                _hoardedItems[valuableComponent.getName()] = valuableComponent;
                _totalValue += valuableComponent.getValue();
                audio.PlayOneShot(clip, 0.7f);
                //Debug.Log("Current Hoard value = " + _totalValue);
            }
        }   
    }
    
    void OnTriggerExit(Collider other) {
        if(other.GetComponent<Valuable>() != null){
            Valuable valuableComponent = other.GetComponent<Valuable>();
            if(_hoardedItems.ContainsKey(valuableComponent.getName())){
                _hoardedItems.Remove(valuableComponent.getName());
                _totalValue -= valuableComponent.getValue();
                //Debug.Log("Current Hoard value = " + _totalValue);
            }
        } 
    }
    
    public float getTotal(){
        return _totalValue;
    }
    public Dictionary<string,Valuable> getValuables(){
        return _hoardedItems;
    }
    public int getItemCount(){
        return _hoardedItems.Count;
    }
}
