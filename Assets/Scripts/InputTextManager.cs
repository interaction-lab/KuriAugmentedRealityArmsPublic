using TMPro;
using UnityEngine;

public class InputTextManager : MonoBehaviour {

    TextMeshProUGUI textMesh;
    public static InputTextManager inputTextManager;
    int currentGuess;

    public static InputTextManager instance {
        get {
            if (!inputTextManager) {
                inputTextManager = FindObjectOfType(typeof(InputTextManager)) as InputTextManager;

                if (!inputTextManager) {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else {
                    inputTextManager.Init();
                }
            }
            return inputTextManager;
        }
    }


    void Init() {
        textMesh = GetComponent<TextMeshProUGUI>();
        currentGuess = 0;
    }

    public void IncrementGuess() {
        UpdateInputText(currentGuess + 1);
    }
    public void DecrementGuess() {
        UpdateInputText(currentGuess - 1);
    }

    public void UpdateInputText(int number) {
        currentGuess = number;
        textMesh.text = number.ToString();
    }

    public int GetCurrentGuess() {
        return currentGuess;
    }
}
