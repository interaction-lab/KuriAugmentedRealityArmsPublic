
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour {

    public List<int> answers;

    bool firstGuess;

    void Start() {
        if (answers.Count == 0) {
            Debug.LogError("No answers given for puzzle - " + transform.name + "- so please add them in unity editor");
        }
        firstGuess = true;
    }

    /// <summary>
    /// Checks for the answer with no modifier of the correct answers
    /// </summary>
    /// <param name="guess"></param>
    /// <returns></returns>
    public bool CheckAnswerActual(int guess) {
        return answers.Contains(guess);
    }

    public bool CheckAnswer(int guess) {
        if (firstGuess && answers.Count > 1) {
            firstGuess = false;
            answers.Remove(guess);
            return false;
        }

        return answers.Contains(guess);
    }
}
