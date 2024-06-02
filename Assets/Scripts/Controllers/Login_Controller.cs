using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login_Controller : MonoBehaviour
{
    [SerializeField] private Button loginButton;

    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;

    private void OnEnable()
    {
        loginButton.onClick.AddListener(OnLoginButtonPressed);
    }

    public void OnLoginButtonPressed()
    {
        StartCoroutine(DB_Interaction_System.Instance.TryPlayerLogin(usernameField.text, passwordField.text));
    }


    public void EnableFields()
    {
        usernameField.text = "Insert username...";
        passwordField.text = "Insert password...";

        usernameField.enabled = true;
        passwordField.enabled = true;
        loginButton.interactable = true;
        loginButton.gameObject.SetActive(true);
    }
    public void DisableFields()
    {
        usernameField.text = "DISABLED";
        passwordField.text = "DISABLED";


        usernameField.enabled = false;
        passwordField.enabled = false;
        loginButton.interactable = false;  
        loginButton.gameObject.SetActive(false);
    }
    public void ResetFields()
    {
        usernameField.text = "Insert username...";
        passwordField.text = "Insert password...";
    }
}
