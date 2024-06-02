using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void Quit()
    {
        StartCoroutine(QuitRoutine());
    }
    private IEnumerator QuitRoutine()
    {
        yield return DB_Interaction_System.Instance.Try_DBConnectionClosing();
        Application.Quit(); 
    }
}
