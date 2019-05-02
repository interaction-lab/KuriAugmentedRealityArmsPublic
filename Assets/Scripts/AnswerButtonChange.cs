using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class AnswerButtonChange : MonoBehaviour, IInputHandler {
    public bool upButton;


    public void OnInputDown(InputEventData eventData) {
        EventManager.TriggerEvent(EventManager.EVENTS.ObjectClickedDown);

        if (upButton)
            InputTextManager.instance.IncrementGuess();
        else
            InputTextManager.instance.DecrementGuess();
    }

    public void OnInputUp(InputEventData eventData) {
        EventManager.TriggerEvent(EventManager.EVENTS.ObjectClickedUp);
    }




}
