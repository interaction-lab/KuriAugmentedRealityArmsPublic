using HoloToolkit.Unity.InputModule;
using System.Collections;
using UnityEngine;

public class AnswerEnterButton : MonoBehaviour, IInputHandler {

    IEnumerator FlashColor(bool correct) {
        Color flashToColor = correct ? Color.green : Color.red;
        Renderer rend = GetComponent<MeshRenderer>();
        Color origColor = rend.material.color;
        rend.material.color = flashToColor;
        yield return new WaitForSeconds(0.5f);
        rend.material.color = origColor;
    }

    public void OnInputDown(InputEventData eventData) {
        EventManager.TriggerEvent(EventManager.EVENTS.ObjectClickedDown);
        bool correct = PuzzleManager.instance.MakeGuessOnCurPuzzle(InputTextManager.instance.GetCurrentGuess());
        StartCoroutine(FlashColor(correct));
    }

    public void OnInputUp(InputEventData eventData) {
        EventManager.TriggerEvent(EventManager.EVENTS.ObjectClickedUp);
    }

}
