using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.InputModule.Utilities.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMovementOnKeyboard : MonoBehaviour {

    public KeyCode disableKey;

    void Update() {
        if (Input.GetKey(disableKey)) {
            if (GetComponent<TwoHandManipulatable>() != null)
                GetComponent<TwoHandManipulatable>().enabled = false;
            if (GetComponent<HandDraggable>() != null)
                GetComponent<HandDraggable>().enabled = false;
            enabled = false;
        }
    }
}
