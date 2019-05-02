using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class LogOnGaze : MonoBehaviour, IFocusable {
    public void OnFocusEnter() {
        ObjectGazeLoggingManager.instance.UpdateGazeEnter(gameObject);
    }

    public void OnFocusExit() {
        ObjectGazeLoggingManager.instance.UpdateGazeExit(gameObject);
    }

}
