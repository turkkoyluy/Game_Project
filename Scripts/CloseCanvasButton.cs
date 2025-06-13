using UnityEngine;
using UnityEngine.UI;

public class SimpleCanvasCloser : MonoBehaviour
{
    public GameObject infoPanel; // Inspector'dan sürükleyip bırakacağınız canvas

    void Start()
    {
        // Buton bileşenini al
        Button btn = GetComponent<Button>();

        // Buton tıklama olayını ayarla
        if (btn != null)
        {
            btn.onClick.AddListener(OnButtonClick);
            Debug.Log("Buton dinleyicisi eklendi!");
        }
        else
        {
            Debug.LogError("Buton bileşeni bulunamadı!");
        }
    }

    // Butona tıklandığında çağrılacak fonksiyon
    public void OnButtonClick()
    {
        Debug.Log("Buton tıklandı!");

        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
            Debug.Log("Canvas kapatıldı!");
        }
        else
        {
            Debug.LogError("canvasToClose referansı ayarlanmamış!");
        }
    }
}