using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameInfoManager : MonoBehaviour
{
    [System.Serializable]
    public class GameInfoItem
    {
        public string title;
        public string description;
    }

    [System.Serializable]
    public class GameInfoList
    {
        public List<GameInfoItem> gameInfo;
    }

    public TextMeshProUGUI infoText;

    void Start()
    {
        LoadGameInfo();
    }

    void LoadGameInfo()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("GameInfo");
        if (jsonFile != null)
        {
            GameInfoList infoList = JsonUtility.FromJson<GameInfoList>(jsonFile.text);
            string allText = "";
            foreach (var item in infoList.gameInfo)
            {
                allText += $"<b>{item.title}:</b> {item.description}\n\n";
            }
            infoText.text = allText;
        }
        else
        {
            infoText.text = "Bilgi dosyası bulunamadı.";
        }
    }
}
