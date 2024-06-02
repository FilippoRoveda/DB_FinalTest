using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// Utility class that contain run data from Json objects.
/// </summary>
[System.Serializable]
public struct Run
{
    public string ID { get; set; }
    public string PlayerID { get; set; }
    public string CurrentLevel { get; set; }
    public string PlayTime { get; set; }
    public string Score { get; set; }
}

/// <summary>
/// Controller for the leaderboards ui interface.
/// </summary>
public class Leaderboards_Controller : MonoBehaviour
{
    public static Leaderboards_Controller instance;

    private string runInsertion_URL = "https://dbtest01eh.000webhostapp.com/GP3/06_AddRun.php";
    private string runSelection_URL = "https://dbtest01eh.000webhostapp.com/GP3/07_GetRunByLevel.php";

    [SerializeField] private TMP_InputField username_Input;
    [SerializeField] private TMP_InputField level_Input;
    [SerializeField] private TMP_InputField playTime_Input;
    [SerializeField] private TMP_InputField score_Input;

    [SerializeField] private Transform rankedList;
    [SerializeField] private GameObject runPrefab;

    [SerializeField] private Button insertRun_Button;
    [SerializeField] private LogPanel logPanel;

    [SerializeField] private List<GetIntButton> selectLevelButtons;

    #region Unity callbacks
    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        insertRun_Button.onClick.AddListener(OnInsertRunButtonPressed);
    }
    private void OnDisable()
    {
        insertRun_Button.onClick.RemoveListener(OnInsertRunButtonPressed);
    }

    private void Start()
    {
        OnRunRakSelection(0);
    }
    #endregion

    #region Callbacks
    public void OnInsertRunButtonPressed()
    {
        StartCoroutine(InsertRunRoutine());
    }

    public void OnRunRakSelection(int runRank)
    {
        DestroyRunPrefabs();
        StartCoroutine(SelectRunRoutine(runRank));
    }
    #endregion

    #region Utilities
    public void ResetFields()
    {
        username_Input.text = "None...";
        level_Input.text = "None...";
        playTime_Input.text = "None...";
        score_Input.text = "None...";
    }
    private void DestroyRunPrefabs()
    {
        for (int i = 0; i < rankedList.transform.childCount; i++)
        {
            Destroy(rankedList.transform.GetChild(i).gameObject);
        }
    }

    //Make possible to choice ascendent or descendent
    //Pass a predicate in which insert the 2 elements of the array
    void ScoreBubbleSort(ref List<Run> array)
    {
        int size = array.Count;

        for (int step = 0; step < size; ++step)
        {
            for (int i = 0; i < size - step - 1; ++i)
            {
                if (int.Parse(array[i].Score) < int.Parse(array[i + 1].Score))
                {
                    Run temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;
                }
            }
        }
    }
    #endregion


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

            ResetFields();
            DestroyRunPrefabs();
            yield return StartCoroutine(SelectRunRoutine(0));
        }

        UI_System.Instance.EnableInput();  
    }


    private IEnumerator SelectRunRoutine(int i)
    {
        UI_System.Instance.BlockInput();

        WWWForm data = new WWWForm();
        data.AddField("Level", i.ToString());

        UnityWebRequest request = UnityWebRequest.Post(runSelection_URL, data);
        yield return request.SendWebRequest();

        if (request.error != null || request.downloadHandler.text.Contains("ERROR"))
        {
            Debug.LogError("ERROR: " + request.downloadHandler.text);
            logPanel.SendLog("ERROR: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogWarning(request.downloadHandler.text);
            List<Run> res = JsonConvert.DeserializeObject<List<Run>>(request.downloadHandler.text);
            ScoreBubbleSort(ref res);
            int c = 1;
            foreach (var r in res)
            {
                var obj = Instantiate(runPrefab, rankedList.transform);

                var comp = obj.GetComponent<RunSlot>();
                comp.position.text = c.ToString();
                c++;
                comp.level.text = r.CurrentLevel.ToString();
                comp.user.text = r.PlayerID.ToString();
                comp.time.text = r.PlayTime.ToString();
                comp.score.text = r.Score.ToString();
            }
        }
        UI_System.Instance.EnableInput();
    }
}
