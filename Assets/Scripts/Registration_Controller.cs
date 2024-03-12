using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Registration_Controller : MonoBehaviour
{
    [SerializeField] private Button registrationButton;
     
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;


    private void OnEnable()
    {
        registrationButton.onClick.AddListener(OnRegistrationButtonPressed);
    }

    public void OnRegistrationButtonPressed()
    {
        StartCoroutine(DB_Interaction_System.Instance.Try_PlayerRegistration(nameField.text, usernameField.text, passwordField.text));
    }

    public void ResetFields()
    {
        nameField.text = string.Empty;
        usernameField.text = string.Empty;
        passwordField.text = string.Empty;
    }

}
