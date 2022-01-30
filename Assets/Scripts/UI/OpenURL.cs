using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void OpenURLInBrowser(string link)
    {
        if (!string.IsNullOrWhiteSpace(link))
        {
            Application.OpenURL(link);
        }
    }
}
