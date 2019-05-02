using RosSharp.RosBridgeClient;
using UnityEngine;

public class RosmsgKeyboardTesting : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            CommandPublisher.instance.PublishCommand("start_animation",
                new RosSharp.RosBridgeClient.Messages.Mayfield.KeyValue());
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            CommandPublisher.instance.PublishCommand("play_stored_animation_command",
                new RosSharp.RosBridgeClient.Messages.Mayfield.KeyValue("name", "gotit"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            CommandPublisher.instance.PublishCommand("override_mode",
                new RosSharp.RosBridgeClient.Messages.Mayfield.KeyValue("mode", "on"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ChestLedPublisher.instance.PublishChestLed(Color.red);
        }

    }
}
