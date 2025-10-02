using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public void LoadStoreScene()
    {
        SceneManager.LoadScene("Scene2-Store");
    }
}
