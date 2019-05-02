using HoloToolkit.Unity.InputModule;
using RosSharp.RosBridgeClient;
using UnityEngine;

public class ChangeKuriColorOnGaze : MonoBehaviour, IFocusable {
    public void OnFocusEnter() {

        ChestLedPublisher.instance.PublishChestLed(GetComponent<Renderer>().material.color);
    }

    public void OnFocusExit() {
    }
}
