using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_System : MonoBehaviour
{
    public static UI_System Instance = null;

    [SerializeField] Button[] buttons;
    [SerializeField] TMP_InputField[] inputFields;

    private void Awake()
    {
        Instance = this;
    }

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
