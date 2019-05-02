using HoloToolkit.Unity.InputModule;

using UnityEngine;

public class LogOnClick : MonoBehaviour, IInputHandler {
    public void OnInputDown(InputEventData eventData) {
        ObjectClickedLoggingManager.instance.UpdateClickedDown(transform.gameObject);
    }

    public void OnInputUp(InputEventData eventData) {
        ObjectClickedLoggingManager.instance.UpdateClickedUp(transform.gameObject);
    }
}
