using UnityEngine;
using System.Collections;

public class Valuable : MonoBehaviour {
    // Name of the valuable
    public string _valuableName = "";
    // Value of the valuable
    public float _value =0.00f;
    // The shader that the valuable has normally
    private Shader _originalShader;
    private bool _hover = false;
    private bool _grabbed = false;
    public GUIStyle tooltipStyle;
    
    void Awake(){
        tooltipStyle.font = (Font)Resources.Load("Font/cityburn");
        tooltipStyle.fontSize = 36;
        tooltipStyle.alignment = TextAnchor.MiddleCenter;
        tooltipStyle.normal.textColor = Color.yellow;
    }
    
    public void OnGUI(){
        if(_hover){
            GUI.Label (new Rect (Screen.width - Screen.width/2, Screen.height - Screen.height/2, 8, 40), _valuableName + ": $ " + string.Format("{0:#,###0}", _value), tooltipStyle); 
        }
    }
    
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
        Debug.Log("menu up is set to "+GameState._sInstance.menuUp);
        if (value && !_grabbed && !GameState._sInstance.menuUp) {
            //Shader glowOutline = Shader.Find ("ObjectGlow");
            Shader glowOutline = Shader.Find ("GlowOutline");
            gameObject.renderer.material.shader = glowOutline;
            gameObject.renderer.material.SetColor ("_RimColor", new Color (1f, 1f, 1f, 1f));
            //gameObject.renderer.material.SetColor ("_RimColor", Color.green);
            gameObject.renderer.material.SetFloat ("_RimPower", 1.0f);
            _hover = true;
        } else {
            gameObject.renderer.material.shader = _originalShader;
            _hover = false;
        }
    }
    public void setGrab(bool grabbed){
        _grabbed = grabbed;
    }
}
