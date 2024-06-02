using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Conrtoller that handle the regiatration screen UIs.
/// </summary>
public class Registration_Controller : MonoBehaviour
{
    [SerializeField] private Button registrationButton;
     
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;

    #region Unity callbacks
    private void OnEnable()
    {
        registrationButton.onClick.AddListener(OnRegistrationButtonPressed);
    }
    #endregion

    #region Callbacks

    public void OnRegistrationButtonPressed()
    {
        StartCoroutine(DB_Interaction_System.Instance.Try_PlayerRegistration(nameField.text, usernameField.text, passwordField.text));
    }
    public void OnResetRegisteredUserButtonPressed()
    {
        StartCoroutine(DB_Interaction_System.Instance.Try_RegisteredUserReset());
    }
    #endregion

    public void ResetFields()
    {
        nameField.text = string.Empty;
        usernameField.text = string.Empty;
        passwordField.text = string.Empty;
    }

}
