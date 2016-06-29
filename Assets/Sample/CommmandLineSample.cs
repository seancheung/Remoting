// ************************
// Author: Sean Cheung
// Create: 2016/06/29/11:54
// Modified: 2016/06/29/12:11
// ************************

using System;
using Eyesar.CommandLine;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class CommmandLineSample : MonoBehaviour, ILogHandler
{
    public Text text;

    private void Awake()
    {
        Debug.logger.logHandler = this;
    }

    private void Start()
    {
        Debug.LogFormat("Executable: {0}", CommandParser.Executable);
        Debug.LogFormat("Count: {0}", CommandParser.Count);
        Debug.LogFormat("string: {0}", CommandParser.GetString("string", false));
        Debug.LogFormat("int: {0}", CommandParser.GetInt("int", false));
        Debug.LogFormat("bool: {0}", CommandParser.GetBool("bool", false));
    }

    #region Implementation of ILogHandler

    public void LogFormat(LogType logType, Object context, string format, params object[] args)
    {
        text.text += string.Format(format, args) + Environment.NewLine;
    }

    public void LogException(Exception exception, Object context)
    {
        text.text += exception.Message + Environment.NewLine;
    }

    #endregion
}