using UnityEngine;

public class StareAtCamera : MonoBehaviour {

    void Update() {
        transform.LookAt(Camera.main.transform);
    }
}
