using UnityEngine;
using UnityEngine.Events;

public class ObjectClickedManager : MonoBehaviour {

    UnityAction clickDownListerner;
    public static ObjectClickedManager instance;
    float lastClickTime;
    void Awake() {
        clickDownListerner = new UnityAction(ListenToClicks);
        lastClickTime = Time.time;
        if (instance == null)
            instance = this;
    }

    void ListenToClicks() {
        lastClickTime = Time.time;
    }


    public float GetTimeSinceLastClick() {
        return Time.time - lastClickTime;
    }

    void OnEnable() {
        EventManager.StartListening(EventManager.EVENTS.ObjectClickedDown, clickDownListerner);
    }

    void OnDisable() {
        EventManager.StopListening(EventManager.EVENTS.ObjectClickedDown, clickDownListerner);
    }

}
