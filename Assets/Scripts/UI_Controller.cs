using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private UI_Interface registration_Screen;
    [SerializeField] private UI_Interface login_Screen;
    [SerializeField] private UI_Interface leaderboards_Screen;
    [SerializeField] private UI_Interface home_Screen;

    #region Callbacks
    public void OpenRegistrationScreen()
    {
        home_Screen.Hide();
        login_Screen.Hide();
        leaderboards_Screen.Hide();
        registration_Screen.Show();
    }
    public void OpenLoginScreen()
    {
        home_Screen.Hide();
        leaderboards_Screen.Hide();
        registration_Screen.Hide();
        login_Screen.Show();
    }
    public void OpenLeaderboardsScreen()
    {
        home_Screen.Hide();
        login_Screen.Hide();
        registration_Screen.Hide();
        leaderboards_Screen.Show();
    }

    public void CloseScreens()
    {
        login_Screen.Hide();
        leaderboards_Screen.Hide();
        registration_Screen.Hide();
        home_Screen.Show();
    }
    #endregion
}
