using RosSharp.RosBridgeClient;
using System.Collections;
using UnityEngine;

public class SetDummyWorldPosition : MonoBehaviour {

    public static SetDummyWorldPosition instance;
    bool hasBeenSet = false;
    Vector3 oPos, oSca;
    Quaternion oRot;
    public Transform t1;

    public float a = 1; // typical change
    public float r = 0.25f; // average noise
    public float moveThreshhold = 0.1f;

    private void Awake() {
        instance = this;
        //t1 = GameObject.Find("ImageTarget").transform;
        oPos = t1.position;
        oRot = t1.rotation;
        oSca = t1.localScale;

    }
    public void SetWorldPosition(Transform tIn) {
        transform.position = tIn.position;
        transform.rotation = tIn.rotation;
    }

    IEnumerator Potato() {
        Vector3 xhat = t1.transform.position;
        Vector3 origXhat = xhat;
        float predictError = 1;
        float gain = 1; // only used for update
        Vector3 observation;
        while (gain > 0.00000001f) {
            yield return new WaitForSeconds(.05f);
            observation = t1.localPosition;// + Camera.main.transform.position;
            // predict
            xhat = a * xhat;
            predictError = a * predictError * a;

            //update
            gain = predictError / (predictError + r);
            xhat = xhat + gain * (observation - xhat);
            predictError = (1 - gain) * predictError;

            transform.position = xhat;
        }
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            transform.position = t1.position;
            CommandPublisher.instance.PublishAnimationCommand("smile");
            GameObject.FindGameObjectWithTag("SpatialMapping").SetActive(false);
            enabled = false;
        }
        /*if (!hasBeenSet && oPos != t1.position) {
            //transform.position = t1.position;
            //transform.rotation = t1.rotation * Quaternion.Inverse(oRot);
            //transform.localScale = t1.localScale;
            hasBeenSet = true;
            StartCoroutine(Potato());
        }*/
    }
}
