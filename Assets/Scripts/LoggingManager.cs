using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class LoggingManager : MonoBehaviour {

    public bool logData = false;
    static LoggingManager loggingManager;
    Dictionary<string, int> columnLookup;
    List<string> row;

    string csvFilename;
    string filePath;

    void Init() {
        Debug.Log("Currently logging data: " + logData.ToString());
        csvFilename = System.DateTime.Now.ToString().Replace(' ', '_').Replace('\\', '_').Replace('/', '_').Replace(':', '-') + ".csv";
        columnLookup = new Dictionary<string, int>();
        row = new List<string>();
        filePath = Path.Combine(Application.persistentDataPath, csvFilename);
        Debug.Log(filePath);
    }

    public static LoggingManager instance {
        get {
            if (!loggingManager) {
                loggingManager = FindObjectOfType(typeof(LoggingManager)) as LoggingManager;

                if (!loggingManager) {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else {
                    loggingManager.Init();
                }
            }
            return loggingManager;
        }
    }


    /// <summary>
    /// Adds key to rowLookup, updates the value
    /// </summary>
    /// <param name="key">Column Name</param>
    /// <param name="value">Value for Column</param>
    /// <returns>True if key not already in row, false otherwise</returns>
    public bool AddLogColumn(string key, string value) {
        bool isNewColumn = columnLookup.ContainsKey(key) ? false : true;
        if (isNewColumn) {
            columnLookup[key] = row.Count;
            row.Add(value);
        }
        UpdateLogColumn(key, value);
        return isNewColumn;
    }

    public void UpdateLogColumn(string key, string value) {
        row[columnLookup[key]] = value;
        Assert.AreEqual(columnLookup.Keys.Count, row.Count);
    }


    void ResetRow() {
        for (int i = 0; i < row.Count; ++i) {
            row[i] = "";
        }
    }

    void WriteRowToCSV() {
        if (!logData) {
            return;
        }
        using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
        using (TextWriter writer = new StreamWriter(fs)) {
            writer.WriteLine(string.Join(",", Time.time.ToString(), string.Join(",", row)));
        }
        ResetRow();
    }

    private void FixedUpdate() {
        WriteRowToCSV(); // Wirte a row each fixed update
    }

    IEnumerator DelayQuit() {
        Debug.Log("Quiting");
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }

    public void FinishLogging(bool hasQuit = false) {
        if (!logData) {
            return;
        }
        var ordered = columnLookup.OrderBy(x => x.Value);
        List<string> columnNames = new List<string>();
        foreach (var pairKeyVal in ordered) {
            columnNames.Add(pairKeyVal.Key);
        }
        using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
        using (TextWriter writer = new StreamWriter(fs)) {
            writer.WriteLine(string.Join(",", "Time", string.Join(",", columnNames)));
        }
        if (!hasQuit) {
            StartCoroutine(DelayQuit());
        }
    }

    // Write out columns, will be at end of file
    void OnApplicationQuit() {
        FinishLogging(true);
    }
}
