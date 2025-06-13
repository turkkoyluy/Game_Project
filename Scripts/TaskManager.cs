using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    [System.Serializable]
    public class Day
    {
        public int day;
        public string title;
        public string[] tasks;
    }

    [System.Serializable]
    public class DayList
    {
        public Day[] days;
    }

    public TMP_Text titleText;
    public Toggle[] taskToggles;
    public GameObject taskPanel;
    public GameObject endOfDayPanel;

    public TMP_Text goldText;
    public Button nextDayButton;

    // 🔊 Ses sistemi için eklenenler
    public AudioSource audioSource;
    public AudioClip applauseClip;

    private DayList loadedData;
    public int currentDay = 1;
    private int gold = 0;

    private IEnumerator StopSoundAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        audioSource.Stop();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level2Scene");
    }

    void Start()
    {
        LoadTasks();
        taskPanel.SetActive(false);
        endOfDayPanel.SetActive(false);

        OpenTaskPanel();
    }


    void LoadTasks()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("dailyTasks_en");
        if (jsonFile != null)
        {
            loadedData = JsonUtility.FromJson<DayList>(jsonFile.text);
            Debug.Log("JSON yüklendi.");
        }
        else
        {
            Debug.LogError("dailyTasks_en.json bulunamadı! Resources klasöründe mi?");
        }
    }

    public void ShowTasksForToday()
    {
        taskPanel.SetActive(true);
        endOfDayPanel.SetActive(false);
        nextDayButton.gameObject.SetActive(false);

        Day today = null;
        foreach (Day d in loadedData.days)
        {
            if (d.day == currentDay)
            {
                today = d;
                break;
            }
        }

        if (today != null)
        {
            titleText.text = today.title;

            for (int i = 0; i < taskToggles.Length; i++)
            {
                if (i < today.tasks.Length)
                {
                    taskToggles[i].gameObject.SetActive(true);
                    taskToggles[i].isOn = false;
                    taskToggles[i].GetComponentInChildren<TMP_Text>().text = today.tasks[i];
                }
                else
                {
                    taskToggles[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            titleText.text = "Bugünlük görev yok.";
            foreach (Toggle toggle in taskToggles)
                toggle.gameObject.SetActive(false);
        }
    }

    public void OpenTaskPanel()
    {
        ShowTasksForToday();
    }

    public void ClosePanel()
    {
        taskPanel.SetActive(false);
    }

    public void SubmitTasks()
    {
        bool allComplete = true;
        int completedTasks = 0;

        foreach (Toggle toggle in taskToggles)
        {
            if (toggle.gameObject.activeSelf)
            {
                if (toggle.isOn)
                    completedTasks++;
                else
                    allComplete = false;
            }
        }

        if (allComplete)
        {
            int earned = completedTasks * 10;
            gold += earned;

            endOfDayPanel.SetActive(true);
            goldText.text = $"Tebrikler! Gün bitti.\nKazancınız: {earned} altın";
            nextDayButton.gameObject.SetActive(true);

            // 🔊 Alkış sesi çal
            if (audioSource != null && applauseClip != null)
            {
                audioSource.clip = applauseClip;
                audioSource.Play();
                StartCoroutine(StopSoundAfterSeconds(3f));
            }

        }
        else
        {
            Debug.Log("Tüm görevler tamamlanmalı.");
        }
    }

    public void NextDay()
    {
        currentDay++;
        ShowTasksForToday();
    }
}
