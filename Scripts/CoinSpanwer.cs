using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Altın Ayarları")]
    public GameObject goldCoinPrefab;
    public Transform goldSpawnPoint;
    public float disappearTime = 2f;

    [Header("Ses Ayarları")]
    public AudioClip goldCollectSound; // Altın sesi
    private AudioSource audioSource; // CoinSpawner'ın kendi AudioSource'u

    // Mevcut arka plan sesini çalan AudioSource referansı
    public AudioSource backgroundAudioSource; // Buraya Inspector'dan sürükleyeceğiz

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.spatialBlend = 1.0f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.maxDistance = 20f;
        audioSource.volume = 0.7f;
        audioSource.playOnAwake = false;
    }

    public void SpawnGoldCoin()
    {
        if (goldCoinPrefab == null)
        {
            Debug.LogError("CoinSpawner: Gold Coin Prefab ayarlanmamış!");
            return;
        }
        if (goldSpawnPoint == null)
        {
            Debug.LogError("CoinSpawner: Gold Spawn Point ayarlanmamış! (GoldSpawnPoint adlı boş obje olmalı)");
            return;
        }

        GameObject newGold = Instantiate(goldCoinPrefab, goldSpawnPoint.position, Quaternion.identity);
        Debug.Log("Altın çıktı: " + newGold.name);

        // Ses çalma mantığı
        if (goldCollectSound != null && audioSource != null)
        {
            // 1. Arka plan sesini durdur
            if (backgroundAudioSource != null && backgroundAudioSource.isPlaying)
            {
                backgroundAudioSource.Pause(); // Duraklat (kaldığı yerden devam etmek için)
                // Veya: backgroundAudioSource.Stop(); // Tamamen durdurup baştan başlatır
                Debug.Log("Arka plan sesi duraklatıldı.");
            }

            // 2. Altın sesini çal
            audioSource.PlayOneShot(goldCollectSound);
            Debug.Log("Altın toplama sesi çalındı.");

            // 3. Altın sesi bittikten sonra arka plan sesini devam ettir
            // Altın sesinin süresi kadar bekleyip sonra devam ettireceğiz.
            StartCoroutine(ResumeBackgroundMusic(goldCollectSound.length));
        }
        else if (goldCollectSound == null)
        {
            Debug.LogWarning("CoinSpawner: Gold Collect Sound atanmamış!");
        }

        Destroy(newGold, disappearTime);
    }

    // IEnumerator (Coroutine) kullanarak belirli bir süre bekleyeceğiz
    private System.Collections.IEnumerator ResumeBackgroundMusic(float delay)
    {
        yield return new WaitForSeconds(delay); // Altın sesi süresi kadar bekle

        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.UnPause(); // Duraklatılmış sesi devam ettir
            // Veya: backgroundAudioSource.Play(); // Eğer Stop() kullandıysanız baştan başlatır
            Debug.Log("Arka plan sesi devam ettirildi.");
        }
    }
}