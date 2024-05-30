using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_System : MonoBehaviour
{
    public static UI_System Instance = null;

    [SerializeField] Button[] buttons;
    [SerializeField] TMP_InputField[] inputFields;

    #region Unity callbacks
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    /// <summary>
    /// Block interaction with every interactable element in the user interface.
    /// </summary>
    public void BlockInput()
    {
        foreach (var button in buttons) 
        {
            button.interactable = false;
        }
        foreach (var field in inputFields)
        {
            field.interactable = false;
        }
    }
    /// <summary>
    /// Enable interaction with every interactable element in the user interface.
    /// </summary>
    public void EnableInput()
    {
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
        foreach (var field in inputFields)
        {
            field.interactable = true;
        }
    }
}
