using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadGameInfoScene()
    {
        SceneManager.LoadScene("Description");
    }
}
