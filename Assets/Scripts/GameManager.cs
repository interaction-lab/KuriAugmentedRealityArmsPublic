using RosSharp.RosBridgeClient;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    UnityAction rosConnectListener;

    public enum GAMESTATES {
        Awake,
        Starting,
        Idle
    }

    GAMESTATES currentState;

    private void Awake() {
        if (instance == null)
            instance = this;
        rosConnectListener = new UnityAction(StartGame);
        currentState = GAMESTATES.Awake;
    }

    IEnumerator StartUpOverride() {
        currentState = GAMESTATES.Idle;
        while (CommandPublisher.instance == null &&
            ChestLedPublisher.instance == null) {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.5f);

        CommandPublisher.instance.PublishOverrideCommand();
        Debug.Log("Override command sent");
        ChestLedPublisher.instance.PublishChestLed(Color.white);
        yield return new WaitForSeconds(1f);
        ChestLedPublisher.instance.PublishChestLed(Color.black);

    }

    void StartGame() {
        currentState = GAMESTATES.Starting;
    }

    private void Update() {
        switch (currentState) {
            case GAMESTATES.Starting:
                StartCoroutine(StartUpOverride());
                break;
            case GAMESTATES.Idle:
                break;
        }

    }

    void OnEnable() {
        EventManager.StartListening(EventManager.EVENTS.RosConnectionReady, rosConnectListener);
    }

    void OnDisable() {
        EventManager.StopListening(EventManager.EVENTS.RosConnectionReady, rosConnectListener);
    }


}
