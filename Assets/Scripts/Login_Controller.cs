using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login_Controller : MonoBehaviour
{
    [SerializeField] private Button logoutButton;

    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;

    private void OnEnable()
    {
        logoutButton.onClick.AddListener(OnLoginButtonPressed);
    }

    public void OnLoginButtonPressed()
    {
        StartCoroutine(DB_Interaction_System.Instance.TryPlayerLogin(usernameField.text, passwordField.text));
    }


    public void EnableFields()
    {
        logoutButton.interactable = true;
        usernameField.interactable = true;
        passwordField.interactable = true;
    }
    public void DisableFields()
    {
        usernameField.text = "DISABLED";
        passwordField.text = "DISABLED";
        logoutButton.interactable= false;
        usernameField.interactable = false;
        passwordField.interactable = false;
    }
    public void ResetFields()
    {
        usernameField.text = string.Empty;
        passwordField.text = string.Empty;
    }
}
