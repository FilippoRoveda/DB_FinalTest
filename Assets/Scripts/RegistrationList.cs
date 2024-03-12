using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public struct DisplayedUser
{
    public string ID;
    public string Username;
}

[System.Serializable]
public struct TableWrapper<T>
{
    public T[] results;
}


public class RegistrationList : MonoBehaviour
{
    [SerializeField] private GameObject userPrefab;
    [SerializeField] private GameObject list;

    private string url = "https://dbtest01eh.000webhostapp.com/GP3/05_GetRegisteredUsers.php";


    private void Awake()
    {
        RefreshRegusteredUserList();
    }
    private void OnEnable()
    {
        DB_Interaction_System.RegisteredUserUpdate.AddListener(RefreshRegusteredUserList);
    }
    private void OnDisable()
    {
        DB_Interaction_System.RegisteredUserUpdate.RemoveListener(RefreshRegusteredUserList);
    }

    public void RefreshRegusteredUserList()
    {
        for(int i = 0; i < list.transform.childCount; i++) 
        {
            Destroy(list.transform.GetChild(i).gameObject);
        }
        StartCoroutine(RequestCoroutine());
    }

    public IEnumerator RequestCoroutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        
        //List<DisplayedUser> res = JsonConvert.DeserializeObject<List<DisplayedUser>>(request.downloadHandler.text);

        //foreach (var r in res)
        //{
        //    var obj = Instantiate(userPrefab, list.transform);
        //    var comp = obj.GetComponent<RegisteredUserPrefab>();
        //    comp.id.text = r.ID;
        //    comp.username.text = r.Username;
        //}
    }

}
