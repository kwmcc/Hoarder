using UnityEngine;
using System.Collections;

public enum PhysicsMaterial{
    CARDBOARD,
    GLASS,
    METAL,
    CONCRETE,
    PLASTIC
}

public class PhysicsSound : MonoBehaviour {
    public PhysicsMaterial material = PhysicsMaterial.CARDBOARD;
    [Range(0, 10)]
    public float maxVolume = 10.0f;
    private float volumePercent;
    //private bool settled = true;
    private AudioClip clip;
    
	// Initialize audiosource and audioclip
	void Start () {
        gameObject.AddComponent("AudioSource");
        switch(material){
            case PhysicsMaterial.CARDBOARD:
                clip = (AudioClip) Resources.Load("Audio/Physics/cardboard_box_impact_hard2", typeof(AudioClip));
                break;
            case PhysicsMaterial.GLASS:
                clip = (AudioClip) Resources.Load("Audio/Physics/glass_bottle_impact_hard1", typeof(AudioClip));
                break;
            case PhysicsMaterial.METAL:
                clip = (AudioClip) Resources.Load("Audio/Physics/soda_can_impact_soft3", typeof(AudioClip));
                break;
            case PhysicsMaterial.CONCRETE:
                clip = (AudioClip) Resources.Load("Audio/Physics/concrete_block_impact_hard1", typeof(AudioClip));
                break;
            case PhysicsMaterial.PLASTIC:
                clip = (AudioClip) Resources.Load("Audio/Physics/plastic_barrel_impact_hard1", typeof(AudioClip));
                break;
            default:
                clip = (AudioClip) Resources.Load("Audio/Physics/cardboard_box_impact_hard2", typeof(AudioClip));
                break;
        }
	}

    void OnCollisionEnter(Collision collision) {
        foreach (ContactPoint contact in collision.contacts) {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
        //Debug.Log("velocity mag = "+collision.relativeVelocity.magnitude);
        // I'll need these debugs as I make the sounds more dynamic later
        
        if (collision.relativeVelocity.magnitude > maxVolume){
            volumePercent = 1.0f;
            //Debug.Log("volume % = "+ volumePercent);
        }else{
            volumePercent = maxVolume * ( collision.relativeVelocity.magnitude / maxVolume );
            //Debug.Log("volume % = "+ volumePercent);
        }
        audio.PlayOneShot(clip, volumePercent);    
    }
}
