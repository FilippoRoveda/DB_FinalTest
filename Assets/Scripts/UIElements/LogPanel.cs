using TMPro;
using UnityEngine;

/// <summary>
/// UI class that display logging messages.
/// </summary>
public class LogPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text logMessageText;

    public void SendLog(string message)
    {
        logMessageText.text = message;
    }
}
