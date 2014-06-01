using UnityEngine;
using System.Collections;

public class Valuable : MonoBehaviour {
    // Name of the valuable
    public string _valuableName = "";
    // Value of the valuable
    public float _value =0.00f;
    // The shader that the valuable has normally
    private Shader _originalShader;
    private bool _grabbed = false;
    
    public float getValue(){
        return _value;
    }
    public string getName(){
        return _valuableName;
    }
    public void highlightItem(bool value) {
        if (!gameObject.renderer) {
            return;
        }
        if (_originalShader == null) {
            _originalShader = gameObject.renderer.material.shader;
        }
        if (value && !_grabbed) {
            Shader glowOutline = Shader.Find ("ObjectGlow");
            gameObject.renderer.material.shader = glowOutline;
            //gameObject.renderer.material.SetColor ("_RimColor", new Color (0f, 1f, 1f, 1f));
            gameObject.renderer.material.SetColor ("_RimColor", Color.green);
            gameObject.renderer.material.SetFloat ("_RimPower", 3.0f);
        } else {
            gameObject.renderer.material.shader = _originalShader;
        }
    }
    public void setGrabbed(bool grabbed){
        _grabbed = grabbed;
    }
}
