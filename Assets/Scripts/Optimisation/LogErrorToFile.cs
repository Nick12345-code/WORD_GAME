using UnityEngine;
using System.IO;

public class LogErrorToFile : MonoBehaviour
{
    string fileName = "";

    void OnEnable() => Application.logMessageReceived += Log;
    void OnDisable() => Application.logMessageReceived -= Log;
    void Start() => fileName = Application.dataPath + "/LogFile.text";

    public void Log(string logString, string stackTrace, LogType type)
    {
        TextWriter tw = new StreamWriter(fileName, true);
        tw.WriteLine("" + logString);
        tw.Close();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
             Debug.Log("Writing the console to a text file test!");
          
        }
    }
}
