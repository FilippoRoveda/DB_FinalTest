using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;


[System.Serializable]
public struct DisplayedUser
{
    public string ID { get; set; }
    public string Username { get; set; }
}

/// <summary>
/// UI class that contain and handle the list of registered users.
/// </summary>
public class RegistrationList : MonoBehaviour
{
    [SerializeField] private GameObject userPrefab;
    [SerializeField] private GameObject list;

    private string url = "https://dbtest01eh.000webhostapp.com/GP3/05_GetRegisteredUsers.php";


    #region Unity callbacks
    private void Awake()
    {
        RefreshRegisteredUserList();
    }
    private void OnEnable()
    {
        DB_Interaction_System.RegisteredUserUpdate.AddListener(RefreshRegisteredUserList);
    }
    private void OnDisable()
    {
        DB_Interaction_System.RegisteredUserUpdate.RemoveListener(RefreshRegisteredUserList);
    }
    #endregion

    public void RefreshRegisteredUserList()
    {
        for(int i = 0; i < list.transform.childCount; i++) 
        {
            Destroy(list.transform.GetChild(i).gameObject);
        }
        StartCoroutine(RequestRegisteredUserCoroutine());
    }

    /// <summary>
    /// Coroutine that send a web request to obtain every registered user.
    /// </summary>
    /// <returns></returns>
    public IEnumerator RequestRegisteredUserCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        
        List<DisplayedUser> res = JsonConvert.DeserializeObject<List<DisplayedUser>>(request.downloadHandler.text);

        foreach (var r in res)
        {
            var obj = Instantiate(userPrefab, list.transform);
            var comp = obj.GetComponent<RegisteredUserPrefab>();
            comp.id.text = r.ID;
            comp.username.text = r.Username;
        }
    }

}
