using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    private Dictionary<EVENTS, UnityEvent> eventDictionary;

    private static EventManager eventManager;

    public enum EVENTS : int {
        SpatialMappingReady,
        ObjectClickedDown,
        ObjectClickedUp,
        RosConnectionReady,
        GameFinished
    }

    public static EventManager instance {
        get {
            if (!eventManager) {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager) {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init() {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<EVENTS, UnityEvent>();
        }
    }

    public static void StartListening(EVENTS eventName, UnityAction listener) {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.AddListener(listener);
        }
        else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(EVENTS eventName, UnityAction listener) {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(EVENTS eventName) {
        UnityEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke();
        }
    }
}