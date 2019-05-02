using RosSharp.RosBridgeClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPaletteManager : MonoBehaviour {

    public enum BLOCKCOLORS : int {
        red,
        green,
        blue,
        yellow
    }

    public static ColorPaletteManager instance;

    bool pointingToColor;

    private void Awake() {
        if (instance == null)
            instance = this;
        pointingToColor = false;
    }


    public Color BlockColorToColor(BLOCKCOLORS color) {
        switch (color) {
            case BLOCKCOLORS.red:
                return Color.red;
            case BLOCKCOLORS.green:
                return Color.green;
            case BLOCKCOLORS.blue:
                return Color.blue;
            case BLOCKCOLORS.yellow:
                return Color.yellow;
        }
        return Color.black;
    }

    IEnumerator PointForTime(BLOCKCOLORS color, float time) {
        pointingToColor = true;
        ObjectInteractionState.instance.UpdateObjectOfInterest(transform.GetChild((int)color).gameObject);
        KuriBehaviorManager.instance.SetTrackingObjOfInterest(true, time);

        ChestLedPublisher.instance.PublishChestLed(BlockColorToColor(color));

        float timeElapsed = 0;
        while (timeElapsed < time) {
            timeElapsed += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        KuriBehaviorManager.instance.SetTrackingObjOfInterest(false);
        ChestLedPublisher.instance.PublishChestLed(Color.black);
        pointingToColor = false;
    }

    public void PointToColor(BLOCKCOLORS color, float time = 0.75f) {
        if (!pointingToColor)
            StartCoroutine(PointForTime(color, time));
    }

}
