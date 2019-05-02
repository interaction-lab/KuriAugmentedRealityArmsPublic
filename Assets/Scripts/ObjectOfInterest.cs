using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ObjectOfInterest : MonoBehaviour, IInputHandler {
    ObjectInteractionState objectInteractionState;
    void Start() {
        objectInteractionState = EventManager.instance.GetComponent<ObjectInteractionState>();
    }

    public void OnInputDown(InputEventData eventData) {
        objectInteractionState.UpdateObjectClickDown(transform.gameObject);
    }
    public void OnInputUp(InputEventData eventData) {
        objectInteractionState.UpdateObjectClickUp();
    }
}
