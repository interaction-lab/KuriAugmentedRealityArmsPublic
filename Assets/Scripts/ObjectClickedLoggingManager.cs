using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ObjectClickedLoggingManager : MonoBehaviour {

    static ObjectClickedLoggingManager objectClickedObjectClickedLoggingManager;
    GameObject currentSelected;
    static string clickedColumnName = "ObjectInteractionClick";

    private void Awake() {
        LoggingManager.instance.AddLogColumn(clickedColumnName, "");
    }

    void Init() {
        currentSelected = null;
        StartCoroutine(LogSelected());
    }

    public static ObjectClickedLoggingManager instance {
        get {
            if (!objectClickedObjectClickedLoggingManager) {
                objectClickedObjectClickedLoggingManager = FindObjectOfType(typeof(ObjectClickedLoggingManager)) as ObjectClickedLoggingManager;

                if (!objectClickedObjectClickedLoggingManager) {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else {
                    objectClickedObjectClickedLoggingManager.Init();
                }
            }
            return objectClickedObjectClickedLoggingManager;
        }
    }

    IEnumerator LogSelected() {
        while (true) {
            if (currentSelected)
                LoggingManager.instance.UpdateLogColumn(clickedColumnName, currentSelected.name);
            else
                LoggingManager.instance.UpdateLogColumn(clickedColumnName, "");

            yield return new WaitForFixedUpdate();
        }
    }

    public void UpdateClickedDown(GameObject clickedObject) {
        currentSelected = clickedObject;
    }

    public void UpdateClickedUp(GameObject clickedObject) {
        Assert.AreEqual(clickedObject, currentSelected);
        currentSelected = null;
    }
}
