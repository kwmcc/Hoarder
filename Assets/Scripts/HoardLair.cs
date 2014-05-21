using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HoardLair : MonoBehaviour {
    // list of hoarded items
    // public just for testing purposes
    public List<Valuable> _hoardedItems = null;
    // current value total
    private float _totalValue = 0.0f;

	// Use this for initialization
	void Start () {
        _hoardedItems = new List<Valuable>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Valuable>() != null){
            Valuable valuableComponent = other.GetComponent<Valuable>();
            _hoardedItems.Add(valuableComponent);
            _totalValue += valuableComponent.getValue();
            Debug.Log("Current Hoard value = " + _totalValue);
        }   
    }
    
    void OnTriggerExit(Collider other) {
        if(other.GetComponent<Valuable>() != null){
            Valuable valuableComponent = other.GetComponent<Valuable>();
            _hoardedItems.Add(valuableComponent);
            _totalValue -= valuableComponent.getValue();
            Debug.Log("Current Hoard value = " + _totalValue);
        } 
        Debug.Log("Current Hoard value = " + _totalValue);
    }
    
    public float getTotal(){
        return _totalValue;
    }
    public List<Valuable> getValuables(){
        return _hoardedItems;
    }
}
