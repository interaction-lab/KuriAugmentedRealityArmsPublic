using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class PointToBlockColor : MonoBehaviour, IInputHandler {


    public ColorPaletteManager.BLOCKCOLORS desiredColor;

    public void OnInputDown(InputEventData eventData) {
        ColorPaletteManager.instance.PointToColor(desiredColor);
    }

    public void OnInputUp(InputEventData eventData) {
    }
}
