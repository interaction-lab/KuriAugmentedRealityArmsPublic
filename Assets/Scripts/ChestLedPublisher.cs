using System;
using UnityEngine;

namespace RosSharp.RosBridgeClient {
    public class ChestLedPublisher : Publisher<Messages.Mayfield.ChestLeds> {

        public static ChestLedPublisher instance;
        private Messages.Mayfield.ChestLeds message;


        protected override void Start() {
            base.Start();
            instance = this;
            InitializeMessage();
        }

        private void InitializeMessage() {
            message = new Messages.Mayfield.ChestLeds();
        }

        public void PublishChestLed(Color cIn) {
            uint r = Convert.ToUInt32(cIn.r * 255);
            uint g = Convert.ToUInt32(cIn.g * 255);
            uint b = Convert.ToUInt32(cIn.b * 255);
            message = new Messages.Mayfield.ChestLeds(r, g, b);
            Publish(message);
            Debug.Log("Publsihed: " + message.ToString());
        }

    }
}
