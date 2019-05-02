using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;


public class ObjectGazeLoggingManager : MonoBehaviour {

    static ObjectGazeLoggingManager objectGazeLoggingManager;
    GameObject currenGazed;
    static string clickedColumnName = "ObjectInteractionGaze";
    public Transform gazeTransform;

    private void Awake() {
        if (gazeTransform == null) {
            Debug.LogError("Need to add gazeTransform to ObjectGazeLoggingManager");
        }
        LoggingManager.instance.AddLogColumn(clickedColumnName, "");
        LoggingManager.instance.AddLogColumn("gaze_x", "");
        LoggingManager.instance.AddLogColumn("gaze_y", "");
        LoggingManager.instance.AddLogColumn("gaze_z", "");
        LoggingManager.instance.AddLogColumn("headpose_x", "");
        LoggingManager.instance.AddLogColumn("headpose_y", "");
        LoggingManager.instance.AddLogColumn("headpose_z", "");
    }

    public void DoNothing() { }

    void Init() {
        currenGazed = null;
        StartCoroutine(LogGazedObject());
        StartCoroutine(LogEyeGaze());
        StartCoroutine(LogHeadPose());
    }

    public static ObjectGazeLoggingManager instance {
        get {
            if (!objectGazeLoggingManager) {
                objectGazeLoggingManager = FindObjectOfType(typeof(ObjectGazeLoggingManager)) as ObjectGazeLoggingManager;

                if (!objectGazeLoggingManager) {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else {
                    objectGazeLoggingManager.Init();
                }
            }
            return objectGazeLoggingManager;
        }
    }

    IEnumerator LogGazedObject() {
        while (true) {
            if (currenGazed)
                LoggingManager.instance.UpdateLogColumn(clickedColumnName, currenGazed.GetFullName());
            else
                LoggingManager.instance.UpdateLogColumn(clickedColumnName, "");

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator LogEyeGaze() {

        while (true) {
            LoggingManager.instance.UpdateLogColumn("gaze_y", gazeTransform.position.y.ToString());
            LoggingManager.instance.UpdateLogColumn("gaze_x", gazeTransform.position.x.ToString());
            LoggingManager.instance.UpdateLogColumn("gaze_z", gazeTransform.position.z.ToString());
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator LogHeadPose() {
        Transform headCam = Camera.main.transform;
        while (true) {
            LoggingManager.instance.UpdateLogColumn("headpose_x", headCam.position.x.ToString());
            LoggingManager.instance.UpdateLogColumn("headpose_y", headCam.position.y.ToString());
            LoggingManager.instance.UpdateLogColumn("headpose_z", headCam.position.z.ToString());
            yield return new WaitForFixedUpdate();
        }
    }

    public void UpdateGazeEnter(GameObject gazedObject) {
        currenGazed = gazedObject;
    }

    public void UpdateGazeExit(GameObject gazedObject) {
        Assert.AreEqual(gazedObject, currenGazed);
        currenGazed = null;
    }
}
