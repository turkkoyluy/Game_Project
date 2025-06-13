using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene("SampleScene"); // geçilecek sahnenin adı
    }
}
