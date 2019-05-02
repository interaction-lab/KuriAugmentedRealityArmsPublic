using UnityEngine;

public class ObjectInteractionState : MonoBehaviour {

    public static ObjectInteractionState instance;
    GameObject currentClickedObject;
    bool objectIsSelected;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        objectIsSelected = false;
        currentClickedObject = transform.gameObject;
    }

    public void UpdateObjectClickDown(GameObject clickedObject) {
        currentClickedObject = clickedObject;
        objectIsSelected = true;
        Debug.Log(currentClickedObject.name);
        EventManager.TriggerEvent(EventManager.EVENTS.ObjectClickedDown);
    }

    public void UpdateObjectClickUp() {
        objectIsSelected = false;
        EventManager.TriggerEvent(EventManager.EVENTS.ObjectClickedUp);
    }

    public void UpdateObjectOfInterest(GameObject goIn) {
        currentClickedObject = goIn;
    }

    public bool IsObjectSelected() {
        return objectIsSelected;
    }

    public GameObject GetCurrentClickedObject() {
        return currentClickedObject;
    }

    public Vector3 GetCurrentClickedObjectPosition() {
        return currentClickedObject.transform.position;
    }
    public Vector3 GetCurrentClickedObjectScale() {
        return currentClickedObject.transform.lossyScale;
    }

}
