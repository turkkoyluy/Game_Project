using UnityEngine;
using UnityEngine.UI;

public class SoundManger : MonoBehaviour
{
    [SerializeField] private RawImage SoundOnIcon;
    [SerializeField] private RawImage SoundOfIcon;

    private bool isMuted;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("isMuted"))
        {
            PlayerPrefs.SetInt("isMuted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = isMuted;
    }

    public void OnButtonPress()
    {
        if (!isMuted)
        {
            isMuted = true;
            AudioListener.pause = true;
        }
        else
        {
            isMuted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateButtonIcon();
    }
    private void UpdateButtonIcon()
    {
        if (!isMuted)
        {
            SoundOnIcon.enabled = true;
            SoundOfIcon.enabled = false;
        }
        else
        {
            SoundOnIcon.enabled = false;
            SoundOfIcon.enabled = true;
        }
    }
    private void Load()
    {
        isMuted = PlayerPrefs.GetInt("isMuted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
    }

}