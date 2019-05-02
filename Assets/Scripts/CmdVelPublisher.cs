using System;
using UnityEngine;

namespace RosSharp.RosBridgeClient {
    public class CmdVelPublisher : Publisher<Messages.Geometry.Twist> {

        public static CmdVelPublisher instance;
        private Messages.Geometry.Twist message;
        private float previousRealTime;

        protected override void Start() {
            base.Start();
            instance = this;
            InitializeMessage();
        }

        private void InitializeMessage() {
            message = new Messages.Geometry.Twist();
            message.linear = new Messages.Geometry.Vector3();
            message.angular = new Messages.Geometry.Vector3();
        }

        public void PublishCmdVel(Vector3 lIn, Vector3 aIn) {
            message.linear = GetGeometryVector3(lIn.Unity2Ros()); ;
            message.angular = GetGeometryVector3(-aIn.Unity2Ros());

            Publish(message);
        }
        public void PublishCmdVel(Tuple<Vector3, Vector3> tIn) {
            message.linear = GetGeometryVector3(tIn.Item1.Unity2Ros()); ;
            message.angular = GetGeometryVector3(-tIn.Item2.Unity2Ros());

            Publish(message);
        }

        private static Messages.Geometry.Vector3 GetGeometryVector3(Vector3 vector3) {
            Messages.Geometry.Vector3 geometryVector3 = new Messages.Geometry.Vector3();
            geometryVector3.x = vector3.x;
            geometryVector3.y = vector3.y;
            geometryVector3.z = vector3.z;
            return geometryVector3;
        }
    }
}

