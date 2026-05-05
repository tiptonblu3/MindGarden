using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMain : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
