using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CVPanelController : MonoBehaviour
{
    [Header("UI Referansları")]
    public GameObject panelCV;
    public Image cvImage;
    public TMP_Text messageText;

    [Header("Ayarlar")]
    public float transitionSpeed = 4f;
    public float zoomScale = 1.2f;
    public string cvFolderName = "Cv"; // Resources klasör adı

    private Sprite[] cvs;
    private int currentIndex = 0;
    private CanvasGroup imageCanvasGroup;
    private bool isTransitioning = false;

    void Start()
    {
        InitializeCV();
    }

    void InitializeCV()
    {
        // CV klasöründeki görselleri yükle
        cvs = Resources.LoadAll<Sprite>(cvFolderName);
        Debug.Log($"Yüklenen CV sayısı: {cvs.Length}");

        // Panel başlangıçta kapalı
        panelCV.SetActive(false);
        messageText.text = "";

        // CanvasGroup'u hazırla
        PrepareCanvasGroup();

        // İlk CV'yi göster
        if (cvs.Length > 0)
        {
            cvImage.sprite = cvs[0];
        }
        else
        {
            Debug.LogWarning($"Resources/{cvFolderName} klasöründe CV bulunamadı!");
        }
    }

    void PrepareCanvasGroup()
    {
        imageCanvasGroup = cvImage.GetComponent<CanvasGroup>();
        if (imageCanvasGroup == null)
        {
            imageCanvasGroup = cvImage.gameObject.AddComponent<CanvasGroup>();
        }
        imageCanvasGroup.alpha = 1f;
    }

    public void TogglePanel()
    {
        Debug.Log("TogglePanel çağrıldı!");

        bool isActive = panelCV.activeSelf;
        panelCV.SetActive(!isActive);

        // Mesajı temizle
        ClearMessage();

        // Panel açılıyorsa zoom efekti uygula
        if (!isActive)
        {
            ApplyZoomEffect();
        }
    }

    void ApplyZoomEffect()
    {
        if (cvImage != null)
        {
            cvImage.rectTransform.localScale = new Vector3(zoomScale, zoomScale, 1f);
        }
    }

    public void Hire()
    {
        Debug.Log($"CV {currentIndex + 1} HİRED!");
        ShowMessage("<color=green>HİRED!</color>");
    }

    public void Reject()
    {
        Debug.Log($"CV {currentIndex + 1} REJECTED!");
        ShowMessage("<color=red>REJECTED!</color>");
    }

    public void NextCV()
    {
        Debug.Log("NEXT butonuna tıklandı!");

        // CV yoksa veya geçiş sırasındaysa çık
        if (cvs.Length == 0 || isTransitioning)
        {
            Debug.LogWarning("CV yok veya geçiş sırasında!");
            return;
        }

        // Sonraki CV'ye geç
        currentIndex = (currentIndex + 1) % cvs.Length;
        Debug.Log($"Şu anki CV: {currentIndex + 1}/{cvs.Length}");

        // Smooth geçişle yeni görseli göster
        StartCoroutine(SmoothTransition(cvs[currentIndex]));

        // Mesajı temizle
        ClearMessage();
    }

    public void PreviousCV()
    {
        Debug.Log("PREVIOUS butonuna tıklandı!");

        if (cvs.Length == 0 || isTransitioning) return;

        currentIndex = (currentIndex - 1 + cvs.Length) % cvs.Length;
        Debug.Log($"Şu anki CV: {currentIndex + 1}/{cvs.Length}");

        StartCoroutine(SmoothTransition(cvs[currentIndex]));
        ClearMessage();
    }

    void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    void ClearMessage()
    {
        if (messageText != null)
        {
            messageText.text = "";
        }
    }

    IEnumerator SmoothTransition(Sprite newSprite)
    {
        if (newSprite == null)
        {
            Debug.LogError("Yeni sprite null!");
            yield break;
        }

        isTransitioning = true;

        // Fade out
        yield return StartCoroutine(FadeImage(1f, 0f));

        // Sprite'ı değiştir
        cvImage.sprite = newSprite;

        // Fade in
        yield return StartCoroutine(FadeImage(0f, 1f));

        isTransitioning = false;
    }

    IEnumerator FadeImage(float fromAlpha, float toAlpha)
    {
        float elapsedTime = 0f;
        float duration = 1f / transitionSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, elapsedTime / duration);
            imageCanvasGroup.alpha = alpha;
            yield return null;
        }

        imageCanvasGroup.alpha = toAlpha;
    }

    // Debug için
    void OnValidate()
    {
        if (transitionSpeed <= 0)
            transitionSpeed = 4f;

        if (zoomScale <= 0)
            zoomScale = 1.2f;
    }
}