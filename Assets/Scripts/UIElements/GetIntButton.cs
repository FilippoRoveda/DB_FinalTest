using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Custom wrapper class that send an integer.
/// </summary>
public class GetIntButton : MonoBehaviour
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
        Leaderboards_Controller.instance.OnRunRakSelection(selectionNum);
    }
}
