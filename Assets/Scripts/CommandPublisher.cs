using UnityEngine;
namespace RosSharp.RosBridgeClient {
    public class CommandPublisher : Publisher<Messages.Mayfield.Command> {

        public static CommandPublisher instance;
        private Messages.Mayfield.Command message;


        protected override void Start() {
            base.Start();
            instance = this;
            InitializeMessage();
        }

        private void InitializeMessage() {
            message = new Messages.Mayfield.Command();
        }

        public void PublishCommand(string name, Messages.Mayfield.KeyValue keyValue) {
            message = new Messages.Mayfield.Command(name, keyValue);
            Publish(message);

            Debug.Log("Publsihed: " + message.ToString());

        }

        public void PublishOverrideCommand() {
            message = new Messages.Mayfield.Command("start_animation", new Messages.Mayfield.KeyValue("mode", "on"));
            Publish(message);
            Publish(message); //needs second publish for some reason
            Debug.Log("Publsihed: " + message.ToString());

        }

        public void PublishAnimationCommand(string animName) {
            PublishCommand("start_animation", new Messages.Mayfield.KeyValue("mode", "on"));
            PublishCommand("play_stored_animation_command", new Messages.Mayfield.KeyValue("name", animName));
        }

    }
}
