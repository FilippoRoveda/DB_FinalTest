using UnityEngine;
using UnityEngine.UI;

public class PassIntButton : MonoBehaviour
{
    private Button button;
    [SerializeField] private int selectionNum;

    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        Leaderboards_Controller.instance.SelectRunRank(selectionNum);
    }
}
