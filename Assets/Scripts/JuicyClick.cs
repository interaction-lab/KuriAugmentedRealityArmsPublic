
using HoloToolkit.Unity.InputModule;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class JuicyClick : MonoBehaviour, IInputHandler {

    public enum ROOMSIZE { Small, Medium, Large, None };
    public ROOMSIZE room = ROOMSIZE.Small;
    public float colorMultiplier = 1.5f;
    public bool disableFlashColor = false;

    AudioSource audioSource;
    Renderer rend;
    Color originalColor;

    void Start() {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        audioSource = GetComponent<AudioSource>();
        //SetSpatialAudio();
    }

    void SetSpatialAudio() {
        audioSource.spatialize = true; // we DO want spatialized audio
        audioSource.spread = 0; // we dont want to reduce our angle of hearing
        audioSource.spatialBlend = 1;   // we do want to hear spatialized audio
        audioSource.SetSpatializerFloat(1, (float)room);
        audioSource.SetCustomCurve(AudioSourceCurveType.CustomRolloff, AnimationCurve.EaseInOut(0.0f, 1.0f, audioSource.clip.length, 0.0f));
    }

    public void OnInputDown(InputEventData eventData) {
        audioSource.Play();
        if (!disableFlashColor)
            rend.material.color = originalColor * colorMultiplier;
    }
    public void OnInputUp(InputEventData eventData) {
        if (!disableFlashColor)
            rend.material.color = originalColor;
    }


}
