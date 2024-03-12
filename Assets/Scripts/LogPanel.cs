using TMPro;
using UnityEngine;

public class LogPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text logMessageText;

    public void SendLog(string message)
    {
        logMessageText.text = message;
    }
}
