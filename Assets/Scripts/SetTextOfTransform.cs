using UnityEngine;
using UnityEngine.UI;

public class SetTextOfTransform : MonoBehaviour {
    Text t1;
    public Transform setTextToThisTransform;
    // Use this for initialization
    void Start() {
        t1 = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        t1.text = "Position: " + setTextToThisTransform.position.x + ", " + setTextToThisTransform.position.y + ", " + setTextToThisTransform.position.z;
        //"\nRotation: " + setTextToThisTransform.rotation;
    }
}
