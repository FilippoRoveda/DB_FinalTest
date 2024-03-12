using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Logout_Controller : MonoBehaviour
{
    [SerializeField] private TMP_Text loggedUsername;

    [SerializeField] private Button logoutButton;

    private void OnEnable()
    {
        DisableLogoutButton();
        logoutButton.onClick.AddListener(OnLogoutButtonPressed);
    }

    private void OnLogoutButtonPressed()
    {
        DB_Interaction_System.Instance.TryPlayerLogout(loggedUsername.text);
    }
    public void SetLoggedUsername(string username)
    {
        loggedUsername.text = username; 
    }
    public void EnableLogoutButton()
    {
        logoutButton.interactable = true;
    }
    public void DisableLogoutButton()
    {
        logoutButton.interactable = false;
    }
}
