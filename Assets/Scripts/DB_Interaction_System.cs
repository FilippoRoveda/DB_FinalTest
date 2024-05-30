using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

/// <summary>
/// Primary singleton class that enable and handle interactions with the online database.
/// </summary>
public class DB_Interaction_System : MonoBehaviour
{
    public static DB_Interaction_System Instance = null;

    //Controller fields

    [SerializeField] private Registration_Controller registrationController;
    [SerializeField] private Login_Controller loginController;
    [SerializeField] private Logout_Controller logoutController;

    [SerializeField] private LogPanel logPanel;

    private string connection_URL = "https://dbtest01eh.000webhostapp.com/GP3/01_DB_Connection.php";
    private string disconnection_URL = "https://dbtest01eh.000webhostapp.com/GP3/08_DB_CloseConnection.php";
    private string registration_URL = "https://dbtest01eh.000webhostapp.com/GP3/02_Player_Registration.php?";
    private string login_URL = "https://dbtest01eh.000webhostapp.com/GP3/03_Player_Login.php?";
    private string logout_URL = "https://dbtest01eh.000webhostapp.com/GP3/04_Player_Logout.php?";

    private bool isLogged = false;
    private float sessionLenght = 0.0f;

    public static UnityEvent RegisteredUserUpdate = new UnityEvent();

    #region Unity callbacks
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(Try_DBConnection());
    }

    private void OnDisable()
    {
        StartCoroutine(Try_DBConnectionClosing());
    }


    private void Update()
    {
        if(isLogged)
        {
            sessionLenght += Time.deltaTime;
        }
    }
    #endregion

    #region Database interaction routines
    /// <summary>
    /// Try connection to database.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Try_DBConnection()
    {
        UI_System.Instance.BlockInput();

        UnityWebRequest request = UnityWebRequest.Get(connection_URL);
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.LogError("Request ERROR: " + request.error);
            logPanel.SendLog("Request ERROR: " + request.error);
        }
        
        if(request.downloadHandler.text.Contains("ERROR"))
        {
            Debug.LogError(request.downloadHandler.text);
            logPanel.SendLog(request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            logPanel.SendLog(request.downloadHandler.text);
        }

        UI_System.Instance.EnableInput();
    }
    /// <summary>
    /// Try to launch a user registration.
    /// </summary>
    /// <param name="name">Name and surname of the user.</param>
    /// <param name="username">Username selected by the user.</param>
    /// <param name="password">Password selected by the user.</param>
    /// <returns></returns>
    public IEnumerator Try_PlayerRegistration(string name, string username, string password)
    {
        UI_System.Instance.BlockInput();

        WWWForm data = new WWWForm();
        data.AddField("Name", name);
        data.AddField("Username", username);
        data.AddField("Password", password);

        UnityWebRequest request = UnityWebRequest.Post(registration_URL, data);
        yield return request.SendWebRequest();

        if (request.error != null || request.downloadHandler.text.Contains("ERROR"))
        {
            Debug.LogError("ERROR: " + request.downloadHandler.text);
            logPanel.SendLog("ERROR: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            logPanel.SendLog(request.downloadHandler.text);
        }

        registrationController.ResetFields();
        UI_System.Instance.EnableInput();

        RegisteredUserUpdate?.Invoke();
    }
    /// <summary>
    /// Try a user login procedure.
    /// </summary>
    /// <param name="username">Username selected by the user.</param>
    /// <param name="password">Password selected by the user.</param>
    /// <returns></returns>
    public IEnumerator TryPlayerLogin(string username, string password)
    {
        UI_System.Instance.BlockInput();

        WWWForm data = new WWWForm();
        data.AddField("Username", username);
        data.AddField("Password", password);


        UnityWebRequest request = UnityWebRequest.Post(login_URL, data);
        yield return request.SendWebRequest();

        if (request.error != null || request.downloadHandler.text.Contains("ERROR"))
        {
            Debug.LogError("ERROR: " + request.downloadHandler.text);
            logPanel.SendLog("ERROR: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            logPanel.SendLog(request.downloadHandler.text);

            logoutController.EnableLogoutButton();
            logoutController.SetLoggedUsername( username );
            loginController.ResetFields();

            ActivateTimer();
        }

        UI_System.Instance.EnableInput();
    }
    /// <summary>
    /// Execute the logout for the player.
    /// </summary>
    /// <param name="username">Username selected by the user.</param>
    /// <returns></returns>
    public IEnumerator TryPlayerLogout(string username)
    {
        UI_System.Instance.BlockInput();

        BlockTimer();
        int intSessionLenght = (int)sessionLenght;

        WWWForm data = new WWWForm();
        data.AddField("Username", username);
        data.AddField("SessionLenght", intSessionLenght);
        UnityWebRequest request = UnityWebRequest.Post(logout_URL, data);

        yield return request.SendWebRequest();

        if (request.error != null || request.downloadHandler.text.Contains("ERROR"))
        {
            Debug.LogError("ERROR: " + request.downloadHandler.text);
            logPanel.SendLog("ERROR: " + request.downloadHandler.text);
        }

        else
        {
            Debug.Log(request.downloadHandler.text);

            loginController.EnableFields();
            logoutController.DisableLogoutButton();
            logoutController.SetLoggedUsername("None");

            ResetTimer();
        }

        loginController.ResetFields();
        UI_System.Instance.EnableInput();
    }
    /// <summary>
    /// Try to close the database connection.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Try_DBConnectionClosing()
    {
        UI_System.Instance.BlockInput();

        UnityWebRequest request = UnityWebRequest.Get(disconnection_URL);
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            Debug.LogError("Request ERROR: " + request.error);
            logPanel.SendLog("Request ERROR: " + request.error);
        }

        if (request.downloadHandler.text.Contains("ERROR"))
        {
            Debug.LogError(request.downloadHandler.text);
            logPanel.SendLog(request.downloadHandler.text);
        }
    }
    #endregion

    #region Utilities
    private void ActivateTimer()
    {
        isLogged = true;
    }
    private void BlockTimer()
    {
        isLogged = false;
    }
    private void ResetTimer()
    {
        sessionLenght = 0.0f;
    }
    #endregion
}
