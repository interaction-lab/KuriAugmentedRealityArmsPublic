using HoloToolkit.Unity.InputModule;
using RosSharp.RosBridgeClient;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveKuriDirectionalBox : MonoBehaviour, IInputHandler {

    public enum DIRECTIONS {
        forward,
        down,
        right,
        left
    }

    public DIRECTIONS direction;

    Dictionary<DIRECTIONS, Tuple<Vector3, Vector3>> directionLookup;

    public

    void Awake() {
        directionLookup = new Dictionary<DIRECTIONS, Tuple<Vector3, Vector3>>() {
           { DIRECTIONS.forward, new Tuple<Vector3, Vector3>(new Vector3(0,0,1), Vector3.zero) },
           { DIRECTIONS.down, new Tuple<Vector3, Vector3>(new Vector3(0,0,-1), Vector3.zero) },
           { DIRECTIONS.right, new Tuple<Vector3, Vector3>(Vector3.zero, new Vector3(0,(float)Math.PI/2.0f,0)) },
           { DIRECTIONS.left, new Tuple<Vector3, Vector3>(Vector3.zero, new Vector3(0,-(float)Math.PI/2.0f,0)) }
        };

    }

    public void OnInputDown(InputEventData eventData) {
        CmdVelPublisher.instance.PublishCmdVel(directionLookup[direction]);
    }

    public void OnInputUp(InputEventData eventData) {
    }

}
