using UnityEngine;

public class GiftBoxInteraction : MonoBehaviour
{
    public CoinSpawner coinSpawner;
    private bool hasSpawnedGold = false;

    // Hediye kutusunun kendi Mesh Renderer'ı (görünürlüğünü kontrol etmek için)
    private MeshRenderer giftBoxMeshRenderer;
    // Hediye kutusunun kendi Collider'ı (tıklanabilirliğini kontrol etmek için)
    private Collider giftBoxCollider;

    void Awake()
    {
        // Başlangıçta MeshRenderer ve Collider bileşenlerini alıyoruz.
        giftBoxMeshRenderer = GetComponent<MeshRenderer>();
        giftBoxCollider = GetComponent<Collider>();
    }

    void OnMouseDown()
    {
        if (!hasSpawnedGold && coinSpawner != null)
        {
            coinSpawner.SpawnGoldCoin();
            hasSpawnedGold = true;
            Debug.Log("Hediye kutusuna tıklandı, altın çıktı!");

            // Hediye kutusunu görünmez yap
            if (giftBoxMeshRenderer != null)
            {
                giftBoxMeshRenderer.enabled = false;
            }

            // Hediye kutusunu tıklanamaz yap
            if (giftBoxCollider != null)
            {
                giftBoxCollider.enabled = false;
            }

            // İsteğe bağlı: Bu script'i de devre dışı bırakabilirsin,
            // ama Collider'ı devre dışı bırakmak tıklamayı engelleyecektir.
            // enabled = false; 
        }
        else if (hasSpawnedGold)
        {
            Debug.Log("Bu hediye kutusu zaten kullanıldı.");
        }
        else
        {
            Debug.LogError("GiftBoxInteraction: CoinSpawner referansı ayarlanmamış!");
        }
    }
}