using RosSharp.RosBridgeClient;
using System.Collections;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {

    static PuzzleManager puzzleManager;
    Puzzle[] puzzles;
    int currentPuzzle;

    // puzzlenumber, guess, guess_actual_correct, guess_said_correct, behavior
    static string puzNumColumnName = "puzzle_number";
    static string guessColumnName = "guess";
    static string guessActualCorColumnName = "guess_actual_correct";
    static string guessSaidCorColumnName = " guess_said_correct";

    public static PuzzleManager instance {
        get {
            if (!puzzleManager) {
                puzzleManager = FindObjectOfType(typeof(PuzzleManager)) as PuzzleManager;

                if (!puzzleManager) {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else {
                    puzzleManager.Init();
                }
            }
            return puzzleManager;
        }
    }


    void Init() {

        // need to set all puzzles active to get components 
        for (int i = 0; i < transform.childCount; ++i) {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        puzzles = GetComponentsInChildren<Puzzle>();
        currentPuzzle = 0;
        puzzles[currentPuzzle].gameObject.SetActive(true);
        for (int i = 1; i < puzzles.Length; ++i) {
            puzzles[i].gameObject.SetActive(false);
        }

        // Set Log Columns
        LoggingManager.instance.AddLogColumn(puzNumColumnName, "");
        LoggingManager.instance.AddLogColumn(guessColumnName, "");
        LoggingManager.instance.AddLogColumn(guessActualCorColumnName, "");
        LoggingManager.instance.AddLogColumn(guessSaidCorColumnName, "");

        // Log tutorial puzzle
        LoggingManager.instance.UpdateLogColumn(puzNumColumnName, currentPuzzle.ToString());
    }

    IEnumerator TimerQuit() {
        yield return new WaitForSecondsRealtime(600);
        LoggingManager.instance.FinishLogging();
    }

    public bool MakeGuessOnCurPuzzle(int guess) {
        LoggingManager.instance.UpdateLogColumn(guessColumnName, guess.ToString());
        LoggingManager.instance.UpdateLogColumn(guessActualCorColumnName, puzzles[currentPuzzle].CheckAnswerActual(guess).ToString());

        bool givenAnswerCorrect = puzzles[currentPuzzle].CheckAnswer(guess);
        LoggingManager.instance.UpdateLogColumn(guessSaidCorColumnName, givenAnswerCorrect.ToString());

        if (givenAnswerCorrect) {
            if (currentPuzzle == 0) {
                Debug.Log("started");
                // beginning of game
                // start time
                StartCoroutine(TimerQuit());
            }
            puzzles[currentPuzzle].gameObject.SetActive(false);
            currentPuzzle = (currentPuzzle + 1) % puzzles.Length;
            if (currentPuzzle == 0) {
                EventManager.TriggerEvent(EventManager.EVENTS.GameFinished);
                // finish logger and quit
                LoggingManager.instance.FinishLogging();
                return true;
            }
            else {
                LoggingManager.instance.UpdateLogColumn(puzNumColumnName, currentPuzzle.ToString());
                puzzles[currentPuzzle].gameObject.SetActive(true);

                // Kuri positive
                CommandPublisher.instance.PublishAnimationCommand("gotit");
                KuriBehaviorManager.instance.RunRandomPositiveBehavior();

                return true;
            }
        }
        // Kuri negative
        CommandPublisher.instance.PublishAnimationCommand("sad");
        KuriBehaviorManager.instance.RunRandomNegativeBehavior();

        return false;

    }


}
