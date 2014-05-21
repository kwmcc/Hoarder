using UnityEngine;
using System.Collections;

public class Valuable : MonoBehaviour {
    // Name of the valuable
    public string _valuableName = "";
    // Value of the valuable
    public float _value =0.00f;
    
    public float getValue(){
        return _value;
    }
    public string getName(){
        return _valuableName;
    }
}
