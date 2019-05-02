using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FollowCurrentObjectOfInterest : MonoBehaviour {

    public float leftHandMult = 0.75f;
    Vector3 offset;
    UnityAction objectClickListener;


    void Awake() {
        offset = Vector3.zero;
    }


    // TODO refactor this trash
    IEnumerator LerpArmToObject(float moveTime, float pointTime) {
        float timeElapsed = 0;
        Vector3 origPos = transform.position;
        while (timeElapsed < moveTime) {
            offset.x = ObjectInteractionState.instance.GetCurrentClickedObjectScale().x * leftHandMult;
            transform.position = Vector3.Lerp(transform.position, (ObjectInteractionState.instance.GetCurrentClickedObjectPosition() + offset), timeElapsed / moveTime);
            yield return new WaitForSeconds(Time.deltaTime);
            timeElapsed += Time.deltaTime;
        }
        timeElapsed = 0;
        while (timeElapsed < pointTime) {
            offset.x = ObjectInteractionState.instance.GetCurrentClickedObjectScale().x * leftHandMult;
            transform.position = ObjectInteractionState.instance.GetCurrentClickedObjectPosition() + offset;
            yield return new WaitForSeconds(Time.deltaTime);
            timeElapsed += Time.deltaTime;
        }
        timeElapsed = 0f;
        while (timeElapsed < moveTime) {
            offset.x = ObjectInteractionState.instance.GetCurrentClickedObjectScale().x * leftHandMult;
            transform.position = Vector3.Lerp(transform.position, origPos, timeElapsed / moveTime);
            yield return new WaitForSeconds(Time.deltaTime);
            timeElapsed += Time.deltaTime;
        }
    }

    public void MoveArmToObjOfInterest(float totalTime) {
        StartCoroutine(LerpArmToObject((totalTime - 0.2f) / 2.0f, 0.2f));
    }

}
