using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Leaderboards_Controller : MonoBehaviour
{
    private string runInsertion_URL = "https://dbtest01eh.000webhostapp.com/GP3/06_AddRun.php";

    [SerializeField] private TMP_InputField username_Input;
    [SerializeField] private TMP_InputField level_Input;
    [SerializeField] private TMP_InputField playTime_Input;
    [SerializeField] private TMP_InputField score_Input;

    [SerializeField] private Button insertRun_Button;
    [SerializeField] private LogPanel logPanel;

    private void OnEnable()
    {
        insertRun_Button.onClick.AddListener(OnInsertRunButtonPressed);
    }
    private void OnDisable()
    {
        insertRun_Button.onClick.RemoveListener(OnInsertRunButtonPressed);
    }
    public void OnInsertRunButtonPressed()
    {
        StartCoroutine(InsertRunRoutine());
    }

    public IEnumerator InsertRunRoutine()
    {
        UI_System.Instance.BlockInput();

        WWWForm data = new WWWForm();
        data.AddField("PlayerID", username_Input.text);
        data.AddField("CurrentLevel", level_Input.text);
        data.AddField("PlayTime", playTime_Input.text);
        data.AddField("Score", score_Input.text);

        UnityWebRequest request = UnityWebRequest.Post(runInsertion_URL, data);
        yield return request.SendWebRequest();

        if (request.error != null || request.downloadHandler.text.Contains("ERROR"))
        {
            Debug.LogError("ERROR: " + request.downloadHandler.text);
            logPanel.SendLog("ERROR: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            logPanel.SendLog(request.downloadHandler.text);
        }
        UI_System.Instance.EnableInput();

        ResetFields();
    }
    public void ResetFields()
    {
        username_Input.text = "None...";
        level_Input.text = "None...";
        playTime_Input.text = "None...";
        score_Input.text = "None...";
    }


}
