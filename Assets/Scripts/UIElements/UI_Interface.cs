using UnityEngine;
/// <summary>
/// Common interface class that enable basic interaction with it.
/// </summary>
public class UI_Interface : MonoBehaviour
{
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
}
