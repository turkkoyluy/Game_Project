using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

[System.Serializable]
public class Case
{
    public int id;
    public string title;
    public string description;
    public string question;
}

[System.Serializable]
public class CaseList
{
    public List<Case> cases;
}

public class CaseManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInput;
    public GameObject panel;
    public Button nextButton;

    private List<Case> cases;
    private int currentIndex = 0;

    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "cases.json");

        if (!File.Exists(path))
        {
            Debug.LogError("cases.json dosyası bulunamadı!");
            return;
        }

        string json = File.ReadAllText(path);
        Debug.Log("Okunan JSON:\n" + json);  // JSON çıktısını gör

        CaseList loaded = JsonUtility.FromJson<CaseList>(json);
        if (loaded == null || loaded.cases == null)
        {
            Debug.LogError("JSON içeriği doğru okunamadı veya 'cases' listesi boş!");
            return;
        }

        cases = loaded.cases;

        panel.SetActive(false);
        nextButton.onClick.AddListener(ShowNextCase);
    }


    public void ClosePanel()
    {
        panel.SetActive(false);
    }


    public void OpenCases()
    {
        currentIndex = 0;
        panel.SetActive(true);
        ShowCase();
    }

    void ShowCase()
    {
        if (currentIndex >= cases.Count)
        {
            panel.SetActive(false);
            return;
        }

        var current = cases[currentIndex];
        titleText.text = current.title;
        descriptionText.text = current.description;
        questionText.text = current.question;
        answerInput.text = "";
    }

    void ShowNextCase()
    {
        // Cevap kaydetme kısmı buraya eklenebilir
        currentIndex++;
        ShowCase();
    }
}
