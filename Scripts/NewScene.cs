using UnityEngine;
using UnityEngine.SceneManagement; // Bu satırı eklemeyi unutmayın!

public class GameManager : MonoBehaviour
{
    // Bu fonksiyon, butona tıklandığında çağrılacak.
    public void LoadStartScene()
    {
        // "StartScene" yerine başlangıç sahnenizin adını yazmalısınız.
        SceneManager.LoadScene("StartScene");
    }

    // Eğer oyunu tamamen kapatmak isterseniz bu fonksiyonu kullanabilirsiniz.
    // Amaç Start Scene'e dönmek olduğu için şimdilik üstteki fonksiyona odaklanalım.
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapatıldı."); // Bu sadece editörde görünür, derlenmiş oyunda görünmez.
    }
}